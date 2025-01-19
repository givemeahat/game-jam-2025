using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : MonoBehaviour
{
    public GM gm;

    public List<string> meetDialogue;
    public List<string> questFinishedDialogue;
    public List<string> finalBearDialogue;


    public bool hasFinishedQuest;

    private void Awake()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GM>();
    }

    public void ConversationManager()
    {
        if (!gm.hasTalkedToBear)
        {
            gm.UIController.RunConversation(meetDialogue, "Daisy");
            gm.currentQuest = GM.Questline.BEAR;
            gm.hasTalkedToDog = true;
        }
        if (gm.finishedBearQuest)
        {
            gm.UIController.RunConversation(questFinishedDialogue, "Daisy");
        }
    }


}
