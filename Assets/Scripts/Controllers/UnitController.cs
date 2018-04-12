using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour {

    public bool isPlayerUnit;
    public bool isSelected;
    public bool isDefeated;
    public UnitGroup unitgroup;
    public Facing facing;
    public bool canMove;
    protected Animator animator;
    public GridPosition position;
    public List<GridPosition> currentPath;
    public int maxMovement;
    public Vector2 spriteOffset;

    protected void Start()
    {
        animator = GetComponent<Animator>();
        if (unitgroup != null)
        {
            maxMovement = unitgroup.leader.maxMovement;
        } else
        {
            maxMovement = 1;
        }
    }

    protected void updateIfDefeated()
    {
        if (!isDefeated)
        {
            bool tempBool = true;
            foreach (UnitPosition up in unitgroup.unitList)
            {
                if (up.unit.health > 0)
                {
                    tempBool = false;
                }
            }
            isDefeated = tempBool;
        }
    }
}
