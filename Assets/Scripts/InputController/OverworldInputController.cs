using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class OverworldInputController : MonoBehaviour
{
    public float cursorDelay;
    public bool canCursorMove;
    float nextCursorMoveAllowed;
    //Holds cursor object
    public GameObject cursor;
    public GridPosition cursorPositon;
    public int maxCursorXPos = 1;
    public int maxCursorYPos = 0;
    //Hold onto an object that the cursor has selected
    public GameObject selectedObject;
    public GameObject levelCanvas;
    private LevelData levelData;
    private bool levelCanvasActive;

    private void Start()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Overworld"));
    }

    private void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        bool enter = Input.GetButtonDown("Submit");
        bool back = Input.GetButtonDown("Back");

        if (levelCanvasActive) {
            if(back)
            {
                Time.timeScale = 1f;
                levelCanvasActive = false;
                levelCanvas.SetActive(false);
            }
        }
        else
        {
            //resonsible for moving cursor when nothing is selected
            if (!(Time.time < nextCursorMoveAllowed))
            {
                if (cursorPositon.x < maxCursorXPos && horizontal > 0)
                {
                    cursorPositon.x++;
                    cursorPositon.y--;
                }
                if (cursorPositon.x > 0 && horizontal < 0)
                {
                    cursorPositon.x--;
                    cursorPositon.y++;
                }
                updateCursor();

                nextCursorMoveAllowed = Time.time + cursorDelay;
            }
            GameObject tempSelect = cursor.GetComponent<CursorController>().objectInTile;
            if (tempSelect != null)
            {
                if (enter)
                {
                    selectObject(cursor.GetComponent<CursorController>().objectInTile);
                }
            }
        }
    }

    /**
     * 
     * 
     */
    public void updateCursor()
    {
        cursor.transform.position = IsometricHelper.gridToGamePostion(cursorPositon);
    }

    /**
     * 
     * 
     */
    private void selectObject(GameObject selected)
    {
        selectedObject = selected;
        switch (selectedObject.tag)
        {
            case "Level":
                Debug.Log("selected Level: " + selected.name);
                Time.timeScale = 0f;
                levelCanvasActive = true;
                levelCanvas.SetActive(true);
                break;
            default:
                Debug.Log("Nothing to select");
                break;
        }
    }

    public void launchLevel()
    {
        if(selectedObject != null && "Level".Equals(selectedObject.tag))
        {
            if(selectedObject.GetComponent<LevelDataContainer>().levelData.map.Count == 0)
            {
                selectedObject.GetComponent<LevelDataContainer>().levelData.generateMapFromTextFile();
            }
            GameObject.Find("Game Data Controller").GetComponent<GameDataController>().gameData.levelData = selectedObject.GetComponent<LevelDataContainer>().levelData;
            string sceneName = selectedObject.GetComponent<LevelDataContainer>().sceneName;
            string activeSceneName = SceneManager.GetActiveScene().name;
            Debug.Log("Loading " + sceneName);
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync(activeSceneName);
            Time.timeScale = 1f;
        }
    }

}
