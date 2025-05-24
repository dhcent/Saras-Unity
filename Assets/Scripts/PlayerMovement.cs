using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float x_MovementForce;
    public float x_max_speed;
    private float x_direction;
    private Rigidbody2D rb;
    public float kFriction;


    [Header("Jumping")]
    public float jumpForce;
    public int maxJumps;
    private int jumpsRemaining;

    [Header("Ground Check")]
    public Transform groundCheckPos;
    public Vector2 groundCheckSize = new Vector2(2f, 0.5f);
    public LayerMask groundLayer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.AddForce(new Vector2(x_direction * x_MovementForce, 0));
        if(Mathf.Abs(rb.linearVelocity.x) > x_max_speed)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x > 0 ? x_max_speed : -x_max_speed, rb.linearVelocity.y);
        }
    
        ApplyFriction();
    }

    public void Movement(InputAction.CallbackContext context)
    {
        x_direction = context.ReadValue<float>();
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            if(jumpsRemaining > 0)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                jumpsRemaining--;
            }   
        }
    }

    private void ApplyFriction()
    {
        if (Mathf.Abs(rb.linearVelocity.x) > 0.05f)
        {
            if (rb.linearVelocity.x > 0)
            {
                rb.AddForce(Vector2.left * kFriction);
            }
            else if (rb.linearVelocity.x < 0)
            {
                rb.AddForce(Vector2.right * kFriction);
            }
        }
        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y); // Directly set velocity to zero within the dead zone
        }
    }

    public bool IsGrounded()
    {
        //Check if drawn Gizmos (under player feet) is overlapping with ground object
        bool grounded = Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer);
        if(grounded && rb.linearVelocity.y < 0)
        {
            jumpsRemaining = maxJumps;
        }
        return grounded;
    }

    //Draws Gizmos box
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize);
    }
}
