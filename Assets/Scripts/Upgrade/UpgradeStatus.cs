using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeStatus : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] Launcher launcher;

    [Header("Damage Upgrade")]
    [SerializeField] float damageUp;
    [SerializeField] int costDamageUp;
    [SerializeField] Button atkDamageButton;
    [SerializeField] TMP_Text costDamageText;

    [Header("Cooldown Upgrade")]
    [SerializeField] float cooldownUp;
    [SerializeField] int costCooldownUp;
    [SerializeField] Button cdSpeedButton;
    [SerializeField] TMP_Text costCooldownText;

    void Start()
    {
        gameManager = GameManager.instance;
        costDamageText.text = costDamageUp.ToString();
        costCooldownText.text = costCooldownUp.ToString();
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
    }

    public void UpgradeAttackDamage()
    {
        var currentGems = gameManager.gemsCount;

        if (currentGems >= costDamageUp)
        {
            gameManager.GemsCount(-costDamageUp);
            launcher.arrowDamageUpgrade += damageUp;
            costDamageUp += 30;
            costDamageText.text = costDamageUp.ToString();
        }
    }

    public void UpgradeCooldownSpeed()
    {
        var currentGems = gameManager.gemsCount;

        if (currentGems >= costCooldownUp)
        {
            gameManager.GemsCount(-costCooldownUp);
            launcher.fireRate -= cooldownUp;
            costCooldownUp += 30;
            costCooldownText.text = costCooldownUp.ToString();
            launcher.cooldownFire.maxValue = launcher.fireRate;
        }
    }
}
