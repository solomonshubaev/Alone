using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(MovementByVelocityEvent))]
[DisallowMultipleComponent]
public class MovementByVelocity : MonoBehaviour
{
    private Rigidbody2D rigidBody2D;
    private MovementByVelocityEvent movementByVelocityEvent;

    private void Awake()
    {
        this.rigidBody2D = GetComponent<Rigidbody2D>();
        this.movementByVelocityEvent = GetComponent<MovementByVelocityEvent>();
    }

    private void OnEnable()
    {
        this.movementByVelocityEvent.onMovementByVelocity += this.MovementByVelocityEvent_OnMovementByVelocity;
    }

    private void OnDisable()
    {
        this.movementByVelocityEvent.onMovementByVelocity -= this.MovementByVelocityEvent_OnMovementByVelocity;
    }

    private void MovementByVelocityEvent_OnMovementByVelocity(MovementByVelocityEvent movementByVelocityEvent,
        MovementByVelocityArgs movementByVelocityArgs)
    {
        this.MoveRigidbody(movementByVelocityArgs.moveDirection, movementByVelocityArgs.moveSpeed);
    }

    private void MoveRigidbody(Vector2 moveDirection, float moveSpeed)
    {
        this.rigidBody2D.velocity = moveDirection * moveSpeed;
    }
}
