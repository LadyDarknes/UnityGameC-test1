using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float runSpeed = 8f;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float speed = isRunning ? runSpeed : moveSpeed;

        Vector3 moveDirection = new Vector3(horizontalInput, verticalInput, 0f).normalized;
        transform.position += moveDirection * speed * Time.deltaTime;
        if (horizontalInput != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(horizontalInput), 1, 1);
        }
        animator.SetBool("isWalking", moveDirection != Vector3.zero && !isRunning);
        animator.SetBool("isRunning", moveDirection != Vector3.zero && isRunning);
        animator.SetBool("isIdle", moveDirection == Vector3.zero);
    }
}
