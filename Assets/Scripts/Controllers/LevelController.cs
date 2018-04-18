using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {
    public string levelName;
    public LevelData levelData;
    public LevelUnitData unitData;
    public bool generateMapObjects;
    public bool updateLevelData;
    public bool updateLevelDataBasic;
    public GameObject selectedUnit;
    public GameObject movesContainer;
    public bool displayingMoves;
    public List<GridPosition> possibleMoves;
    public bool initiatingBattle;

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
        else if (updateLevelDataBasic)
        {
            levelData.generateBasicFlatMap(10,10);
        }
        if (generateMapObjects)
        {
            InstantiateLevelMap();
        }
        initiatingBattle = false;
    }

    private void Start()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(levelData.sceneName));
        levelName = levelData.sceneName;
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

    public void moveUnitGroup(GridPosition destination)
    {
        selectedUnit.GetComponent<PlayerUnitController>().position = destination;
        foreach (LevelUnitPosition lup in unitData.unitPositions)
        {
            UnitGroup ug = unitData.getUnitAt(destination);
            if (ug != null && ug.isEnemy)
            {
                if (!initiatingBattle)
                {
                    initiatingBattle = true;
                    InitiateBattleSequence(selectedUnit.GetComponent<PlayerUnitController>().unitgroup, ug, destination, destination);
                }
            }
        }
    }

    private void InstantiateMovesDisplay(List<GridPosition> moves)
    {
        int count = 0;
        foreach (GridPosition pos in moves)
        {
            count++;
            GameObject go = null;
            UnitGroup unitGroup = unitData.getUnitAt(pos);
            if (unitGroup == null)
            {
                go = (GameObject)Instantiate(Resources.Load("Possible Move"));
            }
            else
            {
                if(unitGroup.isEnemy)
                {
                    go = (GameObject)Instantiate(Resources.Load("Enemy Present"));
                } else
                {
                    go = (GameObject)Instantiate(Resources.Load("Possible Move"));
                }
            }
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
            GameObject go = (GameObject)Instantiate(Resources.Load("Tile"));
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

    

    public List<GridPosition> getPossibleMovement(GridPosition currentPos, int maxMovement)
    {
        List<GridPosition> possibleMoves = new List<GridPosition>();
        foreach(MapTile mt in levelData.map)
        {
            //TODO account for elevation difference
            if(mt.safeToStand &&
                IsometricHelper.distanceBetweenGridPositions(mt.position, currentPos) <= maxMovement && 
                (unitData.getUnitAt(mt.position) == null || unitData.getUnitAt(mt.position).isEnemy))
            {
                possibleMoves.Add(mt.position);
            }
        }

        return possibleMoves;
    }

    private void InitiateBattleSequence(UnitGroup player, UnitGroup enemy, GridPosition playerPos, GridPosition enemyPos)
    {
        BattleData battleData = (BattleData)ScriptableObject.CreateInstance("BattleData");
        battleData.battleMapData = getBattleLevelData(playerPos, enemyPos);
        battleData.playerGroup = player;
        battleData.enemyGroup = enemy;
        GameObject.Find("Game Data Controller").GetComponent<GameDataController>().gameData.battleData = battleData;
        string activeSceneName = SceneManager.GetActiveScene().name;
        Debug.Log("Starting battle");
        string battleSceneName = "Flat Grass Battle Map";
        battleData.sceneName = battleSceneName;
        SceneManager.LoadScene(battleSceneName, LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(levelName);
    }

    private BattleMapData getBattleLevelData(GridPosition playerPos, GridPosition enemyPos)
    {
        BattleMapData battleLevelData = null;
        MapTile battleTilePlayer = levelData.getMapTileFromXY(playerPos);
        MapTile battleTileEnemy = levelData.getMapTileFromXY(playerPos);
        battleLevelData = levelData.getCorrectBattleMap(battleTilePlayer, battleTileEnemy);
        return battleLevelData;
    }

}
