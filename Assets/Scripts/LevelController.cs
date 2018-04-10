using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {
    public LevelData levelData;
    public List<BattleMapData> possibleBattleList;
    public bool generateMapObjects;
    public bool updateLevelData;

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
        if (generateMapObjects)
        {
            InstantiateLevelMap();
        }
        
    }

    private void Start()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(levelData.sceneName));
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
            GameObject go = (GameObject)Instantiate(Resources.Load("Grass Tile"));
            go.transform.SetParent(GameObject.Find("Tiles").transform);
            go.transform.position = IsometricHelper.coordXYToPostion(mt.x, mt.y, mt.elevation);
            go.GetComponent<SpriteRenderer>().sortingOrder = IsometricHelper.getTileSortingOrder(mt.x, mt.y);
            go.name = "Tile " + count;
            
        }
    }

    private void ExportLevelMap()
    {
        int count = 0;
        foreach (MapTile mt in levelData.map)
        {
            count++;
            GameObject go = (GameObject)Instantiate(Resources.Load("Grass Tile"));
            go.transform.SetParent(GameObject.Find("Tiles").transform);
            go.transform.position = IsometricHelper.coordXYToPostion(mt.x, mt.y, mt.elevation);
            go.GetComponent<SpriteRenderer>().sortingOrder = IsometricHelper.getTileSortingOrder(mt.x, mt.y);
            go.name = "Tile " + count;

        }
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

}
