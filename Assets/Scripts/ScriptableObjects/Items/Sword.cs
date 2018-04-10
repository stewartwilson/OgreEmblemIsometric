using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sword : Weapon {

	public Sword()
    {
        attackPower = 5;
        isRanged = false;
    }
}
