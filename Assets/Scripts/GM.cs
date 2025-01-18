using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour
{
    public enum Questline { NONE, DOG, BEAR, DRAGON };
    public Questline currentQuest = Questline.NONE;

    public enum CurrentFollower { NONE, DOG, BEAR, DRAGON };
    public CurrentFollower currentFollower = CurrentFollower.NONE;

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
            UIController.ToggleMenu();
        }
    }

    public void AddDog()
    {
        hasObtainedDog = true;
        currentQuest = Questline.DOG;
        currentFollower = CurrentFollower.DOG;
    }

    public void AddCherry()
    {
        cherryCount++;
        UIController.UpdateCherryText(cherryCount);
    }
}
