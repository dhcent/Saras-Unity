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
        Vector3 mouseScreenPosition = mousePositionAction.action.ReadValue<Vector2>();
        //Convert mouse position from screen position to world position. 
        //Note: camera z is -10, which is different from 0. This causes normalization to be skewed
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(
            new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, -Camera.main.transform.position.z)
            );
        //Gets the direction we are trying to shoot. .normalized minimizes each direction to 1
        direction = (mouseWorldPosition - firePoint.position).normalized;
        //Creates the fireball game object and creates a reference to the fireball script
        GameObject fireball = Instantiate(fireballPrefab, firePoint.position, Quaternion.Euler(direction));//firePoint.rotation);
        Fireball fireballScript = fireball.GetComponent<Fireball>();
        //Sets the direction of the fireball script
        fireballScript.SetDirection(direction);
    }

}
