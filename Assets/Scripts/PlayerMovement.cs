using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float x_speed;
    private float x_movement;
    private Rigidbody2D rb;

    private Animator animator;

    [Header("Ground Check")]
    public Transform groundCheckPos;
    public Vector2 groundCheckSize = new Vector2(0.5f, 0.5f);
    public LayerMask groundLayer;

    [Header("Jump")]
    public float jumpForce;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void OnMovement(InputValue value)
    {
        x_movement = value.Get<float>();
    }

    public void OnJump()
    {
        if(isGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            animator.SetBool("isJumping", !isGrounded());
        }
    }

    void FixedUpdate()
    {
        print(isGrounded());
        //If you are pressing a movement key, you should be moving, otherwise velocity should be 0.
        animator.SetFloat("xVelocity", Mathf.Abs(rb.linearVelocity.x));
        animator.SetFloat("yVelocity", rb.linearVelocity.y);

        if(!isXMovementKeyPressed())
        {
            x_movement = 0;
        }
        rb.linearVelocity = new Vector2(x_movement * x_speed, rb.linearVelocity.y);
    }

    private bool isXMovementKeyPressed()
    {
        return Keyboard.current.aKey.isPressed || Keyboard.current.dKey.isPressed;
    }

    private bool isGrounded()
    {
        if(Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer))
        {
            return true;
        }
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize);
    }
}
