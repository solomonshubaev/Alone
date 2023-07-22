using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HelperUtilities
{

    public static Camera mainCamera;
    
    public static Vector3 GetMouseWorldPosition()
    {
        if (mainCamera == null) mainCamera = Camera.main;

        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.x = Mathf.Clamp(mouseScreenPosition.x, 0f, Screen.width);
        mouseScreenPosition.y = Mathf.Clamp(mouseScreenPosition.y, 0f, Screen.height);

        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mouseScreenPosition);
        worldPosition.z = 0f;

        return worldPosition;
    }

    public static float GetAngleFromVector(Vector3 vector)
    {
        float radians = Mathf.Atan2(vector.y, vector.x);
        float degrees = radians * Mathf.Rad2Deg;
        return degrees;
    }

    public static AimDirection GetAimDirection(float angleDegrees)
    {
        AimDirection aimDirection;

        if (angleDegrees < 45 && angleDegrees >= -45)
            aimDirection = AimDirection.Side; //right
        else if (angleDegrees < -45 && angleDegrees >= -135)
            aimDirection = AimDirection.Down;
        else if ((angleDegrees < -135 && angleDegrees >= -180) || (angleDegrees >= 135 && angleDegrees <= 180)) //wrong
            aimDirection = AimDirection.Side; //left
        else // 45 =< angleDegrees < 135
            aimDirection = AimDirection.Up;

        return aimDirection;
    }
}
