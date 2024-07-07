using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] public float arrowDamage;
    [SerializeField] public float arrowTotalDamage;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        Destroy(gameObject, 15f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage(arrowTotalDamage);
            Destroy(gameObject);
        }
        else if (other.CompareTag("EnemyRanged"))
        {
            other.gameObject.GetComponent<EnemyRanged>().TakeDamage(arrowTotalDamage);
            Destroy(gameObject);
        }
    }
}
