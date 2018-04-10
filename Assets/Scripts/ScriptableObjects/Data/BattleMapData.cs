using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "BattleMapData")]
public class BattleMapData : LevelData
{
    [SerializeField]
    public GroundType groundType;
}

