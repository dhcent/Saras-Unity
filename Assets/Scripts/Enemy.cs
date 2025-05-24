using System;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.Rendering;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    public float health = 100f;
    public float detectionRange;
    public float attackRange;
    public float moveSpeed;

    [Header("Misc")]
    public Transform playerTransform;
    private Rigidbody2D rb;

    private Animator animator;
    private NPCController NPCControllerScript;
    
    private LayerMask entityLayer;

    public enum EnemyState
    {
        Idle,  
        Attack,
        Chase,
        Dying
    }

    public EnemyState currentState;

    public void Awake()
    {
        currentState = EnemyState.Idle;
        animator = GetComponent<Animator>();
        entityLayer = LayerMask.GetMask("Entity");
        rb = GetComponent<Rigidbody2D>();
    }
    
    public void Start()
    {
        NPCControllerScript = GetComponent<NPCController>();
        if(NPCControllerScript == null)
        {
            Debug.Log("NPC Controller Script not found");
        }
    }

    public float GetDetectionRange()
    {
        return detectionRange;
    } 
    public float GetAttackRange()
    {
        return attackRange;
    } 

    public void SetState(EnemyState state)
    {
        currentState = state;
    }
    public void UpdateState()
    {
        if(isPlayerInDetectionRange())
        {
            Debug.Log("Chase State");
            currentState = EnemyState.Chase;
        }    
        else
        {
            currentState = EnemyState.Idle; 
        }
    }

    public bool isPlayerInDetectionRange()
    {
        Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, detectionRange, entityLayer);
        foreach(Collider2D hitInfo in collisions)
        {
            if(hitInfo.CompareTag("Player"))
            {
                return true;
            }   
        }
        return false;
    }

    public bool isPlayerInAttackRange()
    {
        Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, attackRange, entityLayer);
        foreach(Collider2D hitInfo in collisions)
        {
            Debug.Log("Player Found");
            if(hitInfo.CompareTag("Player"))
            {
                Debug.Log("Player Found");
                return true;
            }
        }
        return false;
    }

    public void Update()
    {
        UpdateState();
        switch(currentState)
        {   
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Chase:
                Chase();
                break;
        }
    }

    public void Idle()
    {
        //Run Idle script
        NPCControllerScript.IdlePathFind();
    }

    public void Chase()
    {
        //If currently to the right of it
        if(transform.position.x - playerTransform.position.x > 0)
        {
            rb.linearVelocity = new Vector2(-moveSpeed, rb.linearVelocity.y);
        }
        else if(transform.position.x - playerTransform.position.x < 0)
        {
            rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);
        }
    }

    public void Attack()
    {
        //attack
    }

    public void Dying()
    {

    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Die();
        }
        else
        {
            animator.SetTrigger("isHit");
        }
    }

    private void Die()
    {     
        animator.SetBool("isDead", true);
        Destroy(gameObject, 3f);
    }

    //displays detection radius
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
