using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    bool cameraFocused = false; 
    Animator playerAnimator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    void Update()
{
    float horizontal = Input.GetAxisRaw("Horizontal");
    float vertical = Input.GetAxisRaw("Vertical");
    MoveCharacter(horizontal, vertical);

    // Karakterin hızını animasyona set et
    float characterSpeed = Mathf.Abs(horizontal) + Mathf.Abs(vertical); // Yatay ve dikey hızları toplar
    playerAnimator.SetFloat("Speed", characterSpeed);

    if (Input.GetKeyDown(KeyCode.Y))
    {
        ToggleCameraFocus();
    }
}


    void MoveCharacter(float h, float v)
    {
        rb.velocity = new Vector2(h * speed, v * speed);
    }

    void ToggleCameraFocus()
    {
        cameraFocused = !cameraFocused;

        if (cameraFocused)
        {
            Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);
        }
    }
}
