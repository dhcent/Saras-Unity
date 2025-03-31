using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float x_speed;
    private float x_movement;
    private Rigidbody2D rb;

    [Header("Jumping")]
    public float jumpForce;
    public int maxJumps;
    private int jumpsRemaining;

    [Header("Ground Check")]
    public Transform groundCheckPos;
    public Vector2 groundCheckSize = new Vector2(0.5f, 0.5f);
    public LayerMask groundLayer;

    private Animator animator;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnMovement(InputValue value)
    {

        x_movement = value.Get<float>();
    }

    public void OnJump()
    {
        if(jumpsRemaining > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpsRemaining--;
        }
    }
    //if key not pressed, vel = 0
    //if key pressed, vel = certain number
    void FixedUpdate()
    {
        isGrounded();
        if(!isXMovementKeyPressed())
        {
            x_movement = 0f;
        }
        rb.linearVelocity = new Vector2(x_movement * x_speed, rb.linearVelocity.y);
        animator.SetFloat("xVelocity", Mathf.Abs(rb.linearVelocity.x));
        animator.SetFloat("yVelocity", rb.linearVelocity.y);
        animator.SetBool("isJumping", jumpsRemaining != 2);
        FlipPlayer();
    }

    private bool isXMovementKeyPressed()
    {
        return  Keyboard.current.aKey.isPressed || 
                Keyboard.current.dKey.isPressed;
    }

    private void isGrounded()
    {
        //Check if drawn Gizmos (under player feet) is overlapping with ground object
        bool grounded = Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer);
        if(grounded && rb.linearVelocity.y < 0)
        {
            jumpsRemaining = maxJumps;
        }
    }

    private void FlipPlayer()
    {
        if(rb.linearVelocity.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (rb.linearVelocity.x > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    //Draws Gizmos box
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize);
    }
}