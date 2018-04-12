using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelUnitData
{
    public List<LevelUnitPosition> unitPositions;
    public bool battleStarted = false;
    public UnitGroup battleUnitPlayer;
    public UnitGroup battleUnitEnemy;

    public void moveUnit(UnitGroup ug, GridPosition pos)
    {
        LevelUnitPosition toMove;
        foreach (LevelUnitPosition lup in unitPositions)
        {
            if (ug.Equals(lup.unitGroup))
            {
                toMove = lup;
                break;
            }
        }
        toMove.position.x = pos.x;
        toMove.position.y = pos.y;
        toMove.position.elevation = pos.elevation;
    }

    public bool isSquareEmpty(GridPosition pos)
    {
        foreach (LevelUnitPosition lup in unitPositions)
        {
            if (lup.position.x == pos.x && lup.position.y == pos.y)
                return false;
        }
        return true;
    }

    public UnitGroup getUnitAt(GridPosition pos)
    {
        foreach (LevelUnitPosition lup in unitPositions)
        {
            if (lup.position.x == pos.x && lup.position.y == pos.y)
                return lup.unitGroup;
        }
        return null;
    }
}


