using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : BaseAction
{

    [SerializeField] private Animator unitAnimator;
    [SerializeField] private int maxMoveDistance = 4;

    private Vector3 targetPosition;

    private float rotateSpeed   = 20f;

    protected override void Awake(){
        
        base.Awake();
        targetPosition  = transform.position;
    }

    private void Update(){

        if(!isActive){

            return;
        }

        float stoppingDistance      = 0.1f;
        

        Vector3 moveDirection       = (targetPosition - transform.position).normalized;

        // If the object is within the stopping distance...
        if(Vector3.Distance(transform.position, targetPosition) > stoppingDistance){
            
            // ...move
            float moveSpeed         = 4f;
            transform.position      += moveSpeed * Time.deltaTime * moveDirection;

            // Trigger walking animation
            unitAnimator.SetBool("IsWalking", true);
        }else{
            
            // Trigger not walking
            unitAnimator.SetBool("IsWalking", false);
            isActive                = false;
            onActionComplete();
        }

        // Rotate the unit to face the move direction smoothly
        transform.forward       = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);
    }

    public override void TakeAction(GridPosition gridPosition, Action onActionComplete){

        this.onActionComplete   = onActionComplete;
        this.targetPosition     = LevelGrid.Instance.GetWorldPosition(gridPosition);
        isActive                = true;
    }


    public override List<GridPosition> GetValidActionGridPositionList(){

        List<GridPosition> validGridPositionList    = new List<GridPosition>();

        GridPosition UnitGridPosition               = unit.GetGridPosition();

        for(int x = -maxMoveDistance; x <= maxMoveDistance; x++){
            for(int z = -maxMoveDistance; z <= maxMoveDistance; z++){

                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition   = UnitGridPosition + offsetGridPosition;

                if(!LevelGrid.Instance.IsValidGridPosition(testGridPosition)){

                    continue;
                }

                if(UnitGridPosition == testGridPosition){
                    
                    // Grid position is the current position of the selected unit
                    continue;
                }

                if(LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition)){
                    
                    // Grid position is occupied by another unit
                    continue;
                }

                validGridPositionList.Add(testGridPosition);
            }
        }

        return validGridPositionList;
    }

    public override string GetActionName(){

        return "Move";
    }
}
