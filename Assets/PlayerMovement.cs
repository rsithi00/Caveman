using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;           // Reference to sprite's rigidbody made once during startup
    private Animator animator;          // Reference to sprite's animator

    // Configurable movement parameters
    [SerializeField] private float speed = 2f;
    [SerializeField] private float jumpingPower = 200f;


    private float moveAmount;
    private bool isFacingRight = true;

    // stuff to prevent unlimiting jumping
    public LayerMask ground;        // What counts as ground?
    public Transform groundCheck;   // Location of player feet, where to check for ground
    private int jumpCount = 0;
    [SerializeField] private int maxExtraJumps = 1;  // Determines how many times you can air jump

    // Do initialization here
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        // Move the physics body by the amount specified left/right.  Leave the y velocity at whatever it already is
        body.velocity = new Vector2(moveAmount * speed, body.velocity.y);

        // Check to see if we need to flip the sprite left/right
        // Flip if we're facing right but moving left or facing left but moving right
        if (isFacingRight && moveAmount < 0f || !isFacingRight && moveAmount > 0f) Flip();

        // Reset our jumpCount if we're on the ground
        if (OnGround()) jumpCount = 0;

        // Adjust the animator jumping variable 
        animator.SetBool("OnGround", OnGround());

        if (Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetBool("IsRunning", true);
        }
        else
        {
            animator.SetBool("IsRunning", false);
        }

    }

    // Flips the sprite along the x-axis and updates current facing
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1f,
                                           transform.localScale.y,
                                           transform.localScale.z);
    }

    // Returns true if the player can jump
    // (if we haven't reached our maximum jump amount or if we're on the ground)
    private bool CanJump()
    {
        return (jumpCount < maxExtraJumps) || OnGround();
    }

    // Returns true if the circle at point "groundCheck" is touching an object with the Ground tag
    private bool OnGround()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, ground);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && CanJump())
        {
            body.AddForce(Vector3.up * jumpingPower);
            jumpCount++;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (animator.GetBool("IsStunned"))
            moveAmount = 0f;
        else if (animator.GetBool("IsRunning"))
        {

            moveAmount = context.ReadValue<float>() * 2;
            animator.SetFloat("Speed", Mathf.Abs(moveAmount));
        }
        else
        {

            moveAmount = context.ReadValue<float>();

            // Set the animator variable for movement
            animator.SetFloat("Speed", Mathf.Abs(moveAmount));
        }

    }
}
