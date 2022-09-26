using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitActionSystem : MonoBehaviour
{

    public static UnitActionSystem Instance { get; private set; }

    public event EventHandler OnSelectedUnitChanged;

    [SerializeField] private Unit selectedUnit;
    [SerializeField] private LayerMask unitLayerMask;


    private void Awake(){
        
        // Ensure only one instance exists at any time
        if(Instance != null){

            Debug.LogError("There's more than one UnitActionSystem " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }

        Instance    = this;
    }

    private void Update(){

        if(Input.GetMouseButtonDown(0)){
            
            // Test for units being selected
            if(TryHandleUnitSelection()) return;

            GridPosition mouseGridPosition  = LevelGrid.Instance.GetGridPosition(MouseWorld.GetPosition());

            if(selectedUnit.GetMoveAction().IsValidActionGridPosition(mouseGridPosition)){

                // Set the target position
                selectedUnit.GetMoveAction().Move(mouseGridPosition);
            }

            
        }
    }

    private bool TryHandleUnitSelection(){

        Ray ray     = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, unitLayerMask)){

            if(raycastHit.transform.TryGetComponent<Unit>(out Unit unit)){

                SetSelectedUnit(unit);
                return true;
            }
        }

        return false;
    }

    private void SetSelectedUnit(Unit unit){

        selectedUnit    = unit;

        // Fire event if there are subscribers
        OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
    }

    public Unit GetSelectedUnit(){

        return selectedUnit;
    }

}
