using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] Transform weaponHandler;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RotateWeapon();
    }

    void RotateWeapon()
    {
        float angle = Utility.AngleTowardsMouse(weaponHandler.position);
        weaponHandler.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

        // if (Input.GetTouch(0).phase == TouchPhase.Moved)
        // {
        //     float angle = Utility.AngleTowardsTouch(weaponHandler.position);
        //     weaponHandler.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        // }
    }

}
