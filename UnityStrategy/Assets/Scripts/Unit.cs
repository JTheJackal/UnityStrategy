using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

    [SerializeField] private Animator unitAnimator;
    private float rotateSpeed       = 20f;
    private Vector3 targetPosition;
    private GridPosition gridPosition;

    private void Awake(){

        targetPosition  = transform.position;
    }

    private void Start(){

        gridPosition   = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.AddUnitAtGridPosition(gridPosition, this);
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

        MouseWorld.ShowCursor();

        GridPosition newGridPosition   = LevelGrid.Instance.GetGridPosition(transform.position);

        if(newGridPosition != gridPosition){

            // Unit changed grid position
            LevelGrid.Instance.UnitMovedGridPosition(this, gridPosition, newGridPosition);
            gridPosition    = newGridPosition;
        }
        
    }

    public void Move(Vector3 targetPosition){

        this.targetPosition = targetPosition;
    }
}
