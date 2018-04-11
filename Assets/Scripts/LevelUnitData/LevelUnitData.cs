using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelUnitData
{
    public List<LevelUnitPosition> unitPositions;
    public bool battleStarted = false;
    public Unit battleUnitPlayer;
    public Unit battleUnitEnemy;

    public void moveUnit(Unit u, GridPosition pos, int elevation)
    {
        LevelUnitPosition toMove;
        foreach (LevelUnitPosition lup in unitPositions)
        {
            if (u.Equals(lup.unit))
            {
                toMove = lup;
                break;
            }
        }
        toMove.position.x = pos.x;
        toMove.position.y = pos.y;
        toMove.elevation = elevation;
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
}


