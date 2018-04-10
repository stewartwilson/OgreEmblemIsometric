using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataController : MonoBehaviour {

    public GameData gameData;

    private void Awake()
    {
        if(gameData == null)
        {
            gameData = ScriptableObject.CreateInstance<GameData>();
        }
    }
}
