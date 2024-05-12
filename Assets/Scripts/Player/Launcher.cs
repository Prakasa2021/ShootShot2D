using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Launcher : MonoBehaviour
{
    [SerializeField] Transform projectilePrefab;
    [SerializeField] Transform spawnPoint;
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] SpriteRenderer arrowGFX;
    [SerializeField] Slider cooldownFire;
    [SerializeField] float launchForce = 1.5f;
    [SerializeField] float trajectoryTimeStep = 0.05f;
    [SerializeField] int trajectoryStepCount = 15;
    [SerializeField] float fireRate = 3f;
    [SerializeField] float nextFire;
    [Range(0, 5)][SerializeField] float maxBowCharge;
    Vector2 velocity, startMousePos, currentMousePos;
    float bowCharge;
    bool isCharge = true;

    void Start()
    {
        cooldownFire.value = 0f;
        cooldownFire.maxValue = fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 screenPosDepth = Input.mousePosition;
        // Give it a depth. Maybe a raycast depth, maybe a clipping plane...
        screenPosDepth.z = 10f;

        if (Input.GetMouseButtonDown(0))
        {
            startMousePos = Camera.main.ScreenToWorldPoint(screenPosDepth);
        }
        else if (Input.GetMouseButton(0))
        {
            if (isCharge)
                ChargeBow();

            arrowGFX.enabled = true;
            currentMousePos = Camera.main.ScreenToWorldPoint(screenPosDepth);
            velocity = (startMousePos - currentMousePos) * launchForce;

            RotateLauncher();
            DrawTrajectory();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            ClearTrajectory();
        }

        if (!isCharge)
        {
            if (bowCharge > 0f)
            {
                bowCharge -= 1f * Time.deltaTime;
            }
            else
            {
                bowCharge = 0f;
                isCharge = true;
            }
        }

        if (Time.time > nextFire)
        {
            if (Input.GetMouseButtonUp(0))
            {
                isCharge = false;
                nextFire = Time.time + fireRate;
                FireProjectile();
            }
        }

        if (cooldownFire.value > 0f)
        {
            cooldownFire.value -= 1f * Time.deltaTime;

            if (cooldownFire.value <= 0f)
                cooldownFire.value = 0f;
        }
    }

    void DrawTrajectory()
    {
        Vector3[] positions = new Vector3[trajectoryStepCount];

        for (int i = 0; i < trajectoryStepCount; i++)
        {
            float t = i * trajectoryTimeStep;
            Vector3 pos = (Vector2)spawnPoint.position + velocity * t + 0.5f * t * t * Physics2D.gravity;

            positions[i] = pos;
        }

        lineRenderer.positionCount = trajectoryStepCount;
        lineRenderer.SetPositions(positions);
    }

    void ChargeBow()
    {
        bowCharge += Time.deltaTime;
    }

    void RotateLauncher()
    {
        float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void FireProjectile()
    {
        arrowGFX.enabled = false;
        cooldownFire.value = fireRate;

        if (bowCharge > maxBowCharge)
        {
            bowCharge = maxBowCharge;
        }

        Transform pr = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);
        pr.GetComponent<Rigidbody2D>().velocity = velocity;
        Projectile projectileDamage = pr.GetComponent<Projectile>();

        float projectileTotalDamage = bowCharge * projectileDamage.arrowDamage;
        projectileDamage.arrowTotalDamage = projectileTotalDamage;
    }

    void ClearTrajectory()
    {
        lineRenderer.positionCount = 0;
    }
}
