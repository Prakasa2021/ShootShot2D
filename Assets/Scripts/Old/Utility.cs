using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
    public static float AngleTowardsMouse(Vector3 pos)
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 5.23f;

        Vector3 objectPos = Camera.main.WorldToScreenPoint(pos);
        mousePos.x -= objectPos.x;
        mousePos.y -= objectPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        return angle;
    }

    // public static float AngleTowardsTouch(Vector3 pos)
    // {
    //     if (Input.touchCount > 0)
    //     {
    //         // Get the first touch position
    //         Vector3 touchPos = Input.GetTouch(0).position;
    //         touchPos.z = 5.23f;

    //         // Convert touch position to world space
    //         Vector3 objectPos = Camera.main.WorldToScreenPoint(pos);
    //         touchPos.x -= objectPos.x;
    //         touchPos.y -= objectPos.y;

    //         // Calculate the angle towards the touch position
    //         float angle = Mathf.Atan2(touchPos.y, touchPos.x) * Mathf.Rad2Deg;

    //         return angle;
    //     }
    //     else
    //     {
    //         // Return a default angle if there are no touches
    //         return 0f;
    //     }
    // }
}
