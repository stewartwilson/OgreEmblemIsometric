using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Skill {

    public float id;
    public string skillName;
    public string description;
    private Dictionary<int, int[]> targetPreference;

    public abstract Unit act(UnitGroup allies, UnitGroup enemies, int casterPosition);
}
