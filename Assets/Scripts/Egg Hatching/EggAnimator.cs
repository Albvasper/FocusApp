using UnityEngine;
using System.Collections;

public class EggAnimator : MonoBehaviour
{
    [Range(0f, 8f)][SerializeField] private float shakeDistance = 8f;
    [Range(0f, 20f)][SerializeField] private float shakeSpeed = 20f;
    [Range(0f, 5f)][SerializeField] private float shakeCooldown = 1f;
    
    private Vector3 startPosition;

    private void Awake()
    {
        startPosition = transform.localPosition;
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
        int shakes = 4;

        while (true)
        {
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
        Debug.Log("Hatching!");
        transform.localPosition = startPosition;
        yield return null;
    }
}