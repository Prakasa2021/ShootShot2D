using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] float enemyHealth;
    [SerializeField] float enemyMaxHealth;
    [SerializeField] float minEnemyDamage;
    [SerializeField] float maxEnemyDamage;
    [SerializeField] float speed;
    [SerializeField] float attackSpeed;
    [SerializeField] float canAttack;
    [SerializeField] float transparencyDuration = 1f; // Duration of transparency effect in seconds
    [SerializeField] float transparencyAmount = 0.5f; // Transparency amount (0 = fully transparent, 1 = fully opaque)
    [SerializeField] int enemyValue;
    Color originalColor; // Original color of the sprite
    private GameManager gameManager;
    private bool isAttacking;
    // [SerializeField] Projectile projectile;

    void Start()
    {
        enemyHealth = enemyMaxHealth;
        originalColor = spriteRenderer.color; // Store the original color of the sprite
        gameManager = GameManager.instance;
    }

    void Update()
    {
        if (animator != null && !isAttacking)
        {
            PlayWalkAnimation();
            Movement();
        }
        else if (animator == null && !isAttacking)
        {
            Movement();
        }
    }

    void Movement()
    {
        // Menggerakkan objek ke depan berdasarkan arah hadapannya
        if (!transform.parent)
            transform.Translate(speed * Time.deltaTime * Vector2.left);
        else
            transform.parent.Translate(speed * Time.deltaTime * Vector2.left);
    }

    public void TakeDamage(float dmg)
    {
        enemyHealth -= dmg;

        MakeTransparent();

        if (enemyHealth <= 0)
        {
            gameManager.GemsCount(enemyValue);
            enemyHealth = 0;

            Destroy(gameObject);

            if (transform.parent)
                Destroy(transform.parent.gameObject);
        }
    }

    void PlayWalkAnimation()
    {
        animator.SetBool("isMoving", true);
    }

    void StopWalkAnimation()
    {
        animator.SetBool("isMoving", false);
    }

    void PlayAttackAnimation()
    {

        // Set the appropriate parameters in the Animator to trigger the attack animation
        animator.SetTrigger("attack");
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
        var enemyDamage = Random.Range(minEnemyDamage, maxEnemyDamage);

        if (other.gameObject.CompareTag("Base"))
        {
            isAttacking = true;

            if (animator != null)
            {
                StopWalkAnimation();
            }

            if (attackSpeed <= canAttack)
            {
                if (animator != null)
                {
                    PlayAttackAnimation();
                }
                other.gameObject.GetComponent<Health>().UpdateHealth(-enemyDamage);
                canAttack = 0f;
            }
            else
            {
                Movement();
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
