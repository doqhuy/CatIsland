using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactMovement : MonoBehaviour
{
    GeneralInformation GeneralInformation = GeneralInformation.Instance;

    public float moveSpeed = 5.0f;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator animator;
    private bool isMoving = false;

    private bool isOnBoat = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.speed = 1f;
        animator.SetFloat("X", 0);
        animator.SetFloat("Y", -1);
    }

    void Update()
    {
        if (GeneralInformation.Actioning == "Playing") Move();
        else
        {
            rb.velocity = Vector2.zero;
            animator.speed = 0;
        }

    }

    void Move()
    {
        // Lấy giá trị ngang và đứng từ bàn phím (hoặc thiết bị cảm ứng) để điều khiển di chuyển nhân vật.
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Tạo vector di chuyển dựa trên các giá trị ngang và đứng.
        movement = new Vector2(moveHorizontal, moveVertical).normalized;

        isMoving = movement.x != 0 || movement.y != 0;

        if (isMoving)
        {
            animator.speed = 1f;
            animator.SetFloat("X", movement.x);
            animator.SetFloat("Y", movement.y);
        }
        else
        {
            // Dừng Animator khi nhân vật đứng yên
            animator.speed = 0;
        }
        rb.velocity = movement * moveSpeed;
    }
    // Thiết lập trạng thái người chơi lên hoặc xuống thuyền
    public void SetOnBoat(bool onBoat)
    {
        isOnBoat = onBoat;
    }

    // Hàm kiểm tra xem người chơi có thể rời thuyền khi đến đích không
    public bool IsOnBoat()
    {
        return isOnBoat;
    }

}
