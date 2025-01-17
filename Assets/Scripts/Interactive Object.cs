using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    public GameObject parent;
    
    public void FeedThroughMethod()
    {
        if (parent.tag == "Dog")
        {
            parent.GetComponent<Dog>().DogConversationManager();
        }
        /*if (parent.tag == "Bear")
        {
            parent.GetComponent<Bear>(SomeMethod());
        }
        if (parent.tag == "Dragon")
        {
            parent.GetComponent<Dragon>(SomeMethod());
        }
        if (parent.tag == "Cat")
        {
            parent.GetComponent<Cat>(SomeMethod());
        }*/
    }
}
