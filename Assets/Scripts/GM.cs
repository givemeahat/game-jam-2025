using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour
{
    private Player player;
    public int cherryCount;
    public GameObject[] PartyMembers;
    private GMUIController UIController;

    void Awake()
    {
        DontDestroyOnLoad(this);
        UIController = this.GetComponent<GMUIController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //locates player gameobject and establishes communication between GM/player scripts
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            player.gm = this;
        }
    }

    public void AddCherry()
    {
        cherryCount++;
        UIController.UpdateCherryText(cherryCount);
    }
}
