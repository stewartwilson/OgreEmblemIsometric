using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[System.Serializable]
public abstract class UnitAction
{
    [SerializeField]
    public string actionName;
    [SerializeField]
    public string description;
    [SerializeField]
    public Unit actor;
    [SerializeField]
    public Unit target;
    [SerializeField]
    private Dictionary<int, int[]> targetPreference;

    public abstract void resolveTarget(UnitGroup allies, UnitGroup enemies, int casterPosition);
}
