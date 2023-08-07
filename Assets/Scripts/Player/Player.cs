using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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
[RequireComponent(typeof(InformationUiEvent))]
#endregion
public class Player : SingletonMonobehaviour<Player>
{
    [SerializeField] private Transform interactionCollidersParent;
    [SerializeField] private LookDirection currentInteractionDirection;
    [HideInInspector] public PlayerDetailsSO playerDetails;
    [SerializeField] public PlayerInformationSO playerInformation;
    [HideInInspector] public SpriteRenderer spriteRendrer;
    [HideInInspector] public Animator animator;
    [HideInInspector] public IdleEvent idleEvent;
    [HideInInspector] public MovementByVelocityEvent movementByVelocityEvent;
    private InformationUiEvent informationUiEvent;

    [SerializeField] public float vitality;
    [HideInInspector] public float stamina;

    protected override void Awake()
    {
        base.Awake();
        this.spriteRendrer = GetComponent<SpriteRenderer>();
        this.animator = GetComponent<Animator>();
        this.idleEvent = GetComponent<IdleEvent>();
        this.movementByVelocityEvent = GetComponent<MovementByVelocityEvent>();
        this.informationUiEvent = GetComponent<InformationUiEvent>();
        this.interactionCollidersParent = transform.Find("InteractionColliders");
        this.currentInteractionDirection = LookDirection.Down;
        this.SetInteractionDirectionActive(this.currentInteractionDirection);
    }

    private void OnEnable()
    {
        //Todo: Check race condition, if player not inited so it will be null, enforced it by project's settings (execution order).
        this.movementByVelocityEvent.onMovementByVelocity += MovementByVelocityEvent_SetInteractionToDirection;
    }

    private void OnDisable()
    {
        this.movementByVelocityEvent.onMovementByVelocity -= MovementByVelocityEvent_SetInteractionToDirection;
    }

    private void Start()
    {
        this.vitality = this.playerInformation.maxVitality;
        this.stamina = this.playerInformation.maxStamina;
    }

    private void Update() // will it be critical who will execute first? playerControl or player?
    {
        this.UpdateVitality();
        this.informationUiEvent.CallUpdatePlayer_InformationUiEvent(HelperUtilities.CalculatePercent(this.playerInformation.maxVitality, this.vitality),
            HelperUtilities.CalculatePercent(this.playerInformation.maxStamina, this.stamina));
    }

    private void Initialize(PlayerDetailsSO playerDetails)
    {
        this.playerDetails = playerDetails;
    }

    private void UpdateVitality()
    {
        this.vitality -= Time.deltaTime;
    }

    public void IncreaseVitalityBy(float value)
    {
        this.vitality = Mathf.Clamp(this.vitality + value, 0, this.playerInformation.maxVitality);
    }


    public void MovementByVelocityEvent_SetInteractionToDirection(MovementByVelocityEvent movementByVelocityEvent,
        MovementByVelocityArgs movementByVelocityArgs)
    {
        LookDirection? nullableLookDirection = HelperUtilities.GetLookDirection(movementByVelocityArgs.moveDirection);
        if (nullableLookDirection != null)
        {
            LookDirection lookDirection = (LookDirection)nullableLookDirection;
            this.SetInteractionDirectionActive(lookDirection);
        }
    }

    private void SetInteractionDirectionActive(LookDirection lookDirection)
    {
        if (this.WasLookingDirectionChanged(lookDirection))
        {
            this.interactionCollidersParent.Find(this.currentInteractionDirection.ToString()).gameObject.SetActive(false);
            this.interactionCollidersParent.Find(lookDirection.ToString()).gameObject.SetActive(true);
            this.currentInteractionDirection = lookDirection;
        }
    }

    private bool WasLookingDirectionChanged(LookDirection newLookDirection)
    {
        return this.currentInteractionDirection != newLookDirection;
    }
}
