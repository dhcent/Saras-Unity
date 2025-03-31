using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    public Transform firePoint;
    public GameObject fireballPrefab;

    public void OnSpecialAttack()
    {
        if(GetComponent<SpriteRenderer>().flipX == true)
        {
            firePoint.rotation = Quaternion.Euler(0f, 0f, 180f);
        }
        else
        {
            firePoint.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        Instantiate(fireballPrefab, firePoint.position, firePoint.rotation);
    }
}
