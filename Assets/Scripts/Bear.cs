using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : MonoBehaviour
{
    public GM gm;

    public string[] meetDialogue;
    public string[] questFinishedDialogue;
    public string[] finalBearDialogue;


    public bool hasFinishedQuest;

    private void Awake()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GM>();
    }

    public void DogConversationManager()
    {
        if (!gm.hasTalkedToDog)
        {
            gm.UIController.RunConversation(meetDialogue, "Bear");
            gm.currentQuest = GM.Questline.BEAR;
            gm.hasTalkedToDog = true;
        }
        if (gm.finishedDogQuest)
        {
            gm.UIController.RunConversation(questFinishedDialogue, "Bear");
        }
    }


}
