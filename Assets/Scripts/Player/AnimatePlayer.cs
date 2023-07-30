using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
[DisallowMultipleComponent]
public class AnimatePlayer : MonoBehaviour
{
    private Player player;

    private void Awake()
    {
        this.player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        //Todo: Check race condition, if player not inited so it will be null, enforced it by project's settings (execution order).
        this.player.movementByVelocityEvent.onMovementByVelocity += MovementByVelocityEvent_OnMovementByVelocity;
        this.player.idleEvent.onIdle += IdleEvent_OnIdle;
    }

    private void OnDisable()
    {
        this.player.movementByVelocityEvent.onMovementByVelocity -= MovementByVelocityEvent_OnMovementByVelocity;
        this.player.idleEvent.onIdle -= IdleEvent_OnIdle;
    }

    private void MovementByVelocityEvent_OnMovementByVelocity(MovementByVelocityEvent movementByVelocityEvent,
        MovementByVelocityArgs movementByVelocityArgs)
    {
        this.SetMovementAnimationParameters(movementByVelocityArgs.moveDirection, movementByVelocityArgs.isRunning);
    }

    private void IdleEvent_OnIdle(IdleEvent idleEvent)
    {
        this.SetIdleAnimationParameters();
    }

    private void SetIdleAnimationParameters()
    {
        this.player.animator.SetBool(Settings.isWalking, false);
        this.player.animator.SetBool(Settings.isRunning, false);
        this.player.animator.SetBool(Settings.isIdle, true);
    }

    private void SetMovementAnimationParameters(Vector2 moveDirection, bool isRunning)
    {
        if(moveDirection.x > 0 && moveDirection.y == 0)
        {
            this.SetLookDirection(LookDirection.Right);
        }
        else if(moveDirection.x < 0 && moveDirection.y == 0)
        {
            this.SetLookDirection(LookDirection.Left);
        }
        else if(moveDirection.y > 0 && moveDirection.x == 0)
        {
            this.SetLookDirection(LookDirection.Up);
        }
        else if(moveDirection.y < 0 && moveDirection.x == 0)
        {
            this.SetLookDirection(LookDirection.Down);
        }
        this.player.animator.SetBool(Settings.isIdle, false);
        this.player.animator.SetBool(Settings.isRunning, isRunning);
        this.player.animator.SetBool(Settings.isWalking, !isRunning);
    }

    private void SetLookDirection(LookDirection lookDirection)
    {
        bool isLookingUp, isLookingSide, isLookingDown;
        bool mirror = false;
        switch(lookDirection)
        {
            case LookDirection.Up:
                isLookingUp = true;
                isLookingSide = false;
                isLookingDown = false;
                break;
            case LookDirection.Left:
                isLookingUp = false;
                isLookingSide = true;
                isLookingDown = false;
                break;
            case LookDirection.Right:
                mirror = true;
                isLookingUp = false;
                isLookingSide = true;
                isLookingDown = false;
                break;
            case LookDirection.Down:
                isLookingUp = false;
                isLookingSide = false;
                isLookingDown = true;
                break;
            default:
                isLookingUp = false;
                isLookingSide = false;
                isLookingDown = true;
                break;
        }
        this.player.animator.SetBool(Settings.lookUp, isLookingUp);
        this.player.animator.SetBool(Settings.lookSide, isLookingSide);
        this.player.animator.SetBool(Settings.lookDown, isLookingDown);
        this.player.spriteRendrer.flipX = mirror;
    }
}
