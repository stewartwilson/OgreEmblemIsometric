using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneInputController : MonoBehaviour {
	
	void Update () {
        if (Input.GetButtonDown("Submit"))
        {
            GameObject.Find("Cutscene Controller").GetComponent<CutsceneController>().loadNextScene();
        }
	}
}
