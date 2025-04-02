using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 20f;
    public float damage;
    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if(hitInfo.gameObject.tag == "Enemy")
        {
            //Gets the enemy component script from collision
            Enemy enemy = hitInfo.GetComponent<Enemy>();
            enemy.TakeDamage(damage);
        }

        if(hitInfo.name != "Player")
        {
            Destroy(gameObject);
        }
    }

    public void SetDirection(Vector3 direction)
    {
        rb.linearVelocity = direction * speed;
    }
}