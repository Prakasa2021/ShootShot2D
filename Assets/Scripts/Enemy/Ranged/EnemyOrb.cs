using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOrb : MonoBehaviour
{
    [SerializeField] float minEnemyDamage;
    [SerializeField] float maxEnemyDamage;

    void OnCollisionEnter2D(Collision2D other)
    {
        var enemyDamage = Random.Range(minEnemyDamage, maxEnemyDamage);

        if (other.gameObject.CompareTag("Base"))
        {
            other.gameObject.GetComponent<Health>().UpdateHealth(-enemyDamage);
            Destroy(gameObject);
        }
    }
}
