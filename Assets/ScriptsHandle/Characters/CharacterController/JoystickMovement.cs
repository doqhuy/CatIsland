using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class JoystickMovement : MonoBehaviour
{
    Vector2 movevector;
    public float movespeed = 5f;
    private Animator animator;
    private bool isMoving = false;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.speed = 1f;
        animator.SetFloat("X", 0);
        animator.SetFloat("Y", -1);
    }
    public void InputPlayer(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            movevector = context.ReadValue<Vector2>();
        }
        // Kiểm tra nếu input được huỷ (cancelled)
        else if (context.canceled)
        {
            movevector = Vector2.zero;
        }
    }

    private void Update()
    {
        if (GeneralInformation.Instance.Actioning == "Playing") Move();
        else
        {
            rb.velocity = Vector2.zero;
            animator.speed = 0;
        }
    }

    private void Move()
    {
        movevector = new Vector2(movevector.x, movevector.y).normalized;
        isMoving = movevector.x != 0 || movevector.y != 0;
        if (isMoving)
        {
            animator.speed = 1f;
            animator.SetFloat("X", movevector.x);
            animator.SetFloat("Y", movevector.y);
        }
        else
        {
            // Dừng Animator khi nhân vật đứng yên
            animator.speed = 0;
        }

        rb.velocity = movespeed * movevector;
    }
}
