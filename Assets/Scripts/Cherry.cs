using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Cherry : MonoBehaviour
{
    private void Awake()
    {
        if (GM.singleton.pickedUpCherries.Contains(this.gameObject.GetInstanceID()))
        {
            Destroy(this.gameObject);
        }
    }

    public void OnPickup()
    {
        GM.singleton.pickedUpCherries.Add(this.gameObject.GetInstanceID());
        Destroy(this.gameObject);
    }
}
