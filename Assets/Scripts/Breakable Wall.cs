using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    public int ID;
    public GM gameManager;
    private void Update()
    {
        if (gameManager == null) gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GM>();
        if (gameManager.brokenWalls.Contains(ID))
        {
            Destroy(this.gameObject);
            Debug.Log("Destroyed broken wall");
            this.enabled = false;
        }
    }
    public void Break()
    {
        gameManager.brokenWalls.Add(ID);
        Debug.Log("added to broken wall list");
        Destroy(this.gameObject);
    }
}
