using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{

    // prepare singleton
    public static LevelGrid Instance { get; private set; }

    [SerializeField] private Transform gridDebugObjectPrefab;
    
    private GridSystem gridSystem;

    private void Awake(){

        // Establish singleton
        if(Instance != null){

            Debug.LogError("There's more than one LevelGrid instance. " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance    = this;

        gridSystem  = new GridSystem(10, 10, 2f);  
        gridSystem.CreateDebugObjects(gridDebugObjectPrefab);
    }

    public void AddUnitAtGridPosition(GridPosition gridPosition, Unit unit){

        // Get the grid object which exists at the specified location
        GridObject gridObject   = gridSystem.GetGridObject(gridPosition);
        
        // Tell it which unit is occupying its space
        gridObject.AddUnit(unit);
    }

    public List<Unit> GetUnitListAtGridPosition(GridPosition gridPosition){

        // Get the grid object which exists at the specified location
        GridObject gridObject   = gridSystem.GetGridObject(gridPosition);

        // Get which unit is occupying its space
        return gridObject.GetUnitList();
    }

    public void RemoveUnitAtGridPosition(GridPosition gridPosition, Unit unit){

        // Get the grid object which exists at the specified location
        GridObject gridObject   = gridSystem.GetGridObject(gridPosition);
        
        // Tell it no units are occupying its space
        gridObject.RemoveUnit(unit);
    }

    public void UnitMovedGridPosition(Unit unit, GridPosition fromGridPosition, GridPosition toGridPosition){

        RemoveUnitAtGridPosition(fromGridPosition, unit);

        AddUnitAtGridPosition(toGridPosition, unit);
    }

    public int GetWidth() => gridSystem.GetWidth();

    public int GetHeight() => gridSystem.GetHeight();

    public GridPosition GetGridPosition(Vector3 worldPosition) => gridSystem.GetGridPosition(worldPosition);

    public Vector3 GetWorldPosition(GridPosition gridPosition) => gridSystem.GetWorldPosition(gridPosition);

    public bool IsValidGridPosition(GridPosition gridPosition) => gridSystem.IsValidGridPosition(gridPosition);

    public bool HasAnyUnitOnGridPosition(GridPosition gridPosition){

        GridObject gridObject   = gridSystem.GetGridObject(gridPosition);

        return gridObject.HasAnyUnit();
    }
}
