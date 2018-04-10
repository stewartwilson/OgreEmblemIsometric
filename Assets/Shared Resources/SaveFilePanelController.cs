using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveFilePanelController : MonoBehaviour {

    public GameData gameData;

    public void LoadGameFromData()
    {
        if(GameObject.Find("Start Menu Canvas") != null)
            GameObject.Find("Start Menu Canvas").GetComponent<StartMenuController>().LoadGame(gameData);
    }
}
