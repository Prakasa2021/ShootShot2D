using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] GameObject arrowPrefab;
    [SerializeField] SpriteRenderer arrowGFX;
    [SerializeField] Slider bowPowerSlider;
    [SerializeField] Transform bow;

    [Range(0, 10)]
    [SerializeField] float bowPower;

    [Range(0, 3)]
    [SerializeField] float maxBowCharge;

    float bowCharge;
    bool canFire = true;

    void Start()
    {
        bowPowerSlider.value = 0f;
        bowPowerSlider.maxValue = maxBowCharge;
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && canFire)
        {
            ChargeBow();
        }
        else if (Input.GetMouseButtonUp(0) && canFire)
        {
            FireBow();
        }
        else
        {
            if (bowCharge > 0f)
            {
                bowCharge -= 1f * Time.deltaTime;
            }
            else
            {
                bowCharge = 0f;
                canFire = true;
            }

            bowPowerSlider.value = bowCharge;
        }
    }

    void ChargeBow()
    {
        arrowGFX.enabled = true;
        bowCharge += Time.deltaTime;

        bowPowerSlider.value = bowCharge;

        if (bowCharge > maxBowCharge)
        {
            bowPowerSlider.value = maxBowCharge;
        }
    }

    void FireBow()
    {
        if (bowCharge > maxBowCharge)
        {
            bowCharge = maxBowCharge;
        }

        float arrowSpeed = bowCharge + bowPower;
        float angle = Utility.AngleTowardsMouse(bow.position);

        Quaternion rot = Quaternion.Euler(new Vector3(0f, 0f, angle - 90f));
        Arrow arrow = Instantiate(arrowPrefab, bow.position, rot).GetComponent<Arrow>();
        arrow.arrowVelocity = arrowSpeed;

        canFire = false;
        arrowGFX.enabled = false;
    }
}
