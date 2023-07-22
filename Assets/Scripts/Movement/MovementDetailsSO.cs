using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MovementDetails_", menuName = "Scriptable Objects/Movement/MovementDetails")]
public class MovementDetailsSO : ScriptableObject
{
    public float moveSpeed = 8f;

    private void OnValidate()
    {
        if (moveSpeed <= 0)
        {
            Debug.LogWarning("moveSpeed has to be positive, current value: " + moveSpeed);
        }
    }
}
