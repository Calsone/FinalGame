using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        

        if(collision.gameObject.TryGetComponent<EnemyBehavior>(out EnemyBehavior enemyComponent))
        {
            enemyComponent.TakeDamage(1);
        }

        Destroy(gameObject);
    }
}
