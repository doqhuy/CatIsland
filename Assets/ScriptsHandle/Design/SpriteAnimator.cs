using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteAnimator : MonoBehaviour
{
	private Image thisImage;
	public Sprite[] spriteFrames;
	public float frameDelay = 0.5f; // Set the delay in seconds

	private int frameIndex = 0;
	private float timer = 0f;

	void Start()
	{
		thisImage = GetComponent<Image>();
	}

	void Update()
	{
		// Check if the array is not empty
		if (spriteFrames.Length > 0)
		{
			// Accumulate time
			timer += Time.deltaTime;

			// Check if the accumulated time exceeds the frame delay
			if (timer >= frameDelay)
			{
				// Assign the current sprite to the Image component
				thisImage.sprite = spriteFrames[frameIndex];

				// Increment frameIndex and loop back to 0 if it exceeds the array length
				frameIndex = (frameIndex + 1) % spriteFrames.Length;

				// Reset the timer
				timer = 0f;
			}
		}
		else
		{
			Debug.LogWarning("Không có sprite nào được gán cho tập lệnh!");
		}
	}
}
