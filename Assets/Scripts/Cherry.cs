using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Cherry : MonoBehaviour
{
    public int ID;
    public GM gameManager;

    private void Update()
    {
        if (gameManager == null) gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GM>();
        if (gameManager.pickedUpCherries.Contains(ID))
        {
            Destroy(this.gameObject);
            Debug.Log("Destroyed gathered cherry");
        }
    }

    public void OnPickup()
    {
        GM.singleton.pickedUpCherries.Add(ID);
        Debug.Log("added to GM Cherry list");
        Destroy(this.gameObject);
    }
}
