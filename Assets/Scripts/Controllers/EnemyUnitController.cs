using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnitController : UnitController {

    protected void Update()
    {
        animator.SetInteger("Facing", (int)facing);
        updateIfDefeated();
        transform.position = IsometricHelper.gridToGamePostion(position) + spriteOffset;
    }
}
