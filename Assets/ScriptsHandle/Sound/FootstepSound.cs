using UnityEngine;

public class FootstepSound : MonoBehaviour
{
    public AudioClip footstepSound; // Âm thanh bước chân
    public float stepInterval = 0.5f; // Thời gian giữa mỗi âm bước chân
    private AudioSource audioSource;
    private float stepTimer;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        stepTimer = 0f;
    }

    void Update()
    {
        // Kiểm tra nếu nhân vật đang di chuyển
        if (IsMoving())
        {
            stepTimer -= Time.deltaTime;
            if (stepTimer <= 0f)
            {
                PlayFootstep();
                stepTimer = stepInterval;
            }
        }
        else
        {
            stepTimer = 0f; // Reset timer khi dừng di chuyển
        }
    }

    private bool IsMoving()
    {
        // Kiểm tra di chuyển thông qua Input hoặc tốc độ
        return Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0;
    }

    private void PlayFootstep()
    {
        if (footstepSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(footstepSound);
        }
    }
}
