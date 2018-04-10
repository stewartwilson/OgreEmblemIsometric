using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "AllGameData")]
public class AllGameData : ScriptableObject {
    [SerializeField]
    public List<GameData> listOfSaves;
	
}
