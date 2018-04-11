using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class InputController : MonoBehaviour
{

    
    public GameObject playerUnitUIPanel;
    public GameObject enemyUnitUIPanel;
    public bool panelUIActive;
    public float cursorDelay;
    public bool canCursorMove;
    public float nextCursorMoveAllowed;
    //Holds cursor object
    public GameObject cursor;
    public GridPosition cursorPosition;
    public int maxCursorXPos;
    public int maxCursorYPos;
    public bool touching;
    //Hold onto an object that the cursor has selected
    public GameObject selectedObject;
    public bool movingUnit;
    public bool inspectingUnit;
    public bool placingUnit;
    //hols onto unit being move to determine location and directino facing
    public GameObject unitBeingPlaced;
    //holds units original location before an attempted move
    private Vector2 unitsOriginalLocation;
    private LevelData levelData;

    private void Start()
    {
        levelData = GameObject.Find("Level Controller").GetComponent<LevelController>().levelData;
        maxCursorXPos = levelData.getMaxX();
        maxCursorYPos = levelData.getMaxY();
    }

    private void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        bool enter = Input.GetButtonDown("Submit");
        bool back = Input.GetButtonDown("Back");
        if(panelUIActive)
        {
            if(back)
            {
                playerUnitUIPanel.SetActive(false);
                enemyUnitUIPanel.SetActive(false);
                panelUIActive = false;
            }
        }
        else if (placingUnit)
        {
            if (vertical > 0)
            {
                unitBeingPlaced.GetComponent<UnitController>().facing = Facing.Left;
            }
            if (vertical < 0)
            {
                unitBeingPlaced.GetComponent<UnitController>().facing = Facing.Right;
            }
            if (horizontal > 0)
            {
                unitBeingPlaced.GetComponent<UnitController>().facing = Facing.Back;
            }
            if (horizontal < 0)
            {
                unitBeingPlaced.GetComponent<UnitController>().facing = Facing.Forward;
            }
            if(back)
            {
                unitBeingPlaced.transform.position = unitsOriginalLocation;
                placingUnit = false;
            }
            else if(enter)
            {
                placingUnit = false;
            }
        }
        else
        {
            //resonsible for moving cursor when nothing is selected
            if (!(Time.time < nextCursorMoveAllowed))
            {
                GridPosition startPostion = new GridPosition(cursorPosition.x, cursorPosition.y, cursorPosition.elevation);
                if (vertical > 0)
                {
                    cursorPosition.y++;
                }
                if (vertical < 0)
                {
                    cursorPosition.y--;
                }
                if (horizontal > 0)
                {
                    cursorPosition.x++;
                }
                if (horizontal < 0)
                {
                    cursorPosition.x--;
                }

                if (cursorPosition.y < 0)
                {
                    cursorPosition.y = 0;
                }
                if (cursorPosition.x < 0)
                {
                    cursorPosition.x = 0;
                }
                if (cursorPosition.y > maxCursorYPos)
                {
                    cursorPosition.y = maxCursorYPos;
                }
                if (cursorPosition.x > maxCursorXPos)
                {
                    cursorPosition.x = maxCursorXPos;
                }
                if (movingUnit) {
                    if(GameObject.Find("Level Controller").GetComponent<LevelController>().possibleMoves.Contains(cursorPosition)) {
                        updateCursor();
                    }
                    else
                    {
                        cursorPosition = startPostion;
                    }
                } else 
                {
                    updateCursor();
                }
                

                nextCursorMoveAllowed = Time.time + cursorDelay;
            }
            GameObject tempSelect = cursor.GetComponent<CursorController>().objectInTile;
            if (tempSelect != null && !movingUnit)
            {
                if (enter)
                {
                    selectObject(cursor.GetComponent<CursorController>().objectInTile);
                    GameObject.Find("Level Controller").GetComponent<LevelController>().selectedUnit = selectedObject;
                    unitsOriginalLocation = selectedObject.transform.position;
                }
            }
            else
            {
                if (movingUnit && enter)
                {
                    
                    moveUnitTo(cursor.transform.position + new Vector3(0, .5f));
                    selectedObject = null;
                    GameObject.Find("Level Controller").GetComponent<LevelController>().selectedUnit = null;
                }
                if (enter && !movingUnit)
                {
                    selectedObject = null;
                    GameObject.Find("Level Controller").GetComponent<LevelController>().selectedUnit = null;
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
        LevelData levelData = GameObject.Find("Level Controller").GetComponent<LevelController>().levelData;
        MapTile mt = levelData.getMapTileFromXY(cursorPosition);
        cursor.transform.position = IsometricHelper.gridToGamePostion(cursorPosition);
        
    }

    /**
     * Move unit is called by UI button click
     * 
     */
    public void moveUnit()
    {
        if ("Player Unit".Equals(selectedObject.tag))
        {
            movingUnit = true;
            playerUnitUIPanel.SetActive(false);
            panelUIActive = false;
        }
        
    }

    /**
     * 
     * 
     */
    public void inspectUnit()
    {
        if ("Player Unit".Equals(selectedObject.tag) || "Enemy Unit".Equals(selectedObject.tag))
        {
            inspectingUnit = true;
            Debug.Log("Inspecting Unit");
        }

    }

    /**
     * 
     * 
     */
    public void moveUnitTo(Vector2 destination)
    {
        if ("Player Unit".Equals(selectedObject.tag))
        {
            selectedObject.transform.position = destination;
            unitBeingPlaced = selectedObject;
            selectedObject.GetComponent<PlayerUnitController>().position = cursorPosition;
            placingUnit = true;
            if (cursor.GetComponent<CursorController>().objectInTile != null && 
                "Enemy Unit".Equals(cursor.GetComponent<CursorController>().objectInTile.tag))
            {
                movingUnit = false;
                BattleData battleData = (BattleData)ScriptableObject.CreateInstance("BattleData");
                battleData.battleMapData = getBattleLevelData();
                battleData.playerGroup = selectedObject.GetComponent<PlayerUnitController>().unitgroup;
                battleData.enemyGroup = cursor.GetComponent<CursorController>().objectInTile.GetComponent<EnemyUnitController>().unitgroup;
                GameObject.Find("Game Data Controller").GetComponent<GameDataController>().gameData.battleData = battleData;
                string activeSceneName = SceneManager.GetActiveScene().name;
                Debug.Log("Starting battle");
                string battleSceneName = "Flat Grass Battle Map";
                battleData.sceneName = battleSceneName;
                SceneManager.LoadScene(battleSceneName, LoadSceneMode.Additive);
                SceneManager.UnloadSceneAsync(activeSceneName);
            }
        }
        movingUnit = false;
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
            case "Player Unit":
                playerUnitUIPanel.SetActive(true);
                panelUIActive = true;
                Debug.Log("selected Player Unit");
                break;
            case "Enemy Unit":
                enemyUnitUIPanel.SetActive(true);
                panelUIActive = true;
                Debug.Log("selected Enemy Unit");
                break;
            default:
                Debug.Log("Nothing to select");
                break;
        }
    }

    private BattleMapData getBattleLevelData()
    {
        BattleMapData battleLevelData = null;
        MapTile battleTile = levelData.getMapTileFromXY(cursorPosition);
        if (GameObject.Find("Level Controller") != null) {
            battleLevelData = GameObject.Find("Level Controller").GetComponent<LevelController>().getCorrectBattleMap(battleTile.groundType);
        }
        return battleLevelData;
    }


}
