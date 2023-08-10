using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[DisallowMultipleComponent]
[RequireComponent(typeof(MovementByVelocityEvent))]
[RequireComponent(typeof(MovementByVelocity))]
[RequireComponent(typeof(SortingGroup))]
public class DynamicObject : MonoBehaviour
{
    [SerializeField] private DynamicObjectSO dynamicObjectSO;

    private MovementByVelocityEvent movementByVelocityEvent;

    private bool isMoving;
    private float lastTimeFinishedMove;
    private float timeBetweenMoves;
    private Vector2 randomPositionToMove;
    private Vector2 startingPos;

    private SortingGroup sortingGroup;

    private void Awake()
    {
        this.movementByVelocityEvent = GetComponent<MovementByVelocityEvent>();
        this.sortingGroup = GetComponent<SortingGroup>();
    }

    private void Start()
    {
        this.startingPos = (Vector2)this.transform.position;
        this.isMoving = false;
        this.lastTimeFinishedMove = Time.time;
        this.timeBetweenMoves = Random.Range(this.dynamicObjectSO.minSecondsBetweenMove,
            this.dynamicObjectSO.maxSecondsBetweenMove);
        this.sortingGroup.sortingOrder = HelperUtilities.GetSortingOrderByPosition(this.transform.position);
    }

    private void Update()
    {
        if (!this.isMoving
            && GameManager.Instance.IsDark()
            && this.dynamicObjectSO.distanceFromPlayerToMove < Player.Instance.DistanceFromPlayer(this.transform.position)
            && this.lastTimeFinishedMove + this.timeBetweenMoves <= Time.time)
        {
            this.isMoving = true;
            this.Moving();
        }
        else if (this.isMoving)
        {
            this.sortingGroup.sortingOrder = HelperUtilities.GetSortingOrderByPosition(this.transform.position);
            if (Vector2.Distance(this.transform.position, this.randomPositionToMove) < 0.05)
            {
                this.movementByVelocityEvent.CallMovementByVelocityEvent(Vector2.zero, 0f, false);
                this.lastTimeFinishedMove = Time.time;
                this.timeBetweenMoves = Random.Range(this.dynamicObjectSO.minSecondsBetweenMove,
                    this.dynamicObjectSO.maxSecondsBetweenMove);
                this.isMoving = false;
            }
        }
        else if (this.dynamicObjectSO.distanceFromPlayerToMove >= Player.Instance.DistanceFromPlayer(this.transform.position))
        {
            this.lastTimeFinishedMove = Time.time;
        }
    }

    private void Moving()
    {
        this.randomPositionToMove = this.startingPos + (Random.insideUnitCircle * this.dynamicObjectSO.maxRadiusOfNextPosPoint);
        Vector2 direction = (randomPositionToMove - (Vector2)this.transform.position).normalized;
        this.movementByVelocityEvent.CallMovementByVelocityEvent(direction, this.dynamicObjectSO.movementSpeed, false);    
    }

}
