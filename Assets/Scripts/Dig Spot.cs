using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigSpot : MonoBehaviour
{
    public int digSpotID;
    public GM gameManager;

    private void Update()
    {
        if (gameManager == null) gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GM>();
        if (gameManager.brokenWalls.Contains(digSpotID))
        {
            Destroy(this.gameObject);
            Debug.Log("Destroyed broken wall");
            this.enabled = false;

        }
    }
    public void Dig()
    {
        gameManager.brokenFloors.Add(digSpotID);
        Debug.Log("added to broken floor list");
        Destroy(this.gameObject);
    }
}
