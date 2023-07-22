using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "playerDetails_", menuName = "Scriptable Objects/Player/Player Details")]
public class PlayerDetailsSO : ScriptableObject
{
    [Tooltip("Prefab gameobject for player")]
    public GameObject playerPrefab;

    [Tooltip("Player runtime animator controller")]
    public RuntimeAnimatorController runtimeAnimatorController;

    private void OnValidate()
    {
        //HelperUtilities - check attribute are legal
        if(this.playerPrefab == null)
        {
            Debug.LogWarning("Prefab can't be null"); // TODO: do general method for this.
        }
    }
}
