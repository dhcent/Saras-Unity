using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerAttacks : MonoBehaviour
{
    [Header("Fireball instantiation")]
    public Transform firePoint;
    public GameObject fireballPrefab;
    private float firePointOffsetX;
    public InputActionReference mousePositionAction;
    private Vector3 direction;

    [Header("Mana")]
    public float maxMana;
    public float manaRegen;
    public ManaBar manaBarScript;
    private float currentMana;

    [Header("Melee Attack")]
    public Transform meleeAttackPoint;
    private float meleeAttackPointOffsetX;
    public float meleeAttackRange;
    public float meleeDamage;

    private Animator animator;

    public void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Start()
    {
        currentMana = maxMana;
        manaBarScript.SetMaxMana(maxMana);
        firePointOffsetX = firePoint.localPosition.x;
        meleeAttackPointOffsetX = meleeAttackPoint.localPosition.x;
    }
    public void SpecialAttack(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            if(currentMana > 15)
            {
                //Gets screen mouse position
                Vector3 mouseScreenPosition = mousePositionAction.action.ReadValue<Vector2>();
                //Convert mouse position from screen position to world position. 
                //Note: camera z is -10, which is different from 0. This causes normalization to be skewed
                Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(
                    new Vector3(mouseScreenPosition.x, 
                                mouseScreenPosition.y, 
                                -Camera.main.transform.position.z)
                    );
                //Gets the direction we are trying to shoot. .normalized minimizes each direction to 1
                direction = (mouseWorldPosition - firePoint.position).normalized;

                //computes angle based on vector and convert rad to degrees
                float angle = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.Euler(0,0,angle);
                //Creates the fireball game object and creates a reference to the fireball script
                GameObject fireball = Instantiate(fireballPrefab, firePoint.position, rotation);
                Fireball fireballScript = fireball.GetComponent<Fireball>();
                //Sets the direction of the fireball script
                fireballScript.SetDirection(direction);
                currentMana -= 15;
            }   
        }
    }

    public void MeleeAttack(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            animator.SetTrigger("meleeAttack");
            //Creates an array of all things hit by the sword
            Collider2D[] entitiesHit = Physics2D.OverlapCircleAll(meleeAttackPoint.position, meleeAttackRange);
            foreach(Collider2D entity in entitiesHit)
            {
                //If the sword hit an enemy, deal damage to it.
                if(entity.gameObject.tag == "Enemy")
                {
                    Debug.Log("hit enemy");
                    Enemy enemy = entity.GetComponent<Enemy>();
                    enemy.TakeDamage(meleeDamage);
                }
            }

        }
    }
    //Draws gizmos in scene view when selected
    public void OnDrawGizmosSelected()
    {
        if(meleeAttackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(meleeAttackPoint.position, meleeAttackRange);
    }

    public void FixedUpdate()
    {
        if(currentMana < maxMana)
        {
            //updates regen on fixed interval
            currentMana += manaRegen * Time.fixedDeltaTime;
            //clamp to max
            currentMana = Mathf.Min(currentMana, maxMana);
            manaBarScript.SetMana(currentMana);
        }       
    }

    public void Update()
    {
        //If localScale is < 0, then player is facing left, thus move attackpoint accordingly.
        bool isFlipped = GetComponent<SpriteRenderer>().flipX == true;

        //if flipped, make local position negative. If not flipped, make it positive.
        Vector3 fireLocalPos = firePoint.localPosition;
        fireLocalPos.x = Mathf.Abs(firePointOffsetX) * (isFlipped ? -1 : 1);
        firePoint.localPosition = fireLocalPos;

        Vector3 meleeLocalPos = meleeAttackPoint.localPosition;
        meleeLocalPos.x = Mathf.Abs(meleeAttackPointOffsetX) * (isFlipped ? -1 : 1);
        meleeAttackPoint.localPosition = meleeLocalPos;
    }
}


