using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] public float maxHealth;
    [SerializeField] public Slider healthBar;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        healthBar.maxValue = maxHealth;
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
            // healthBar.value = health;
        }
    }

    void OnGUI()
    {
        healthBar.value = Mathf.Lerp(healthBar.value, health, Time.deltaTime / 1f);
    }
}
