using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region REQUIRE COMPONENTS
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(IdleEvent))]
#endregion
[DisallowMultipleComponent]
public class Idle : MonoBehaviour
{
    private Rigidbody2D rigidBody2D;
    private IdleEvent idleEvent;

    private void Awake()
    {
        this.rigidBody2D = GetComponent<Rigidbody2D>();
        this.idleEvent = GetComponent<IdleEvent>();
    }

    private void OnEnable()
    {
        idleEvent.onIdle += this.IdleEvent_OnIdle;
    }

    private void OnDisable()
    {
        idleEvent.onIdle -= this.IdleEvent_OnIdle;
    }

    private void IdleEvent_OnIdle(IdleEvent idleEvent)
    {
        this.MoveRigidBody();
    }

    private void MoveRigidBody()
    {
        this.rigidBody2D.velocity = Vector2.zero;
    }
}
