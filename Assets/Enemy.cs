using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 100f;
    private Animator animator;

    public void Awake()
    {
        animator = GetComponent<Animator>();
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
}
