using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{
    public GM gm;

    public List<string> meetDialogue;
    public List<string> questFinishedDialogue;
    public List<string> finalDogDialogue;


    public bool hasFinishedQuest;

    private void Awake()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GM>();
    }

    public void ConversationManager()
    {
        if (!gm.hasTalkedToDog)
        {
            gm.UIController.RunConversation(meetDialogue, "Bongo");
            gm.currentQuest = GM.Questline.DOG;
            gm.hasTalkedToDog = true;
        }
        if (gm.finishedDogQuest)
        {
            gm.UIController.RunConversation(questFinishedDialogue, "Bongo");
        }
    }


}
