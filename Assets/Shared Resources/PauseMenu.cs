using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Pause))
        {
            if(GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
	}
    
    public void Resume()
    {
        if (GameIsPaused)
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            GameIsPaused = false;
        }
        else
        {
            Pause();
        }
    }

    public void Pause()
    {
        if (!GameIsPaused)
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            GameIsPaused = true;
        } else
        {
            Resume();
        }
    }

    public void LoadOptionsMenu()
    {
        Debug.Log("Load Options Menu");
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
    }
}
