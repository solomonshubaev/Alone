using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


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


    public static float CalculatePercent(float maxValue, float currentValue)
    {
        return currentValue * 100.0f / maxValue;
    }

    public static LookDirection? GetLookDirection(Vector2 moveDirection)
    {
        if (moveDirection.x > 0 && moveDirection.y == 0)
        {
            return LookDirection.Right;
        }
        else if (moveDirection.x < 0 && moveDirection.y == 0)
        {
            return LookDirection.Left;
        }
        else if (moveDirection.y > 0 && moveDirection.x == 0)
        {
            return LookDirection.Up;
        }
        else if (moveDirection.y < 0 && moveDirection.x == 0)
        {
            return LookDirection.Down;
        }

        return null;
    }
}
