using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;


#region REQUIRE COMPONENTS
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SortingGroup))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Idle))]
[RequireComponent(typeof(IdleEvent))]
[RequireComponent(typeof(MovementByVelocityEvent))]
[RequireComponent(typeof(MovementByVelocity))]
[RequireComponent(typeof(AnimatePlayer))]
[RequireComponent(typeof(PlayerControl))]
#endregion
public class Player : MonoBehaviour
{
    [HideInInspector] public PlayerDetailsSO playerDetails;
    [HideInInspector] public SpriteRenderer spriteRendrer;
    [HideInInspector] public Animator animator;
    [HideInInspector] public IdleEvent idleEvent;
    [HideInInspector] public MovementByVelocityEvent movementByVelocityEvent;

    public void Awake()
    {
        this.spriteRendrer = GetComponent<SpriteRenderer>();
        this.animator = GetComponent<Animator>();
        this.idleEvent = GetComponent<IdleEvent>();
        this.movementByVelocityEvent = GetComponent<MovementByVelocityEvent>();
    }

    private void Initialize(PlayerDetailsSO playerDetails)
    {
        this.playerDetails = playerDetails;
    }
}
