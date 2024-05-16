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

    void Start()
    {
        gameManager = GameManager.instance;
        costDamageText.text = costDamageUp.ToString();
    }

    void Update()
    {
        if (gameManager.gemsCount < costDamageUp)
            atkDamageButton.interactable = false;
        else
            atkDamageButton.interactable = true;
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
}
