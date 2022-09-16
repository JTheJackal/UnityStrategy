using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitActionSystem : MonoBehaviour
{
    [SerializeField] private Unit selectedUnit;
    [SerializeField] private LayerMask unitLayerMask;

    private void Update(){
        
        

        if(Input.GetMouseButtonDown(0)){
            
            // Test for units being selected
            if(TryHandleUnitSelection()) return;

            // Set the target position
            selectedUnit.Move(MouseWorld.GetPosition());
        }
    }

    private bool TryHandleUnitSelection(){

        Ray ray     = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, unitLayerMask)){

            if(raycastHit.transform.TryGetComponent<Unit>(out Unit unit)){

                selectedUnit    = unit;
                return true;
            }
        }

        return false;
    }

}
