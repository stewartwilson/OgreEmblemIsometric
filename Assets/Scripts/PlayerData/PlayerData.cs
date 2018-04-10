using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData {
    public string playerName;
    public string playTime;
    public string currentMission;
    [SerializeField]
    List<Unit> units;
    [SerializeField]
    List<UnitGroup> unitGroups;
    [SerializeField]
    List<Item> inventory;

}
