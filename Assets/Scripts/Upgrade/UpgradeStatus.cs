using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeStatus : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] Launcher launcher;
    [SerializeField] Health health;

    [Header("Damage Upgrade")]
    [SerializeField] int damageLevel;
    [SerializeField] float damageUp;
    [SerializeField] int costDamageUp;
    [SerializeField] Button atkDamageButton;
    [SerializeField] TMP_Text costDamageText;
    [SerializeField] int addCostDamage;

    [Header("Cooldown Upgrade")]
    [SerializeField] int cooldownLevel;
    [SerializeField] float cooldownUp;
    [SerializeField] int costCooldownUp;
    [SerializeField] Button cdSpeedButton;
    [SerializeField] TMP_Text costCooldownText;
    [SerializeField] int addCostCooldown;

    [Header("Charge Speed Upgrade")]
    [SerializeField] int chargeLevel;
    [SerializeField] float chargeSpeedUp;
    [SerializeField] int costChargeSpeedUp;
    [SerializeField] Button chargeSpeedButton;
    [SerializeField] TMP_Text costChargeSpeedText;
    [SerializeField] int addCostChargeSpeed;

    [Header("Health Upgrade")]
    [SerializeField] int healthLevel;
    [SerializeField] float maxHealthUp;
    [SerializeField] int costMaxHealthUp;
    [SerializeField] Button maxHealthButton;
    [SerializeField] TMP_Text costMaxHealthText;
    [SerializeField] int addCostMaxHealth;

    void Start()
    {
        gameManager = GameManager.instance;
        costDamageText.text = costDamageUp.ToString();
        costCooldownText.text = costCooldownUp.ToString();
        costChargeSpeedText.text = costChargeSpeedUp.ToString();
        costMaxHealthText.text = costMaxHealthUp.ToString();
    }

    void Update()
    {
        // Butuh script simple interact button
        if (gameManager.gemsCount >= costDamageUp && damageLevel <= 10)
            atkDamageButton.interactable = true;
        else
            atkDamageButton.interactable = false;

        if (gameManager.gemsCount >= costCooldownUp && cooldownLevel <= 10)
            cdSpeedButton.interactable = true;
        else
            cdSpeedButton.interactable = false;

        if (gameManager.gemsCount >= costChargeSpeedUp && chargeLevel <= 10)
            chargeSpeedButton.interactable = true;
        else
            chargeSpeedButton.interactable = false;

        if (gameManager.gemsCount >= costMaxHealthUp && healthLevel <= 10)
            maxHealthButton.interactable = true;
        else
            maxHealthButton.interactable = false;
    }

    public void UpgradeAttackDamage()
    {
        var currentGems = gameManager.gemsCount;

        if (currentGems >= costDamageUp)
        {
            gameManager.GemsCount(-costDamageUp);
            launcher.arrowDamageUpgrade += damageUp;
            costDamageUp += addCostDamage;
            costDamageText.text = costDamageUp.ToString();
        }
        damageLevel += 1;
    }

    public void UpgradeCooldownSpeed()
    {
        var currentGems = gameManager.gemsCount;

        if (currentGems >= costCooldownUp)
        {
            gameManager.GemsCount(-costCooldownUp);
            launcher.fireRateSpeed += cooldownUp;
            costCooldownUp += addCostCooldown;
            costCooldownText.text = costCooldownUp.ToString();
        }
        cooldownLevel += 1;
    }

    public void UpgradeChargeSpeed()
    {
        var currentGems = gameManager.gemsCount;

        if (currentGems >= costChargeSpeedUp)
        {
            gameManager.GemsCount(-costChargeSpeedUp);
            launcher.chargeSpeed += chargeSpeedUp;
            costChargeSpeedUp += addCostChargeSpeed;
            costChargeSpeedText.text = costChargeSpeedUp.ToString();
        }
        chargeLevel += 1;
    }

    public void UpgradeMaxHealth()
    {
        var currentGems = gameManager.gemsCount;

        if (currentGems >= costMaxHealthUp)
        {
            gameManager.GemsCount(-costMaxHealthUp);
            health.maxHealth += maxHealthUp;
            costMaxHealthUp += addCostMaxHealth;
            costMaxHealthText.text = costMaxHealthUp.ToString();
            health.healthBar.maxValue = health.maxHealth;
        }
        healthLevel += 1;
    }
}
