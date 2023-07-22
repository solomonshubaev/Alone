using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class MovementByVelocityEvent : MonoBehaviour
{
    public event Action<MovementByVelocityEvent, MovementByVelocityArgs> onMovementByVelocity;

    public void CallMovementByVelocityEvent(Vector2 moveDirection, float moveSpeed)
    {
        onMovementByVelocity?.Invoke(this, new MovementByVelocityArgs() { moveDirection = moveDirection, moveSpeed = moveSpeed });
    }

}

public class MovementByVelocityArgs: EventArgs
{
    public Vector2 moveDirection;
    public float moveSpeed;
}