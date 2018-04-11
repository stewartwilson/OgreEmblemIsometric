using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatController : MonoBehaviour {

    public BattleData battleData;
    public int playerElevation;
    public int enemyElevation;
    public bool updateLevelData;
    public bool generateMapObjects;
    public float timeBetweenActions;
    public float nextActionAllowed;

    public List<Unit> turnOrder;
    public Unit nextUnitToAct;

    public int turnNumber;
    public int maxTurns;

    public int enemyNetDamageTaken;
    public int playerNetDamageTaken;

    // Use this for initialization
    void Start () {
        if(battleData == null)
        {
            battleData = GameObject.Find("Game Data Controller").GetComponent<GameDataController>().gameData.battleData;
        }
        if (updateLevelData)
        {
            battleData.battleMapData.generateMapFromTextFile();
        }
        if (generateMapObjects)
        {
            InstantiateLevelMap();
        }
        if (GameObject.Find("Game Data Controller") != null)
        {
            battleData = GameObject.Find("Game Data Controller").GetComponent<GameDataController>().gameData.battleData;
            instantiateUnitGroups();
        }
        initiateTurnOrder();
        nextUnitToAct = turnOrder[0];
        turnNumber = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(battleData.playerGroup.isDefeated())
        {
            battleData.afterBattle = true;
            battleData.didPlayerWin = false;
            returnToLevelScene();

        } else if(battleData.enemyGroup.isDefeated())
        {

            battleData.afterBattle = true;
            battleData.didPlayerWin = true;
            returnToLevelScene();
        }
        foreach(Unit u in turnOrder)
        {
            if(u.health <= 0)
            {
                u.canAct = false;
            }
        }
        
        if (turnNumber <= maxTurns) {
            if (!(Time.time < nextActionAllowed))
            {
                UnitAction nextAction = null;
                Debug.Log(nextUnitToAct + " taking turn " + nextUnitToAct.canAct);
                if (nextUnitToAct.canAct)
                {
                    if (nextUnitToAct.isEnemy)
                    {
                        nextAction = nextUnitToAct.returnAction(battleData.playerGroup, battleData.enemyGroup);
                    }
                    else
                    {
                        nextAction = nextUnitToAct.returnAction(battleData.enemyGroup, battleData.playerGroup);

                    }
                    Debug.Log(nextAction.target + ": " + ((DamagingAction)nextAction).damage);
                    resolveAction(nextAction);
                }
                int turn = turnOrder.IndexOf(nextUnitToAct);

                if (turn < turnOrder.Count - 1)
                {
                    nextUnitToAct = turnOrder[turn + 1];
                    
                } else
                {
                    nextUnitToAct = turnOrder[0];
                    turnNumber++;
                }
                if (nextUnitToAct.canAct)
                {
                    nextActionAllowed = Time.time + timeBetweenActions;
                }
                else
                {
                    nextActionAllowed = Time.time;
                }
            }
        }
        else
        {
            battleData.afterBattle = true;
            if (playerNetDamageTaken < enemyNetDamageTaken)
            {
                battleData.didPlayerWin = true;
            } else {
                battleData.didPlayerWin = false;
            }
            returnToLevelScene();
        }
    }

    public void resolveAction(UnitAction action)
    {
        if(action is DamagingAction)
        {
            Debug.Log("Applying damage");
            action.target.takeDamage(((DamagingAction)action).damage);
            if(action.target.isEnemy)
            {
                enemyNetDamageTaken += ((DamagingAction)action).damage;
            }
            else
            {
                playerNetDamageTaken += ((DamagingAction)action).damage;
            }
        }
        else if (action is HealingAction)
        {
            Debug.Log("Applying healing");
            action.target.takeHealing(((HealingAction)action).healing);
            if (action.target.isEnemy)
            {
                enemyNetDamageTaken -= ((HealingAction)action).healing;
            }
            else
            {
                playerNetDamageTaken -= ((HealingAction)action).healing;
            }
        }
        else
        {

        }
    }

    public void returnToLevelScene()
    {
        LevelData levelData = GameObject.Find("Game Data Controller").GetComponent<GameDataController>().gameData.levelData;
        SceneManager.LoadScene(levelData.sceneName, LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(battleData.sceneName);
    }

    public void initiateTurnOrder()
    {
        turnOrder = new List<Unit>();
        foreach(UnitPosition up in battleData.playerGroup.unitList)
        {
            turnOrder.Add(up.unit);
        }
        foreach (UnitPosition up in battleData.enemyGroup.unitList)
        {
            turnOrder.Add(up.unit);
        }
        //Create sort list based on a units Initiation stat in case of a tie use dexterity
        //If dexterity ties randomly assign
        turnOrder.Sort(
            delegate (Unit u1, Unit u2)
            {
                if (u1.initiation == u2.initiation)
                {
                    if(u1.initiation == u2.initiation)
                    {
                        return Random.Range(0, 1);
                    }
                    return u1.dexterity.CompareTo(u2.dexterity);
                }
                else {
                    return u1.initiation.CompareTo(u2.initiation);
                }
            }
        );
    }

    public void updateTurnOrder()
    {
        
    }

    public void instantiateUnitGroups()
    {
        UnitGroup playerGroup = battleData.playerGroup;
        UnitGroup enemyGroup = battleData.enemyGroup;

        foreach(UnitPosition up in playerGroup.unitList)
        {
            GridPosition unitPos = IsometricHelper.battleCoordToPostion(up.position, true);
            Debug.Log("Position " + unitPos.x + ", " + unitPos.y);
            GameObject go = (GameObject)Instantiate(Resources.Load("Monk"));
            go.transform.SetParent(GameObject.Find("Player Units").transform);
            go.transform.position = IsometricHelper.gridToGamePostion(unitPos);
            go.GetComponent<SpriteRenderer>().sortingOrder = IsometricHelper.getTileSortingOrder(unitPos);
            go.name = up.unit.unitName;
            go.GetComponent<CombatUnitController>().facing = Facing.Back;
        }
        foreach (UnitPosition up in enemyGroup.unitList)
        {
            GridPosition unitPos = IsometricHelper.battleCoordToPostion(up.position, false);
            Debug.Log("Position " + (int)unitPos.x + ", " + (int)unitPos.y);
            GameObject go = (GameObject)Instantiate(Resources.Load("Monk"));
            go.transform.SetParent(GameObject.Find("Enemy Units").transform);
            go.transform.position = IsometricHelper.gridToGamePostion(unitPos);
            go.GetComponent<SpriteRenderer>().sortingOrder = IsometricHelper.getTileSortingOrder(unitPos);
            go.name = up.unit.unitName;
            go.GetComponent<CombatUnitController>().facing = Facing.Forward;
        }
    }

    private void InstantiateLevelMap()
    {
        int count = 0;
        foreach (MapTile mt in battleData.battleMapData.map)
        {
            count++;
            GameObject go = (GameObject)Instantiate(Resources.Load("Grass Tile"));
            if (mt.position.x < 4 && mt.position.y < 3)
            {
                go.transform.SetParent(GameObject.Find("Player Tiles").transform);
            }
            else
            {
                go.transform.SetParent(GameObject.Find("Enemy Tiles").transform);
            }
            go.transform.position = IsometricHelper.gridToGamePostion(mt.position);
            go.GetComponent<SpriteRenderer>().sortingOrder = IsometricHelper.getTileSortingOrder(mt.position);
            go.name = "Tile " + count;

        }
    }
}
