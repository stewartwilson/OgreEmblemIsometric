using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {
    public LevelData levelData;
    public List<BattleMapData> possibleBattleList;
    public LevelUnitData unitData;
    public bool generateMapObjects;
    public bool updateLevelData;
    public bool updateLevelDataBasic;
    public GameObject selectedUnit;
    public GameObject movesContainer;
    public bool displayingMoves;
    public List<GridPosition> possibleMoves;

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
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(levelData.sceneName));
        displayingMoves = false;
    }

    // Update is called once per frame
    void Update () {
        if(selectedUnit != null && !displayingMoves)
        {
            GridPosition gp =  selectedUnit.GetComponent<PlayerUnitController>().position;
            int maxMovement = selectedUnit.GetComponent<PlayerUnitController>().maxMovement;
            possibleMoves = getPossibleMovement(gp, maxMovement);
            
            InstantiateMovesDisplay(possibleMoves);
            displayingMoves = true;
        } else if (selectedUnit == null)
        {
            if (displayingMoves) {
                destroyMovesDisplay();
                displayingMoves = false;
            }
        }
    }

    private void InstantiateMovesDisplay(List<GridPosition> moves)
    {
        int count = 0;
        foreach (GridPosition pos in moves)
        {
            count++;
            GameObject go = (GameObject)Instantiate(Resources.Load("Possible Move"));
            Debug.Log(go);
            go.transform.SetParent(movesContainer.transform);
            go.transform.position = IsometricHelper.gridToGamePostion(pos);
            go.GetComponent<SpriteRenderer>().sortingOrder = IsometricHelper.getTileSortingOrder(pos);
            go.name = "Move " + count;

        }
    }

    private void destroyMovesDisplay()
    {
        foreach (Transform child in movesContainer.transform)
        {
            Destroy(child.gameObject);
        }
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

    private void ExportLevelMap()
    {
        int count = 0;
        foreach (MapTile mt in levelData.map)
        {
            count++;
            GameObject go = (GameObject)Instantiate(Resources.Load("Grass Tile"));
            go.transform.SetParent(GameObject.Find("Tiles").transform);
            go.transform.position = IsometricHelper.gridToGamePostion(mt.position);
            go.GetComponent<SpriteRenderer>().sortingOrder = IsometricHelper.getTileSortingOrder(mt.position);
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
