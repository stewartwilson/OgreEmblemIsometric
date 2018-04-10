using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "GameData")]
public class GameData : ScriptableObject {
    [SerializeField]
    public string saveName;
    [SerializeField]
    public string timePlayed;
    [SerializeField]
    public string lastPlayed;
    [SerializeField]
    public PlayerData playerData;
    [SerializeField]
    public LevelData levelData;
    [SerializeField]
    public BattleData battleData;

}
