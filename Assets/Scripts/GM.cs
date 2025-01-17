using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour
{
    private Player player;
    public int cherryCount;
    public GameObject[] PartyMembers;
    public GMUIController UIController;

    //--- game progression variables ---
    public bool hasTalkedToDog;
    public bool finishedDogQuest;
    public bool hasObtainedDog;

    public List<GameObject> companions;

    public GameObject[] companionPrefabs;

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

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape))
        {
            UIController.OpenMenu();
        }
    }

    public void AddDog()
    {
        hasObtainedDog = true;
    }

    public void AddCherry()
    {
        cherryCount++;
        UIController.UpdateCherryText(cherryCount);
    }
}
