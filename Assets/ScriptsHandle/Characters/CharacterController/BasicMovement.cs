using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
	public float moveSpeed = 5.0f;

	void Update()
	{
		// Lấy giá trị ngang và đứng từ bàn phím (hoặc thiết bị cảm ứng) để di chuyển nhân vật.
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		// Tạo vector di chuyển dựa trên các giá trị ngang và đứng.
		Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0);

		// Normalized để đảm bảo di chuyển đường chéo có cùng tốc độ với di chuyển theo trục x và y.
		movement.Normalize();

		// Di chuyển nhân vật.
		transform.Translate(movement * moveSpeed * Time.deltaTime);
	}
}
