using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

    private Vector3 targetPosition;

    private void Update(){
        
        float stoppingDistance      = 0.1f;

        // If the object is within the stopping distance...
        if(Vector3.Distance(transform.position, targetPosition) > stoppingDistance){
            
            // ...move
            Vector3 moveDirection   = (targetPosition - transform.position).normalized;
            float moveSpeed         = 4f;
            transform.position      += moveSpeed * Time.deltaTime * moveDirection;
        }

        if(Input.GetKeyDown(KeyCode.T)){
            
            // Set the target position
            Move(new Vector3(4, 0, 4));
        }
    }

    private void Move(Vector3 targetPosition){

        this.targetPosition = targetPosition;
    }
}
