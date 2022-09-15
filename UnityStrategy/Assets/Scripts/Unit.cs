using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

    [SerializeField] private Animator unitAnimator;
    private float rotateSpeed       = 20f;
    private Vector3 targetPosition;

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

        if(Input.GetMouseButtonDown(0)){
            
            // Set the target position
            Move(MouseWorld.GetPosition());
        }
    }

    private void Move(Vector3 targetPosition){

        this.targetPosition = targetPosition;
    }
}
