using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerControl : MonoBehaviour
{
    [SerializeField] private MovementDetailsSO movementDetails;

    private Player player;
    private float walkingSpeed;
    private float runningSpeed;
    private float lastTimeRan;
    private bool isRunning = false;

    private void Awake()
    {
        this.player = GetComponent<Player>();
        this.walkingSpeed = this.movementDetails.walkingSpeed;
        this.runningSpeed = this.movementDetails.runningSpeed;
        this.lastTimeRan = Time.time;
        this.isRunning = false;
    }

    private void Update()
    {
        this.MovementInput();
    }

    private void MovementInput()
    {
        float horizontalMovement = Input.GetAxisRaw(InputEnum.Horizontal.ToString());
        float verticalMovement = Input.GetAxisRaw(InputEnum.Vertical.ToString());
        float moveSpeed = this.walkingSpeed;
        Vector2 direction = new Vector2(horizontalMovement, verticalMovement);

        // Adjust distance for diagonal movement (pythagoras approximation)
        if (horizontalMovement != 0f && verticalMovement != 0f)
        {
            direction *= 0.7f;
        }

        if (Input.GetKey(KeyCode.LeftShift) && this.player.stamina > 0 && this.IsThereMovement(direction)) // running
        {
            this.isRunning = true;
            moveSpeed = this.runningSpeed;
            this.player.stamina = Mathf.Max(this.player.stamina - Time.deltaTime, 0); // duplicate logic
        }
        else // walking
        {
            if(this.isRunning)
            {
                // Here is the switch from running to walking
                this.lastTimeRan = Time.time;
                Debug.Log("Player switched from running to walking");
            }
            this.isRunning = false;

            if (this.player.stamina < this.player.playerInformation.maxStamina)
            {
                if (Time.time - this.lastTimeRan > this.player.playerInformation.recoverCoolDown)
                {
                    this.player.stamina = Mathf.Min(this.player.stamina + Time.deltaTime, this.player.playerInformation.maxStamina); // duplicate logic
                }
            }
        }

        if (this.IsThereMovement(direction))
        {
<<<<<<< HEAD
            this.player.movementByVelocityEvent.CallMovementByVelocityEvent(direction, moveSpeed, isRunning);
=======
            this.player.movementByVelocityEvent.CallMovementByVelocityEvent(direction, moveSpeed);
>>>>>>> e11af48daa1dc416857b3b2c80475ef048810e7a
        }
        else
        {
            this.player.idleEvent.CallIdleEvent();
        }
    }

    private bool IsThereMovement(Vector2 direction)
    {
        return direction != Vector2.zero;
    }

    #region VALIDATION
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperValidations.ValidateNotNull(this.movementDetails, nameof(this.movementDetails));
    }
#endif
    #endregion
}
