using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterCreatorController : MonoBehaviour {

	public void createCharacter(string name)
    {
        PlayerData newPlayer = PlayerDataHelper.createNewPlayer(name);
        GameData newGameData = (GameData)ScriptableObject.CreateInstance("GameData");
        newGameData.name = "gameDataContainer";
        newGameData.playerData = newPlayer;
        GameObject.Find("Game Data Controller").GetComponent<GameDataController>().gameData = newGameData;
        SceneManager.LoadScene("Opening Cutscene", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("Character Creator");

    }
}
