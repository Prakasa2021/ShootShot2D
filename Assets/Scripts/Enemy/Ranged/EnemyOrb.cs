using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOrb : MonoBehaviour
{
    [SerializeField] int enemyDamage;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Base"))
        {
            other.gameObject.GetComponent<Health>().UpdateHealth(-enemyDamage);
            Destroy(gameObject);
        }
    }
}
