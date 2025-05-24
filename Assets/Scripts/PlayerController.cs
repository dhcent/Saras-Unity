using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Animator animator;
    private Rigidbody2D rb;
    private PlayerMovement playerMovementScript;


    public enum PlayerState {
        Idle,
        Jumping,
        Running
    }

    public PlayerState currentState;

    void Awake()
    {
        animator = GetComponent<Animator>();   
        rb = GetComponent<Rigidbody2D>();
        currentState = PlayerState.Idle;
        playerMovementScript = GetComponent<PlayerMovement>();
    }

    public void UpdateState()
    {
        SelectState();
        switch(currentState)
        {
            case PlayerState.Idle:
                break;
            case PlayerState.Jumping:
                break;
            case PlayerState.Running:
                break;
        }
    }

    public void SelectState()
    {
        if(Mathf.Abs(rb.linearVelocity.y) > 0.02)
        {
            currentState = PlayerState.Jumping;
            animator.SetFloat("yVelocity", rb.linearVelocity.y);
        }
        else if(Mathf.Abs(rb.linearVelocity.x) > 0.02)
        {
            currentState = PlayerState.Running;
        }
        else
        {
            currentState = PlayerState.Idle;
        }
    }


    private void Update()
    {
        FlipPlayer();
        animator.SetFloat("xVelocity", Mathf.Abs(rb.linearVelocity.x));
        animator.SetFloat("yVelocity", rb.linearVelocity.y);
        animator.SetBool("isJumping", !playerMovementScript.IsGrounded());
        if(rb.linearVelocity.y < 0)
        {
            rb.gravityScale = 2.0f;
        }
        else{ 
            rb.gravityScale = 1.0f;
        }
    }

    private void FlipPlayer()
    {
        if(rb.linearVelocity.x < -0.1)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (rb.linearVelocity.x > 0.1)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }
}
