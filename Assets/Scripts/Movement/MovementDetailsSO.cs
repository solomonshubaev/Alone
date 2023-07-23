using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MovementDetails_", menuName = "Scriptable Objects/Movement/MovementDetails")]
public class MovementDetailsSO : ScriptableObject
{
    public float walkingSpeed = 5f;
    public float runningSpeed = 8f;

    private void OnValidate()
    {
        if (this.walkingSpeed <= 0)
        {
            Debug.LogWarning("walkingSpeed has to be positive, current value: " + this.walkingSpeed);
        }
        if (this.runningSpeed <= 0)
        {
            Debug.LogWarning("runningSpeed has to be positive, current value: " + this.runningSpeed);
        }
    }
}
