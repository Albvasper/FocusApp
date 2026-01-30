using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioClip clickSFX;
    [SerializeField] private AudioClip successSFX;
    [SerializeField] private AudioClip gameOverSFX;
    [SerializeField] private AudioClip eggCrackSFX;
    [SerializeField] private AudioClip eggShakeSFX;
    [SerializeField] private AudioClip slideWhistleSFX;

    private void Awake()
    {
        // Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void PlayClickSFX()
    {
        sfxSource.PlayOneShot(clickSFX);
    }
    
    public void PlaySuccessSFX()
    {
        sfxSource.PlayOneShot(successSFX);
    }

    public void PlayGameOverSFX()
    {
        sfxSource.PlayOneShot(gameOverSFX);
    }

    public void PlayEggCrackSFX()
    {
        sfxSource.PlayOneShot(eggCrackSFX);
    }

    public void PlayEggShakeSFX()
    {
        sfxSource.PlayOneShot(eggShakeSFX);
    }

    public void PlaySlideWhistleUpSFX()
    {
        sfxSource.PlayOneShot(slideWhistleSFX);
    }

    public void StopSFX()
    {
        sfxSource.Stop();
    }
}
