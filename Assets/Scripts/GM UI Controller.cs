using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GMUIController : MonoBehaviour
{
    public GM gm;
    public bool isConversing = false;
    public List<string> currentConversation;

    public TextMeshProUGUI cherryText;

    public GameObject menu;
    public GameObject tutorialImage;

    public GameObject loadingScreen;

    public GameObject cherryCountObj;
    public TextMeshProUGUI cherryLoadScreenText;

    //---dialogue---
    public GameObject dialogueBox;
    public TextMeshProUGUI dialogueBoxName;
    public TextMeshProUGUI dialogueLines;
    public int dialogueCount;

    bool progressToBongoCutscene;

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
        }
    }

    private void ProgressConversation()
    {
        GameObject _player = GameObject.FindGameObjectWithTag("Player");
        /*if (gm.currentQuest == GM.Questline.DOG)
        {
            ManageDogDialogue(_player);
        }*/
        if (gm.hasTalkedToDog && !gm.hasObtainedDog && dialogueCount == 5)
        {
            gm.AddDog();
            dialogueBoxName.text = " ";
            Destroy(_player.GetComponent<Player>().currentInteractive.transform.parent.gameObject);
        }
        if (gm.hasTalkedToDog && gm.hasObtainedDog && gm.finishedDogQuest && dialogueCount == 2)
        {
            Debug.Log("Hello???");
            gm.DogFadeCutscene();
        }
        if (dialogueCount == currentConversation.Count)
        {
            dialogueBox.SetActive(false);
            isConversing = false;
            _player.GetComponent<Player>().enabled = true;
            dialogueCount = 0;
            currentConversation.Clear();
            if (progressToBongoCutscene)
            {
                gm.LoadScene(5);
            }
        }
        else if (dialogueCount < currentConversation.Count)
        {
            PrintText();
        }
    }

    private void ManageDogDialogue(GameObject _player)
    {

    }

    public void ToggleMenu()
    {
        menu.SetActive(!menu.activeInHierarchy);
    }

    public void RunConversation(List<string> _lines, string _name)
    {
        dialogueCount = 0;
        isConversing = true;
        currentConversation = _lines;
        dialogueBoxName.text = _name;
        dialogueBox.SetActive(true);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().enabled = false;
        dialogueLines.GetComponent<TextMeshProEffect>().Play();
        dialogueLines.text = currentConversation[dialogueCount];
        dialogueCount++;
    }
    public void RunKeyLine()
    {
        dialogueCount = 0;
        isConversing = true;
        currentConversation.Clear();
        currentConversation.Add("<i>Wormy has obtained the pantry key!</i>");
        dialogueLines.text = currentConversation[0];
        dialogueBoxName.text = " ";
        dialogueBox.SetActive(true);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().enabled = false;
        dialogueLines.GetComponent<TextMeshProEffect>().Play();
        dialogueCount++;
        progressToBongoCutscene = true;
        //dialogueLines.text = currentConversation[dialogueCount];
    }
    public void RunBandanaLine()
    {
        dialogueCount = 0;
        isConversing = true;
        currentConversation.Add("<i>Wormy has unearthed Daisy's bandana!</i>");
        dialogueBoxName.text = " ";
        dialogueBox.SetActive(true);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().enabled = false;
        dialogueLines.GetComponent<TextMeshProEffect>().Play();
        //dialogueLines.text = currentConversation[dialogueCount];
    }
    public void RunBootsLine()
    {
        dialogueCount = 0;
        isConversing = true;
        currentConversation.Add("<i>Wormy has located Boots' boots!</i>");
        dialogueBoxName.text = " ";
        dialogueBox.SetActive(true);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().enabled = false;
        dialogueLines.GetComponent<TextMeshProEffect>().Play();
        //dialogueLines.text = currentConversation[dialogueCount];
    }
    public void PrintText()
    {
        dialogueLines.GetComponent<TextMeshProEffect>().Play();
        dialogueLines.text = currentConversation[dialogueCount];
        dialogueCount++;
    }
}
