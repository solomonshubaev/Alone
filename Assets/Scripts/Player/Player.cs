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
[RequireComponent(typeof(InformationUiEvent))]
#endregion
public class Player : MonoBehaviour
{
    [HideInInspector] public PlayerDetailsSO playerDetails;
    [SerializeField] public PlayerInformationSO playerInformation;
    [HideInInspector] public SpriteRenderer spriteRendrer;
    [HideInInspector] public Animator animator;
    [HideInInspector] public IdleEvent idleEvent;
    [HideInInspector] public MovementByVelocityEvent movementByVelocityEvent;
    private InformationUiEvent informationUiEvent;

    [HideInInspector] public int hunger;
    [HideInInspector] public float stamina;

    public void Awake()
    {
        this.spriteRendrer = GetComponent<SpriteRenderer>();
        this.animator = GetComponent<Animator>();
        this.idleEvent = GetComponent<IdleEvent>();
        this.movementByVelocityEvent = GetComponent<MovementByVelocityEvent>();
        this.informationUiEvent = GetComponent<InformationUiEvent>();
    }

    private void Start()
    {
        this.hunger = this.playerInformation.maxHunger;
        this.stamina = this.playerInformation.maxStamina;
    }

    private void Update() // will it be critical who will execute first? playerControl or player?
    {
        this.informationUiEvent.CallUpdatePlayer_InformationUiEvent(HelperUtilities.CalculatePercent(this.playerInformation.maxHunger, this.hunger),
            HelperUtilities.CalculatePercent(this.playerInformation.maxStamina, this.stamina));
    }

    private void Initialize(PlayerDetailsSO playerDetails)
    {
        this.playerDetails = playerDetails;
    }
}
