using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] public float health;
    [SerializeField] public float maxHealth;
    [SerializeField] public Slider healthBar;
    public UnityEvent OnDie;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        healthBar.maxValue = maxHealth;
    }

    void Update()
    {
        healthBar.value = Mathf.Lerp(healthBar.value, health, Time.deltaTime / 0.5f);
    }

    public void UpdateHealth(float mod)
    {
        health += mod;

        if (health <= 0f)
        {
            health = 0f;
            OnDie?.Invoke();
            // healthBar.value = health;
        }
    }
}
