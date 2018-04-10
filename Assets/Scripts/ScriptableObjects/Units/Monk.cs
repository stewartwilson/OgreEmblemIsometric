using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Units/Monk")]
public class Monk : Unit {

    public new void levelUp()
    {
        base.levelUp();
        strength += 3;
        dexterity += 2;
        wisdom += 0;
        vitality += 2;
        maxHealth += 2;
        health += 2;
    }

    public override UnitAction returnAction(UnitGroup enemies, UnitGroup allies)
    {
        MeleeAttack action = new MeleeAttack();
        //TODO pass in correct unit position
        action.resolveTarget(allies, enemies, 0);
        action.damage = determineActionDamage();
        return action;
    }

    public int determineActionDamage()
    {
        int damage = 0;
        damage += mainWeapon.attackPower;

        return damage;
    }

    public override int takeDamage(int damage)
    {
        changeCurrentHealth(-damage);
        return damage;
    }

    public override int takeHealing(int healing)
    {
        changeCurrentHealth(healing);
        return healing;
    }
}
