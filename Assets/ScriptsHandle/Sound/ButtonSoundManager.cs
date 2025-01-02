using UnityEngine;

public class ButtonSoundManager : MonoBehaviour
{
    public AudioClip buttonClickSound; // Âm thanh khi nhấn
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayButtonClickSound()
    {
        if (buttonClickSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(buttonClickSound);
        }
    }
}
