using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 20f;
    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Debug.Log(hitInfo.name);
        if(hitInfo.name != "Player")
        {
            Destroy(gameObject);
        }
    }

}
