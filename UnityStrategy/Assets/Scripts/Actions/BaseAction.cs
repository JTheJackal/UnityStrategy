using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAction : MonoBehaviour
{
    protected Action onActionComplete;
    protected Unit unit;
    protected bool isActive;

    protected virtual void Awake(){

        unit    = GetComponent<Unit>();
    }
}
