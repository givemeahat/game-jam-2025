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

    public void DogConversationManager()
    {
        if (!gm.hasTalkedToDog)
        {
            gm.UIController.RunConversation(meetDialogue, "Boots");
            gm.currentQuest = GM.Questline.DOG;
            gm.hasTalkedToDog = true;
        }
        if (gm.finishedDogQuest)
        {
            gm.UIController.RunConversation(questFinishedDialogue, "Boots");
        }
    }


}
