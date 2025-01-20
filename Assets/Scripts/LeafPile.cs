using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafPile : MonoBehaviour
{
    public Sprite revealSprite;
    public GameObject buriedItem;
    public int cherryID;

    public int digSpotID;
    public GM gameManager;

    private void Update()
    {
        if (gameManager == null) gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GM>();
        if (gameManager.brokenFloors.Contains(digSpotID))
        {
            this.GetComponent<SpriteRenderer>().sprite = revealSprite;
            Debug.Log("Destroyed broken wall");
            this.enabled = false;
        }
    }
    public void DigIn()
    {
        this.GetComponentInChildren<Collider2D>().enabled = false;
        this.GetComponent<SpriteRenderer>().sprite = revealSprite;
        if (buriedItem != null)
        {
            if (buriedItem.tag == "Bandana" && GameObject.FindGameObjectWithTag("GameController").GetComponent<GM>().hasObtainedBear) return;
            GameObject _obj = Instantiate(buriedItem) as GameObject;
            _obj.transform.position = this.gameObject.transform.position;
            if (_obj.tag == "Cherry") _obj.GetComponent<Cherry>().ID = cherryID;
        }
        gameManager.brokenFloors.Add(digSpotID);
        Debug.Log("added to broken floor list");
        //Destroy(this.gameObject);
    }

}
