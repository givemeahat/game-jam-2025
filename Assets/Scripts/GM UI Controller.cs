using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GMUIController : MonoBehaviour
{
    public bool isConversing = false;
    public string[] currentConversation;

    public TextMeshProUGUI cherryText;
    public GameObject menu;

    //---dialogue---
    public GameObject dialogueBox;
    public TextMeshProUGUI dialogueBoxName;
    public TextMeshProUGUI dialogueLines;
    public int dialogueCount;

    public void UpdateCherryText(int _cherryCount)
    {
        cherryText.text = "Cherries: " + _cherryCount;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && isConversing)
        {
            ProgressConversation();
        }
    }

    private void ProgressConversation()
    {
        if (dialogueCount == currentConversation.Length-1)
        {
            dialogueBox.SetActive(false);
            isConversing = false;
            dialogueCount = 0;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().enabled = true;
        }
        else if (dialogueCount < currentConversation.Length)
        {
            dialogueCount++;
            PrintText();
        }
    }

    public void OpenMenu()
    {
        menu.SetActive(true);
    }

    public void RunConversation(string[] _lines, string _name)
    {
        isConversing = true;
        currentConversation = _lines;
        dialogueBoxName.text = _name;
        PrintText();
        dialogueBox.SetActive(true);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().enabled = false;
    }

    public void PrintText()
    {
        dialogueLines.GetComponent<TextMeshProEffect>().Play();
        dialogueLines.text = currentConversation[dialogueCount];
    }
}
