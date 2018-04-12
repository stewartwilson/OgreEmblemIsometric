using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class EditorInputController : MonoBehaviour
{
    public float cursorDelay;
    public bool canCursorMove;
    public float nextCursorMoveAllowed;
    //Holds cursor object
    public GameObject cursor;
    public GridPosition cursorPosition;
    public int maxCursorXPos;
    public int maxCursorYPos;

    private LevelData levelData;

    private void Start()
    {
        levelData = GameObject.Find("Level Editor Controller").GetComponent<LevelEditorController>().levelData;
        maxCursorXPos = levelData.getMaxX();
        maxCursorYPos = levelData.getMaxY();
    }

    private void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        bool enter = Input.GetButtonDown("Submit");
        bool back = Input.GetButtonDown("Back");

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

            updateCursor();
                
                

            nextCursorMoveAllowed = Time.time + cursorDelay;
        }
        
    }

    /**
     * 
     * 
     */
    public void updateCursor()
    {
        LevelData levelData = GameObject.Find("Level Editor Controller").GetComponent<LevelEditorController>().levelData;
        MapTile mt = levelData.getMapTileFromXY(cursorPosition);
        cursor.transform.position = IsometricHelper.gridToGamePostion(mt.position);
        
    }

   

    private BattleMapData getBattleLevelData()
    {
        BattleMapData battleLevelData = null;
        MapTile battleTile = levelData.getMapTileFromXY(cursorPosition);
        if (GameObject.Find("Level Controller") != null) {
            //battleLevelData = GameObject.Find("Level Controller").GetComponent<LevelController>().levelData.getCorrectBattleMap(battleTile.groundType);
        }
        return battleLevelData;
    }


}
