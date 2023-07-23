using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private MovementDetailsSO movementDetails;

    private Player player;
    private float moveSpeed;

    private void Awake()
    {
        this.player = GetComponent<Player>();
        this.moveSpeed = this.movementDetails.moveSpeed;
    }

    private void Update()
    {
        this.MovementInput();
    }

    private void MovementInput()
    {
        float horizontalMovement = Input.GetAxisRaw(InputEnum.Horizontal.ToString());
        float verticalMovement = Input.GetAxisRaw(InputEnum.Vertical.ToString());
        Vector2 direction = new Vector2(horizontalMovement, verticalMovement);

        // Adjust distance for diagonal movement (pythagoras approximation)
        if (horizontalMovement != 0f && verticalMovement != 0f)
        {
            direction *= 0.7f;
        }

        if (this.IsThereMovement(direction))
        {
            this.player.movementByVelocityEvent.CallMovementByVelocityEvent(direction, this.moveSpeed);
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