using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Slash : Skill {

    private Dictionary<int,int[]> targetPreference = new Dictionary<int, int[]> {
        { 0, new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 } },
        { 1, new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 } },
        { 2, new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 } },
        { 3, new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 } },
        { 4, new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 } },
        { 5, new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 } },
        { 6, new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 } },
        { 7, new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 } },
        { 8, new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 } },
    };

    public override Unit act(UnitGroup allies, UnitGroup enemies, int casterPosition)
    {
        for(int i = 0; i < targetPreference[casterPosition].Length; i++)
        {
            foreach (UnitPosition up in enemies.unitList)
            {
                if (targetPreference[casterPosition][i] == up.position &&
                    up.unit.health > 0)
                {
                    return up.unit;
                }
            }               
        }
        
        return null;
    }


}
