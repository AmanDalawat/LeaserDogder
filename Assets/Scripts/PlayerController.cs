using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleInputNamespace;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public SimpleJoystick joystick; // Reference to the joystick
    private Rigidbody rb;
    private Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>(); // Reference to the Animator component
    }

    private void Update()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        UpdateAnimations();
    }

    void Move()
    {
        Vector3 direction = joystick.InputDirection;
        Vector3 movement = new Vector3(direction.x, 0, direction.z) * moveSpeed * Time.deltaTime;
        transform.Translate(movement, Space.Self);
    }

    public void Jump()
    {
        if (Mathf.Abs(rb.velocity.y) < 0.01f)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetTrigger("Jump"); // Trigger jump animation
        }
    }

    void UpdateAnimations()
    {
        Vector3 direction = joystick.InputDirection;
        
        if (Mathf.Abs(rb.velocity.y) > 0.01f)
        {
            // Player is jumping
            animator.SetBool("isRunning", false);
            animator.SetBool("isIdle", false);
        }
        else if (direction.magnitude > 0.1f)
        {
            // Player is running
            animator.SetBool("isRunning", true);
            animator.SetBool("isIdle", false);
        }
        else
        {
            // Player is idle
            animator.SetBool("isRunning", false);
            animator.SetBool("isIdle", true);
        }
    }
}
