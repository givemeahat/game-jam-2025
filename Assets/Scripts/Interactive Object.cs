using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    public GameObject parent;
    
    public void FeedThroughMethod()
    {
        if (parent.tag == "DigSpot")
        {
            parent.GetComponent<DigSpot>().Dig();
        }
        if (parent.tag == "BreakableWall")
        {
            parent.GetComponent<BreakableWall>().Break();
        }
        if (parent.tag == "Dog")
        {
            parent.GetComponent<Dog>().ConversationManager();
        }
        if (parent.tag == "Bear")
        {
            parent.GetComponent<Bear>().ConversationManager();
        }
        if (parent.tag == "Dragon")
        {
            parent.GetComponent<Dragon>().ConversationManager();
        }
        /*if (parent.tag == "Cat")
        {
            parent.GetComponent<Cat>(SomeMethod());
        }*/
    }
}
