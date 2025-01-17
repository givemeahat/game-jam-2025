using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour
{
    private Player player;
    public int cherryCount;
    public GameObject[] PartyMembers;

    void Awake()
    {
        DontDestroyOnLoad(this);
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
