using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerAttacks : MonoBehaviour
{
    public Transform firePoint;
    public GameObject fireballPrefab;
    public InputActionReference mousePositionAction;
    public float fireSpeed;

    private Vector3 direction;
    public void OnSpecialAttack()
    {
        //Gets mouse position
        Vector3 mousePosition = mousePositionAction.action.ReadValue<Vector2>();
        //Gets the direction we are trying to shoot. .normalized minimizes each direction to 1
        direction = (mousePosition - firePoint.position).normalized;
        // if(GetComponent<SpriteRenderer>().flipX == true)
        // {
        //     firePoint.rotation = Quaternion.Euler(0f, 0f, 180f);
        // }
        // else
        // {
        //     firePoint.rotation = Quaternion.Euler(0f, 0f, 0f);
        // }
        //Creates the fireball game object and creates a reference to the fireball script
        GameObject fireball = Instantiate(fireballPrefab, firePoint.position, firePoint.rotation);
        Fireball fireballScript = fireball.GetComponent<Fireball>();
        //Sets the direction of the fireball script
        fireballScript.SetDirection(direction);
    }

}
