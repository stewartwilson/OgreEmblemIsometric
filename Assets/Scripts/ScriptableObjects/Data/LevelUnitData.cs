using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "LevelUnitData")]
class LevelUnitData : ScriptableObject
{
    [SerializeField]
    public List<LevelUnitPosition> unitPositions;
}


