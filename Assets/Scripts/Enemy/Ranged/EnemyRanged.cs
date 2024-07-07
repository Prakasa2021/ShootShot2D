using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanged : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] GameObject bulletPrefab; // The bullet prefab to be spawned
    [SerializeField] Transform spawnPoint; // The point from which the bullet will be spawned
    [SerializeField] float bulletSpeed; // The speed at which the bullet will be shot
    [SerializeField] float enemyHealth;
    [SerializeField] float enemyMaxHealth;
    [SerializeField] float walkTimeBegin;
    [SerializeField] float walkTimeTarget; // Target time
    [SerializeField] float speed;
    [SerializeField] float attackSpeed;
    [SerializeField] float canAttack;
    [SerializeField] float transparencyDuration = 1f; // Duration of transparency effect in seconds
    [SerializeField] float transparencyAmount = 0.5f; // Transparency amount (0 = fully transparent, 1 = fully opaque)
    [SerializeField] int enemyValue;
    SpriteRenderer spriteRenderer;
    Color originalColor; // Original color of the sprite
    private GameManager gameManager;
    private float walkTime;
    private float randomTime;

    void Start()
    {
        enemyHealth = enemyMaxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color; // Store the original color of the sprite
        gameManager = GameManager.instance;
        randomTime = Random.Range(walkTimeBegin, walkTimeTarget);
    }

    void Update()
    {
        // Menggerakkan objek ke depan berdasarkan arah hadapannya
        Transform targetTransform = transform.parent ? transform.parent : transform;
        walkTime += Time.deltaTime;

        if (walkTime < randomTime)
        {
            PlayWalkAnimation();

            targetTransform.Translate(speed * Time.deltaTime * Vector2.left);
        }
        else
        {
            StopWalkAnimation();

            if (attackSpeed <= canAttack)
            {
                PlayAttackAnimation();

                SpawnBullet();

                canAttack = 0f;
            }
            else
            {
                canAttack += Time.deltaTime;
            }
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

    // void PlayIdleAnimation()
    // {
    //     // Set the appropriate parameters in the Animator to trigger the idle animation
    //     animator.SetTrigger("idle");
    // }

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

    void SpawnBullet()
    {
        // Instantiate the bullet at the spawn point's position and rotation
        GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);

        // Add velocity to the bullet if it has a Rigidbody component
        if (bullet.TryGetComponent<Rigidbody2D>(out var rb))
        {
            rb.velocity = Vector2.left * bulletSpeed;
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
