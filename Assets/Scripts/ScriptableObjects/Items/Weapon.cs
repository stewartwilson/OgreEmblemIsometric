using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Weapon", menuName = "Inventory/Weapon")]
public class Weapon : Item {
    [SerializeField]
    public int attackPower;
    [SerializeField]
    public bool isRanged;

}
