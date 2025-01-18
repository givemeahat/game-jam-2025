using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GMUIController : MonoBehaviour
{
    public GM gm;
    public bool isConversing = false;
    public string[] currentConversation;

    public TextMeshProUGUI cherryText;
    public GameObject menu;

    //---dialogue---
    public GameObject dialogueBox;
    public TextMeshProUGUI dialogueBoxName;
    public TextMeshProUGUI dialogueLines;
    public int dialogueCount;

    private void Awake()
    {
        gm = GetComponent<GM>();
    }

    public void UpdateCherryText(int _cherryCount)
    {
        cherryText.text = "Cherries: " + _cherryCount;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && isConversing)
        {
            ProgressConversation();
            dialogueCount++;
        }
    }

    private void ProgressConversation()
    {
        GameObject _player = GameObject.FindGameObjectWithTag("Player");

        if (gm.currentQuest == GM.Questline.DOG && gm.hasTalkedToDog && !gm.hasObtainedDog && dialogueCount == 5)
        {
            gm.AddDog();
            dialogueBoxName.text = " ";
            Destroy(_player.GetComponent<Player>().currentInteractive.transform.parent.gameObject);
        }
        if (dialogueCount == currentConversation.Length)
        {
            dialogueBox.SetActive(false);
            isConversing = false;
            dialogueCount = 0;
            _player.GetComponent<Player>().enabled = true;
        }
        else if (dialogueCount < currentConversation.Length)
        {
            PrintText();
        }
    }

    public void ToggleMenu()
    {
        menu.SetActive(!menu.activeInHierarchy);
    }

    public void RunConversation(string[] _lines, string _name)
    {
        dialogueCount = 0;
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
