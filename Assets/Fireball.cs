using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 20f;
    public float damage;
    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
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
}