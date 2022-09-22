using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelectedVisual : MonoBehaviour
{
    [SerializeField] private Unit unit;

    private MeshRenderer meshRenderer;

    private void Awake(){

        meshRenderer    = GetComponent<MeshRenderer>();
    }

    private void Start(){

        // Subscribe to event
        UnitActionSystem.Instance.OnSelectedUnitChanged += UnitActionSystem_OnSelectedUnitChanged;

        // Call for select visual to change
        UpdateVisual();
    }

    private void UnitActionSystem_OnSelectedUnitChanged(object sender, EventArgs empty){

        UpdateVisual();
    }

    private void UpdateVisual(){

        if(UnitActionSystem.Instance.GetSelectedUnit() == unit){

            meshRenderer.enabled    = true;
        }else{

            meshRenderer.enabled    = false;
        }
    }
}
