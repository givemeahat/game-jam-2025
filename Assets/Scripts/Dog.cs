using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{
    public GM gm;

    public string[] meetDialogue;
    public string[] questFinishedDialogue;

    public bool hasFinishedQuest;

    private void Awake()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GM>();
    }

    public void DogConversationManager()
    {
        if (!gm.hasTalkedToDog)
        {
            gm.UIController.RunConversation(meetDialogue, "Doggy");
            gm.hasTalkedToDog = true;
        }
        if (gm.finishedDogQuest)
        {
            gm.UIController.RunConversation(questFinishedDialogue, "Doggy");
        }
    }


}
