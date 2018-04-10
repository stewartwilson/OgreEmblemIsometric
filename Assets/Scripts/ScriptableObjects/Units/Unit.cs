using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Unit : ScriptableObject {

    public Sprite sprite;
    public string id;
    public string unitName;
    public UnitClass unitClass;
    public int width;
    public int height;
    public int level;
    public int experience;
    public int strength;
    public int dexterity;
    public int wisdom;
    public int vitality;
    public int speed;
    public int health;
    public int maxHealth;
    public int alignment;
    public int critChance;
    public int initiation;
    public UnitAction[] actionList = new UnitAction[3];
    public bool canAct;
    public Weapon mainWeapon;
    public bool canLead;
    public bool isEnemy;

    private int generateUniqueID()
    {
        int uniqueID = 0;

        return uniqueID;
    }

    public void levelUp()
    {
        level++;
        experience -= 100;
    }

    public void addXP(int xp)
    {
        experience += xp;
        if(experience >=100)
        {
            levelUp();
        }
    }

    public void changeCurrentHealth(int value)
    {
        health += value;
        if(health > maxHealth)
        {
            health = maxHealth;
        }
        if(health < 0 )
        {
            health = 0;
        }
    }

    public void resetHealth()
    {
        health = maxHealth;
    }

    public abstract UnitAction returnAction(UnitGroup enemies, UnitGroup allies);

    public abstract int takeDamage(int damage);

    public abstract int takeHealing(int healing);


}
