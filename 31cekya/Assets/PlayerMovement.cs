using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float waterGravity = 0.1f;

    private Vector2 moveDirection;
    private Rigidbody2D rb;
    private Animator animator;
    private float idleTime = 0f;
    private float idleThreshold = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rb.gravityScale = waterGravity;
    }

    void Update()
    {
        ProcessInputs();
        UpdateAnimations();
    }

    void FixedUpdate()
    {
        Move();
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;
    }

    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y - waterGravity);

        if (moveDirection.x != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(moveDirection.x), 1, 1);
        }
    }

    void UpdateAnimations()
    {
        if (moveDirection.x != 0 || moveDirection.y != 0)
        {
            animator.SetBool("isWalking", true);
            idleTime = 0f;
        }
        else
        {
            animator.SetBool("isWalking", false);
            idleTime += Time.deltaTime;

            if (idleTime > idleThreshold)
            {
                animator.SetTrigger("Idle2");
                idleTime = 0f;
            }
        }
    }
}
