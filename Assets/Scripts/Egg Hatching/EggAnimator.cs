using UnityEngine;
using System.Collections;

public class EggAnimator : MonoBehaviour
{
    [Header("Egg sprites")]
    [SerializeField] private Sprite crackedEggSprite;
    [SerializeField] private GameObject crackedEggTop;
    [SerializeField] private GameObject crackedEggBottom;
    [Header("Animation settings")]
    [Range(0f, 8f)][SerializeField] private float shakeDistance = 8f;
    [Range(0f, 20f)][SerializeField] private float shakeSpeed = 20f;
    [Range(0f, 5f)][SerializeField] private float shakeCooldown = 1f;
    [Range(0f, 5f)][SerializeField] private float crackedAnimationDuration = 3f;
    [Header("Components")]
    private SpriteRenderer petSpriteRenderer;

    private Vector3 startPosition;
    private SpriteRenderer eggSpriteRenderer;
     
    private void Awake()
    {
        startPosition = transform.localPosition;
        eggSpriteRenderer = GetComponent<SpriteRenderer>();
        crackedEggTop.SetActive(false);
        crackedEggBottom.SetActive(false);
    }

    public void ShakeEggAnimation()
    {
        StartCoroutine(ShakeEgg());
    }

    public void HatchEggAnimation()
    {
        StopAllCoroutines();
        StartCoroutine(HatchEgg());
    }

    private IEnumerator ShakeEgg()
    {
        int shakes = 6;

        while (true)
        {
            AudioManager.Instance.PlayEggShakeSFX();

            for (int i = 0; i < shakes; i++)
            {
                // Move right
                yield return MoveEggX(startPosition.x + shakeDistance, shakeSpeed);
                // Move left
                yield return MoveEggX(startPosition.x - shakeDistance, shakeSpeed);
                
            }
            transform.localPosition = startPosition;
            yield return new WaitForSeconds(shakeCooldown);
        }
    }

    private IEnumerator MoveEggX(float targetX, float speed)
    {
        Vector3 position = transform.localPosition;

        while (Mathf.Abs(transform.localPosition.x - targetX) > 0.01f)
        {
            position.x = Mathf.Lerp(position.x, targetX, Time.deltaTime * speed);
            transform.localPosition = position;
            yield return null;
        }
    }

    private IEnumerator HatchEgg()
    {
        AudioManager.Instance.StopSFX();
        AudioManager.Instance.PlayEggCrackSFX();

        transform.localPosition = startPosition;
        // Crack egg animation
        eggSpriteRenderer.sprite = crackedEggSprite;
        yield return new WaitForSeconds(crackedAnimationDuration);
        // Open egg animation
        eggSpriteRenderer.sprite = null;
        crackedEggTop.SetActive(true);
        crackedEggBottom.SetActive(true);
        yield return ShowPet();
    }

    private IEnumerator ShowPet()
    {
        AudioManager.Instance.PlaySlideWhistleUpSFX();

        Vector3 topShellStartingPosition = crackedEggTop.transform.localPosition;
        Vector3 topShellTargetPosition = new(0f,9.98f,0f);
        float duration = 0.5f;
        float elapsedTime = 0;

        crackedEggTop.SetActive(true);
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            crackedEggTop.transform.localPosition = Vector3.Lerp(topShellStartingPosition, topShellTargetPosition, elapsedTime / duration);
            yield return null;
        }  
        crackedEggTop.transform.localPosition = topShellTargetPosition;
        yield return null;
    }
}