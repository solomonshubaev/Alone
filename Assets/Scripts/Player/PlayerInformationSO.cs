using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "playerInformation_", menuName = "Scriptable Objects/Player/Player Information")]

public class PlayerInformationSO : ScriptableObject
{

    public int maxHunger = 100;

    public int maxStamina = 5;

    [Tooltip("How long will take to stamina to START recover")]
    public float recoverCoolDown = 3f;

    private void OnValidate()
    {
        if (maxHunger < 0)
        {
            Debug.LogWarning("maxHunger Can't be negative"); // TODO: do general method for this.
        }
        if (maxStamina < 0)
        {
            Debug.LogWarning("maxStamina Can't be negative"); // TODO: do general method for this.
        }
    }
}
