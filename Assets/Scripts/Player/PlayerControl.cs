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
    private GameObject interactableObject;

    private void Awake()
    {
        this.player = GetComponent<Player>();
        this.walkingSpeed = this.movementDetails.walkingSpeed;
        this.runningSpeed = this.movementDetails.runningSpeed;
        this.lastTimeRan = Time.time;
        this.isRunning = false;
        this.interactableObject = null;
    }

    private void Update()
    {
        this.MovementInput();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag(TagsEnum.Interactable.ToString()))
        {
            this.CheckClosestInteractableObject(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(TagsEnum.Interactable.ToString()))
        {
            if (collision.gameObject == this.interactableObject)
            {
                this.interactableObject = null;
            }
        }
    }

    private void InteractSystem()
    {
        if (this.interactableObject != null)
        {
            Debug.Log("Trying to interact with: " + this.interactableObject.name);
            InteractableAbstract interactableObject = this.interactableObject.GetComponent<InteractableAbstract>();
            HelperValidations.ValidateNotNull(interactableObject, nameof(interactableObject));
            interactableObject.InteractWithPlayer();
        }
    }

    private void CheckClosestInteractableObject(GameObject newInteractableObject)
    {
        if (this.interactableObject == null)
        {
            Debug.Log("Set new interactableObject");
            this.interactableObject = newInteractableObject;
            return;
        }

        if (Vector2.Distance(this.transform.position, this.interactableObject.transform.position)
            > Vector2.Distance(this.transform.position, newInteractableObject.transform.position))
        {
            this.interactableObject = newInteractableObject;
            return;
        }
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
            this.player.movementByVelocityEvent.CallMovementByVelocityEvent(direction, moveSpeed, isRunning);
        }
        else
        {
            this.player.idleEvent.CallIdleEvent();
        }

        if (Input.GetKeyDown(KeyCode.E) && !this.isRunning)
        {
            this.InteractSystem();
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
