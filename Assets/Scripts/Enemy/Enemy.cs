using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float enemyHealth;
    [SerializeField] float enemyMaxHealth;
    [SerializeField] int enemyDamage;
    [SerializeField] float speed;
    [SerializeField] float attackSpeed;
    [SerializeField] float canAttack;
    [SerializeField] float transparencyDuration = 1f; // Duration of transparency effect in seconds
    [SerializeField] float transparencyAmount = 0.5f; // Transparency amount (0 = fully transparent, 1 = fully opaque)
    SpriteRenderer spriteRenderer;
    Color originalColor; // Original color of the sprite

    // [SerializeField] Projectile projectile;

    void Start()
    {
        enemyHealth = enemyMaxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color; // Store the original color of the sprite
    }

    void Update()
    {
        // Menggerakkan objek ke depan berdasarkan arah hadapannya
        transform.Translate(speed * Time.deltaTime * Vector2.left);
    }

    public void TakeDamage(float dmg)
    {
        enemyHealth -= dmg;

        MakeTransparent();

        if (enemyHealth <= 0)
        {
            enemyHealth = 0;
            Destroy(gameObject);
        }
    }

    // void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.CompareTag("Arrow") && enemyHealth > 0)
    //     {
    //         Destroy(other.gameObject);

    //         enemyHealth -= projectile.arrowDamage;

    //         if (enemyHealth <= 0)
    //         {
    //             Destroy(gameObject);
    //         }
    //     }
    // }

    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Base"))
        {
            if (attackSpeed <= canAttack)
            {
                other.gameObject.GetComponent<Health>().UpdateHealth(-enemyDamage);
                canAttack = 0f;
            }
            else
            {
                canAttack += Time.deltaTime;
            }
        }
    }

    void MakeTransparent()
    {
        // Change the alpha value of the sprite's color to achieve transparency
        Color transparentColor = originalColor;
        transparentColor.a = transparencyAmount;
        spriteRenderer.color = transparentColor;

        // Restore the sprite's transparency after a delay
        Invoke(nameof(RestoreTransparency), transparencyDuration);
    }

    void RestoreTransparency()
    {
        // Restore the original color of the sprite
        spriteRenderer.color = originalColor;
    }
}
