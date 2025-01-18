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

    public GameObject loadingScreen;

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
    }
    public void RunLine(string _line, string _name)
    {
        dialogueCount = 0;
        isConversing = true;
        currentConversation.Add(_line);
        dialogueBoxName.text = _name;
        dialogueBox.SetActive(true);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().enabled = false;
        dialogueLines.GetComponent<TextMeshProEffect>().Play();
        dialogueLines.text = currentConversation[dialogueCount];
    }
    public void PrintText()
    {
        dialogueLines.GetComponent<TextMeshProEffect>().Play();
        dialogueLines.text = currentConversation[dialogueCount];
        dialogueCount++;
    }
}
