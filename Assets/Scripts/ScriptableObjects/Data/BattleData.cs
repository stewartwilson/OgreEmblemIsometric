using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "BattleData")]
public class BattleData : ScriptableObject {
    [SerializeField]
    public BattleMapData battleMapData;
    [SerializeField]
    public UnitGroup playerGroup;
    [SerializeField]
    public UnitGroup enemyGroup;
    [SerializeField]
    public string sceneName;
    [SerializeField]
    public bool afterBattle;
    [SerializeField]
    public bool didPlayerWin;
}
