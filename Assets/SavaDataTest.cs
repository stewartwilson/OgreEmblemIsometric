using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavaDataTest : MonoBehaviour {

    public string gameName;
    public SaveData saveData;
    public GameData gameData;

    void Start()
    {
        if(gameData == null)
        {
            gameData = GameObject.Find("Game Data Controller").GetComponent<GameDataController>().gameData;
        }
    }

	public void LoadSaveFile()
    {
        gameData.playerData = SaveDataHelper.loadPlayerData(gameName);
    }

    public void SaveGameData()
    {
        SaveDataHelper.createSaveFile(gameData.playerData);
    }
}
