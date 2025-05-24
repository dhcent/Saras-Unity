using System.Collections.Generic;
using UnityEngine;

public class PlayerEnemyDetection : MonoBehaviour
{
    public float detectionRange;
    private LayerMask entityLayer;
    public HashSet<Enemy> enemies;

    void Awake()
    {
        enemies = new HashSet<Enemy>();
        entityLayer = LayerMask.GetMask("Entity");
    }

    void Update()
    {
        // FindEnemies();
    }

    //Only if I want to shift enemy detection to player

    // private void FindEnemies()
    // {
    //     bool foundEnemy;
    //     Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, detectionRange, entityLayer);
    //     foreach(Collider2D hitInfo in collisions)
    //     {
    //         if(hitInfo.CompareTag("Enemy"))
    //         {
    //             Debug.Log("Enemy detected");
    //             Enemy enemyScript = hitInfo.GetComponent<Enemy>();
    //             //If it is within Enemy attack range
    //             float distance = Vector2.Distance(hitInfo.transform.position, transform.position);

    //             if(distance < enemyScript.GetAttackRange())
    //             {
    //                 enemyScript.SetState(Enemy.EnemyState.Attack);
    //             }
    //             else if(distance < enemyScript.GetDetectionRange())
    //             {
    //                 enemyScript.SetState(Enemy.EnemyState.Chase);
    //             }
    //             enemies.Add(enemyScript);
    //             foundEnemy = true;
    //         }
    //     }
    // }

    // public void OnDrawGizmosSelected()
    // {
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawWireSphere(transform.position, detectionRange);
    // }

}
