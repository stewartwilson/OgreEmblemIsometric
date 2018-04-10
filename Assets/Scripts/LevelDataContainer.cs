using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDataContainer : MonoBehaviour {

    public LevelData levelData;
    public string sceneName;

	// Use this for initialization
	void Start () {
        if (levelData.sceneName != null) {
            sceneName = levelData.sceneName;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
