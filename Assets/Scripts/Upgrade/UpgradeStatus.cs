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
    [SerializeField] float damageUp;
    [SerializeField] int costDamageUp;
    [SerializeField] Button atkDamageButton;
    [SerializeField] TMP_Text costDamageText;
    [SerializeField] int addCostDamage;

    [Header("Cooldown Upgrade")]
    [SerializeField] float cooldownUp;
    [SerializeField] int costCooldownUp;
    [SerializeField] Button cdSpeedButton;
    [SerializeField] TMP_Text costCooldownText;
    [SerializeField] int addCostCooldown;

    [Header("Charge Speed Upgrade")]
    [SerializeField] float chargeSpeedUp;
    [SerializeField] int costChargeSpeedUp;
    [SerializeField] Button chargeSpeedButton;
    [SerializeField] TMP_Text costChargeSpeedText;
    [SerializeField] int addCostChargeSpeed;

    [Header("Charge Speed Upgrade")]
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
        // Butuh efisiensi interact button
        if (gameManager.gemsCount < costDamageUp)
            atkDamageButton.interactable = false;
        else
            atkDamageButton.interactable = true;

        if (gameManager.gemsCount < costCooldownUp)
            cdSpeedButton.interactable = false;
        else
            cdSpeedButton.interactable = true;

        if (gameManager.gemsCount < costChargeSpeedUp)
            chargeSpeedButton.interactable = false;
        else
            chargeSpeedButton.interactable = true;

        if (gameManager.gemsCount < costMaxHealthUp)
            maxHealthButton.interactable = false;
        else
            maxHealthButton.interactable = true;
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
    }
}
