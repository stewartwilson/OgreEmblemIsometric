using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Keyboard : MonoBehaviour {
    public List<TextAsset> keyboardTypes;
    public List<GameObject> keys;
    public string[] row1;
    public string[] row2;
    public string[] row3;
    public string spriteType;

    public string userInput;
    public Text characterName;
    public int maxNameLength;
    public bool isUpper = true;

    // Use this for initialization
    void Awake()
    {
        userInput = "";
        genrateRowsFromFile(0);
        updateKeysFromRows();
        SceneManager.LoadScene("Persistence", LoadSceneMode.Additive);

    }

    public void genrateRowsFromFile(int index)
    {
        string[] textLines;
        if (keyboardTypes[index] != null)
        {
            textLines = keyboardTypes[index].text.Split('\n');
            int endLine = textLines.Length - 1;
            for (int i = 0; i <= endLine; i++)
            {
                
                string newLine = textLines[i];
                string[] keyArray = newLine.Split('|');
                if (i == 0)
                {
                    row1 = keyArray;
                }
                else if (i == 1)
                {
                    row2 = keyArray;
                }
                else 
                {
                    row3 = keyArray;
                }


            }
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (userInput != null && userInput.Length > 0)
        {
            characterName.text = userInput;
        } else
        {
            characterName.text = "";
        }
    }

    public void updateKeysFromRows()
    {
        int count = 0;
        foreach (string c in row1)
        {
            string value = c;
            keys[count].name = value;
            keys[count].GetComponentsInChildren<Text>()[0].text = value;
            count++;
        }
        foreach (string c in row2)
        {
            string value = c;
            keys[count].name = value;
            keys[count].GetComponentsInChildren<Text>()[0].text = value;
            count++;
        }
        foreach (string c in row3)
        {
            string value = c;
            keys[count].name = value;
            keys[count].GetComponentsInChildren<Text>()[0].text = value;
            count++;
        }
    }

    public void switchCase()
    {
        for(int i = 0; i <row1.Length; i++)
        {
            string tempValue = row1[i];
            if (!(tempValue.StartsWith("Ent") || tempValue.Equals("Up"))) {
                if(isUpper)
                {
                    row1[i] = tempValue.ToLower();
                } else
                {
                    row1[i] = tempValue.ToUpper();
                }
            }
        }
        for (int i = 0; i < row2.Length; i++)
        {
            string tempValue = row2[i];
            if (!(tempValue.StartsWith("Ent") || tempValue.Equals("Up")))
            {
                if (isUpper)
                {
                    row2[i] = tempValue.ToLower();
                }
                else
                {
                    row2[i] = tempValue.ToUpper();
                }
            }
        }
        for (int i = 0; i < row3.Length; i++)
        {
            string tempValue = row3[i];
            if (!(tempValue.StartsWith("Ent") || tempValue.Equals("Up")))
            {
                if (isUpper)
                {
                    row3[i] = tempValue.ToLower();
                }
                else
                {
                    row3[i] = tempValue.ToUpper();
                }
            }
            if(tempValue.Equals("Up"))
            {
                row3[i] = "Lwr";
            } else if (tempValue.Equals("Lwr"))
            {
                row3[i] = "Up";
            }
        }

        isUpper = !isUpper;
    }

    public void addCharacterToString()
    {
        string buttonName = EventSystem.current.currentSelectedGameObject.name;
        if(buttonName.StartsWith("Ent") || buttonName.Equals("Up"))
        {

        } else
        {
            if (userInput.Length < maxNameLength)
            {
                userInput += buttonName;
            }
        }
    }

    public void clickEnter()
    {
        GameObject.Find("Character Creator Controller").GetComponent<CharacterCreatorController>().createCharacter(userInput);
        
    }

    public void clickBackspace()
    {
        if (userInput.Length > 0)
        {
            userInput = userInput.Remove(userInput.Length - 1);
        } 
    }

    public void clickShift()
    {
        switchCase();
        updateKeysFromRows();
    }
}
