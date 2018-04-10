using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "UnitGroup")]
public class UnitGroup : ScriptableObject {

    //   front
    // [0][1][2]
    // [3][4][5]
    // [6][7][8]
    //   back
    [SerializeField]
    public Unit leader;
    [SerializeField]
    public List<Item> items;
    [SerializeField]
    public List<UnitPosition> unitList;
    [SerializeField]
    public int movementDistance;
    

    public UnitGroup()
    {
        unitList = new List<UnitPosition>();
    }

    public void addUnit(Unit toAdd, int position)
    {
        UnitPosition u = new UnitPosition();
        u.position = position;
        u.unit = toAdd;
        unitList.Add(u);
    }

    public bool canAddUnit(Unit toAdd, int position)
    {
        bool canAdd = true;

        return canAdd;
    }

    public Unit getLeader()
    {
        return leader;
    }

    public void setLeader(Unit leader)
    {
        if(leader.canLead)
            this.leader = leader;
    }

    public bool isDefeated()
    {
        bool defeated = true;
        foreach (UnitPosition u in unitList)
        {
            if(u.unit.health > 0)
            {
                defeated = false;
            }
        }
        return defeated;
    }
    

}