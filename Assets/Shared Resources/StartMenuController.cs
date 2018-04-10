using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuController : MonoBehaviour {

    public GameObject startPanel;
    public GameObject optionsPanel;
    public AllGameData saveData;
    public GameObject loadGamePanel;
    public GameObject saveFilePanel;

    private void Start()
    {
        SceneManager.LoadScene("Persistence", LoadSceneMode.Additive);
    }

    public void StartNewGame()
    {
        string activeSceneName = SceneManager.GetActiveScene().name;
        Debug.Log("Creating new game");
        SceneManager.LoadScene("New Game", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(activeSceneName);

    }

    public void LoadGame(GameData gameData)
    {
        string activeSceneName = SceneManager.GetActiveScene().name;
        GameObject.Find("Game Data Controller").GetComponent<GameDataController>().gameData = gameData;
        Debug.Log("Loading game");
        SceneManager.LoadScene("Overworld", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(activeSceneName);
        
        
    }

    public void OpenLoadGameScreen()
    {
        loadGamePanel.SetActive(true);
        startPanel.SetActive(false);
        if (saveData != null && saveData.listOfSaves.Count > 0)
        {
            int count = 0;
            foreach (GameData gd in saveData.listOfSaves)
            {
                GameObject go = Instantiate(saveFilePanel, new Vector2(0, 0),Quaternion.identity, GameObject.Find("Save File Panels").transform);
                go.transform.Translate(new Vector2(0, -count * 100 - 10));
                go.name = "Save " + count;
                go.GetComponent<SaveFilePanelController>().gameData = gd;
                Text saveName = GameObject.Find(go.name +"/Save Name Value").GetComponent<Text>();
                saveName.text = gd.saveName;
                Text playTime = GameObject.Find(go.name + "/Play Time Value").GetComponent<Text>();
                playTime.text = gd.timePlayed;
                Text lastPlayed = GameObject.Find(go.name + "/Last Played Value").GetComponent<Text>();
                lastPlayed.text = gd.lastPlayed;
                count++;

            }
        }
    }

    public void OpenOptions()
    {
        optionsPanel.SetActive(true);
        startPanel.SetActive(false);
    }

    public void CloseOptions()
    {
        optionsPanel.SetActive(false);
        startPanel.SetActive(true);
    }
}
