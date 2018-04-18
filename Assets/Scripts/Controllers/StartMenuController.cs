using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuController : MonoBehaviour
{

    public GameObject startPanel;
    public GameObject optionsPanel;
    public AllGameData saveData;
    public GameObject loadGamePanel;
    public GameObject saveFilePanel;
    public List<PlayerData> saves;

    public void StartNewGame()
    {
        Debug.Log("Creating new game");
        SceneManager.LoadScene("Character Creator");

    }

    public void LoadGame(PlayerData playerData)
    {
        GameObject.Find("Game Data Controller").GetComponent<GameDataController>().gameData.playerData = playerData;
        Debug.Log("Loading game");
        if (playerData.inOverworld)
        {
            SceneManager.LoadScene("Overworld", LoadSceneMode.Additive);
        }
        else
        {
            SceneManager.LoadScene(playerData.currentMission, LoadSceneMode.Additive);
        }
        SceneManager.UnloadSceneAsync("Start Menu");


    }

    public void OpenLoadGameScreen()
    {
        SceneManager.LoadScene("Persistence", LoadSceneMode.Additive);
        loadGamePanel.SetActive(true);
        startPanel.SetActive(false);
        saves = SaveDataHelper.getListOfSaves();
        if (saves != null && saves.Count > 0)
        {
            int count = 0;
            foreach (PlayerData save in saves)
            {
                GameObject go = Instantiate(saveFilePanel, new Vector2(0, 0), Quaternion.identity, GameObject.Find("Save File Panels").transform);
                go.transform.Translate(new Vector2(0, -count * 100 - 10));
                go.name = save.playerName;
                go.GetComponent<SaveFilePanelController>().playerData = save;
                //go.GetComponent<SaveFilePanelController>().gameData = gd;
                Text saveName = GameObject.Find(go.name + "/Save Name Value").GetComponent<Text>();
                saveName.text = save.playerName;
                Text currentMission = GameObject.Find(go.name + "/Current Mission Value").GetComponent<Text>();
                currentMission.text = save.currentMission;
                Text playTime = GameObject.Find(go.name + "/Play Time Value").GetComponent<Text>();
                playTime.text = save.playTime;
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
