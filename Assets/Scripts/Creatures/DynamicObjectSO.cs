using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DynamicObject_", menuName = "Scriptable Objects/Creatures/Dynamic Object")]
public class DynamicObjectSO : ScriptableObject
{
    public float distanceFromPlayerToMove = 3f;
    public float movementSpeed = 1f;
    public float maxRadiusOfNextPosPoint = 5;

    public float minSecondsBetweenMove;
    public float maxSecondsBetweenMove;

    private void OnValidate()
    {
        if (this.distanceFromPlayerToMove < 0)
        {
            Debug.LogWarning("Distance should be positive, setting to 1");
            this.distanceFromPlayerToMove = 1f;
        }

        if (this.minSecondsBetweenMove > this.maxSecondsBetweenMove)
        {
            Debug.LogWarning("minSecondsBetweenMove is bigger than maxSecondsBetweenMove." +
                " Setting minSecondsBetweenMove to maxSecondsBetweenMove.");
            this.minSecondsBetweenMove = this.maxSecondsBetweenMove;
        }
    }
}
