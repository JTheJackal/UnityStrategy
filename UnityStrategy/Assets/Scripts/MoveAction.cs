using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : MonoBehaviour
{

    [SerializeField] private Animator unitAnimator;
    [SerializeField] private int maxMoveDistance = 4;

    private Vector3 targetPosition;
    private float rotateSpeed       = 20f;
    private Unit unit;

    private void Awake(){

        targetPosition  = transform.position;
        unit            = GetComponent<Unit>();
    }

    private void Update(){

        float stoppingDistance      = 0.1f;

        // If the object is within the stopping distance...
        if(Vector3.Distance(transform.position, targetPosition) > stoppingDistance){
            
            // ...move
            Vector3 moveDirection   = (targetPosition - transform.position).normalized;
            float moveSpeed         = 4f;
            transform.position      += moveSpeed * Time.deltaTime * moveDirection;

            // Rotate the unit to face the move direction smoothly
            
            transform.forward       = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);

            // Trigger walking animation
            unitAnimator.SetBool("IsWalking", true);
        }else{
            
            // Trigger not walking
            unitAnimator.SetBool("IsWalking", false);
        }
    }

    public void Move(GridPosition gridPosition){

        this.targetPosition = LevelGrid.Instance.GetWorldPosition(gridPosition);
    }

    public bool IsValidActionGridPosition(GridPosition gridPosition){

        List<GridPosition> validGridPositionList    = GetValidActionGridPositionList();

        return validGridPositionList.Contains(gridPosition);
    }

    public List<GridPosition> GetValidActionGridPositionList(){

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
}
