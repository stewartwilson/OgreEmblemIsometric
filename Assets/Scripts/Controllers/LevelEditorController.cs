using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEditorController : MonoBehaviour {
    public LevelData levelData;
    public List<BattleMapData> possibleBattleList;
    public LevelUnitData unitData;
    public bool generateMapObjects;
    public bool updateLevelData;
    public bool updateLevelDataBasic;

    // Use this for initialization
    void Awake() {
        if (levelData == null)
        {
            levelData = GameObject.Find("Game Data Controller").GetComponent<GameDataController>().gameData.levelData;
        }
        if (updateLevelData)
        {
            levelData.generateMapFromTextFile();
        }
        if (updateLevelDataBasic)
        {
            levelData.generateBasicFlatMap(10,10);
        }
        if (generateMapObjects)
        {
            InstantiateLevelMap();
        }
    }

    private void Start()
    {
    }

    // Update is called once per frame
    void Update () {
        
    }

    private void InstantiateLevelMap()
    {
        int count = 0;
        foreach(MapTile mt in levelData.map)
        {
            count++;
            GameObject go = (GameObject)Instantiate(Resources.Load("Pixel Tile"));
            go.transform.SetParent(GameObject.Find("Tiles").transform);
            go.transform.position = IsometricHelper.gridToGamePostion(mt.position);
            go.GetComponent<SpriteRenderer>().sortingOrder = IsometricHelper.getTileSortingOrder(mt.position);
            go.name = "Tile " + count;
            
        }
    }

    public void UpdateLevelMap()
    {
        foreach (Transform child in GameObject.Find("Tiles").transform)
        {
            Destroy(child.gameObject);
        }
        int count = 0;
        foreach (MapTile mt in levelData.map)
        {
            count++;
            GameObject go = (GameObject)Instantiate(Resources.Load("Pixel Tile"));
            go.transform.SetParent(GameObject.Find("Tiles").transform);
            go.transform.position = IsometricHelper.gridToGamePostion(mt.position);
            go.GetComponent<SpriteRenderer>().sortingOrder = IsometricHelper.getTileSortingOrder(mt.position);
            go.name = "Tile " + count;

        }
    }

    public void ExportLevelMap()
    {
        StreamWriter sr = File.CreateText("Assets/Text Files/New Level.txt");
        Debug.Log(sr.ToString());
        string line = "";
        foreach (MapTile mt in levelData.map)
        {

            line += Convert.ToInt32(mt.groundType) + "," + mt.position.x + "," + mt.position.y + "," + mt.position.elevation + "," + Convert.ToInt32(mt.safeToStand) + "|";
        }
        line.TrimEnd('|');
        sr.WriteLine(line);
        sr.Close();
        
    }

    public BattleMapData getCorrectBattleMap(GroundType groundType)
    {
        foreach(BattleMapData bmd in possibleBattleList)
        {
            if(bmd.groundType == groundType)
            {
                return bmd;
            }
        }
        return possibleBattleList[0];
    }

    public List<GridPosition> getPossibleMovement(GridPosition currentPos, int maxMovement)
    {
        List<GridPosition> possibleMoves = new List<GridPosition>();
        foreach(MapTile mt in levelData.map)
        {
            //TODO account for elevation difference
            if(mt.safeToStand &&
                IsometricHelper.distanceBetweenGridPositions(mt.position, currentPos) <= maxMovement &&
                unitData.isSquareEmpty(mt.position) )
            {
                possibleMoves.Add(mt.position);
            }
        }

        return possibleMoves;
    }

    

}
