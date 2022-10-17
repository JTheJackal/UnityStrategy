using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

    private GridPosition gridPosition;
    private MoveAction moveAction;
    private SpinAction spinAction;
    private BaseAction[] BaseActionArray;
    private int actionPoints    = 2;

    private void Awake(){

        moveAction      = GetComponent<MoveAction>();
        spinAction      = GetComponent<SpinAction>();
        BaseActionArray = GetComponents<BaseAction>();
    }

    private void Start(){

        gridPosition   = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.AddUnitAtGridPosition(gridPosition, this);
    }
    
    private void Update(){
        
        MouseWorld.ShowCursor();

        GridPosition newGridPosition   = LevelGrid.Instance.GetGridPosition(transform.position);

        if(newGridPosition != gridPosition){

            // Unit changed grid position
            LevelGrid.Instance.UnitMovedGridPosition(this, gridPosition, newGridPosition);
            gridPosition    = newGridPosition;
        }
    }

    public MoveAction GetMoveAction(){

        return moveAction;
    }

    public SpinAction GetSpinAction(){

        return spinAction;
    }

    public GridPosition GetGridPosition(){

        return gridPosition;
    }

    public BaseAction[] GetBaseActionArray(){

        return BaseActionArray;
    }

    public bool TrySpendActionPointsToTakeAction(BaseAction baseAction){

        if(CanSpendActionPointsToTakeAction(baseAction)){

            SpendActionPoints(baseAction.GetActionPointsCost());
            return true;
        }else{

            return false;
        }
    }

    public bool CanSpendActionPointsToTakeAction(BaseAction baseAction){

        if(actionPoints >= baseAction.GetActionPointsCost()){

            return true;
        }else{

            return false;
        }
    }

    private void SpendActionPoints(int amount){

        actionPoints -= amount;
    }

    public int GetActionPoints(){

        return actionPoints;
    }
}
