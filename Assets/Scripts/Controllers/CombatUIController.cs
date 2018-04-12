using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatUIController : MonoBehaviour {

    public BattleData battleData;
    public List<Text> playerHealthText;
    public List<Image> playerHealthBars;
    public List<Image> enemyHealthBars;
    public UnitGroup playerGroup;
    public UnitGroup enemyGroup;

    public float healthBarRightValue;

    public float enemyHealthXPos = 110;
    public float playerHealthXPos = 0;

    private void Start()
    {
        if (battleData == null)
        {
            battleData = GameObject.Find("Game Data Controller").GetComponent<GameDataController>().gameData.battleData;
        }
        playerGroup = battleData.playerGroup;
        enemyGroup = battleData.enemyGroup;
        foreach(Image i in enemyHealthBars)
        {
            i.gameObject.SetActive(false);
        }
        foreach (UnitPosition up in enemyGroup.unitList)
        {
            enemyHealthBars[up.position].gameObject.SetActive(true);
        }
        foreach (Image i in playerHealthBars)
        {
            i.gameObject.SetActive(false);
        }
        foreach (Text i in playerHealthText)
        {
            i.gameObject.SetActive(false);
        }
        foreach (UnitPosition up in playerGroup.unitList)
        {
            playerHealthBars[up.position].gameObject.SetActive(true);
            playerHealthText[up.position].gameObject.SetActive(true);
        }


    }

    void Update()
    {
        foreach (UnitPosition up in enemyGroup.unitList)
        {
            Image unitHealth = enemyHealthBars[up.position];
            float healthPercent = (float)up.unit.health / (float)up.unit.maxHealth;
            float top = unitHealth.rectTransform.offsetMax.y;
            unitHealth.rectTransform.offsetMax = new Vector2(-healthBarRightValue * healthPercent, top);
        }
        foreach (UnitPosition up in playerGroup.unitList)
        {
            Image unitHealth = playerHealthBars[up.position];
            float healthPercent = (float)up.unit.health / (float)up.unit.maxHealth;
            float top = unitHealth.rectTransform.offsetMax.y;
            unitHealth.rectTransform.offsetMax = new Vector2(-healthBarRightValue * healthPercent, top);
            Text unitHealthText = playerHealthText[up.position];
            unitHealthText.text = "" + up.unit.health;
        }
    }

}
