using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] float health = 0f;
    [SerializeField] float maxHealth = 100f;
    [SerializeField] Slider healthBar;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        healthBar.maxValue = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateHealth(float mod)
    {
        health += mod;

        if (health > maxHealth)
        {
            health = maxHealth;
        }
        else if (health <= 0f)
        {
            health = 0f;
            healthBar.value = health;
        }
    }

    void OnGUI()
    {
        float t = Time.deltaTime / 1f;
        healthBar.value = Mathf.Lerp(healthBar.value, health, t);
    }
}
