using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {

    public GameObject dialogBox;

    public Text currentText;
    public Button answerButton1;
    public Button answerButton2;
    public Button answerButton3;

    public TextAsset textFile;
    public string[] textLines;

    public int currentLine;
    public int endLine;

    public bool inDialog;

    public bool isQuestioning;

    private void Awake()
    {
        inDialog = true;
        isQuestioning = false;
        answerButton1.gameObject.SetActive(false);
        answerButton2.gameObject.SetActive(false);
        answerButton3.gameObject.SetActive(false);
    }

    // Use this for initialization
    void Start () {
		if(textFile != null)
        {
            textLines = textFile.text.Split('\n');
            endLine = textLines.Length - 1;
        }
	}

    private void Update()
    {
        string newLine = textLines[currentLine];
        if (newLine.Contains("[Question]"))
        {
            isQuestioning = true;
            string[] questionArray = newLine.Split('|');
            currentText.text = questionArray[0].Substring(10);
            answerButton1.gameObject.SetActive(true);
            answerButton1.GetComponentInChildren<Text>().text = questionArray[1];
            answerButton2.gameObject.SetActive(true);
            answerButton2.GetComponentInChildren<Text>().text = questionArray[2];
            answerButton3.gameObject.SetActive(true);
            answerButton3.GetComponentInChildren<Text>().text = questionArray[3];

        }
        else
        {
            isQuestioning = false;
            currentText.text = textLines[currentLine];
        }
    }

    public void nextLine()
    {
        if (currentLine < endLine)
        {
            currentLine++;
        }
        else
        {
            closeDialogBox();
        }
    }

    public void closeDialogBox()
    {
        if(dialogBox != null)
        {
            inDialog = false;
            dialogBox.SetActive(false);
        }
    }

    public void openDialogBox()
    {
        if (dialogBox != null)
        {
            inDialog = true;
            dialogBox.SetActive(true);
        }
    }

}
