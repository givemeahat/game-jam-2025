using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonoBehaviour
{
    public GM gm;

    public string[] meetDialogue;
    public string[] questFinishedDialogue;
    public string[] finalDragonDialogue;


    public bool hasFinishedQuest;

    private void Awake()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GM>();
    }

    public void ConversationManager()
    {
        if (!gm.hasTalkedToDragon)
        {
            gm.UIController.RunConversation(meetDialogue, "Boots");
            gm.currentQuest = GM.Questline.DRAGON;
            gm.hasTalkedToDog = true;
        }
        if (gm.finishedDragonQuest)
        {
            gm.UIController.RunConversation(questFinishedDialogue, "Boots");
        }
    }


}
