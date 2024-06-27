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
        rb.gravityScale = 0; // Suyun altında olduğumuz için gravity scale'i sıfırlıyoruz.
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
        // Karakterin hareket etmesi ve suyun altında yavaşça aşağı düşmesi
        Vector2 newVelocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed - waterGravity);
        rb.velocity = newVelocity;

        // Karakterin yönünü ayarlamak (sola veya sağa bakması için)
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
