using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GM : MonoBehaviour
{
    public static GM singleton;
    public List<int> pickedUpCherries = new List<int>();
    public List<int> brokenWalls = new List<int>();
    public List<int> brokenFloors = new List<int>();
    public GM() => singleton = this;

    public enum Questline { NONE, DOG, BEAR, DRAGON };
    public Questline currentQuest = Questline.NONE;

    public enum CurrentFollower { NONE, DOG, BEAR, DRAGON };
    public CurrentFollower currentFollower = CurrentFollower.NONE;

    public Vector3 startPosition = new Vector3(-5.73999977f, .2f, -0.0306214802f);
    private Player player;
    public GameObject playerPrefab;
    public int cherryCount;
    public GameObject[] PartyMembers;
    public GMUIController UIController;
    bool isLoading;

    //--- game progression variables ---
    public bool hasTalkedToDog;
    public bool finishedDogQuest;
    public bool hasObtainedDog;
    public bool hasTalkedToBear;
    public bool finishedBearQuest;
    public bool hasObtainedBear;
    public bool hasTalkedToDragon;
    public bool finishedDragonQuest;
    public bool hasObtainedDragon;

    //---triggers that remove companions from scene once they are obtained---
    public bool shouldHaveDog = true;

    public bool isInCutscene = false;

    //---extra text triggers---
    public bool newWayDownS4 = false;
    public bool newHallways = false;

    public bool hasFinishedTut;

    public List<GameObject> companions;

    public GameObject[] companionPrefabs;

    void Awake()
    {
        //ensures no doubles
        if (GameObject.FindGameObjectsWithTag("GameController").Length > 1)
        {
            Destroy(GameObject.FindGameObjectsWithTag("GameController")[1]);
            Debug.Log("Destroyed");
        }
        foreach (GameObject _cherry in GameObject.FindGameObjectsWithTag("Cherry"))
        {
            _cherry.GetComponent<Cherry>().gameManager = this;
        }
        //locates player gameobject and establishes communication between GM/player scripts

        DontDestroyOnLoad(this);
        UIController = this.GetComponent<GMUIController>();
        Camera.main.nearClipPlane = -2;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (!GameObject.FindGameObjectWithTag("Player") && !isLoading)
        {
            GameObject _player = Instantiate(playerPrefab) as GameObject;
            Camera.main.GetComponent<SmoothCameraFollow>().target = _player.transform;
            _player.GetComponent<Player>().gm = this;
            _player.transform.position = startPosition;
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            player.gm = this;
        }
    }

    public void Update()
    {
        if (newWayDownS4 && SceneManager.GetActiveScene().buildIndex == 4)
        {
            ClearObtainedCompanions();
            NewWayDown();
        }
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape))
        {
            UIController.ToggleMenu();
        }
    }

    private void NewWayDown()
    {
        UIController.RunNewWayDownLine();
        Destroy(GameObject.FindGameObjectWithTag("Removable"));
        newWayDownS4 = false;
    }
    private void NewHallways()
    {
        UIController.RunNewHallwayLine();
        //Destroy(GameObject.FindGameObjectWithTag("Removable"));
        newHallways = false;
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

    public void ClearObtainedCompanions()
    {
        if (GameObject.FindGameObjectWithTag("Dog") && hasObtainedDog && !isInCutscene)
            Destroy(GameObject.FindGameObjectWithTag("Dog"));
        if (GameObject.FindGameObjectWithTag("Bear") && hasObtainedBear && !isInCutscene)
            Destroy(GameObject.FindGameObjectWithTag("Bear"));
        if (GameObject.FindGameObjectWithTag("Dragon") && hasObtainedDragon && !isInCutscene)
            Destroy(GameObject.FindGameObjectWithTag("Dragon"));
    }

    public void FadeToBlack()
    {
        StartCoroutine(SimpleFade());
    }
    public void DogFadeCutscene()
    {
        StartCoroutine(DogCutscene());
    }
    public void LoadSceneAndPosition(int _index, Vector3 _position, bool _flipX)
    {
        StartCoroutine(LoadInSceneAndPosition(_index, _position, _flipX));
    }
    public void LoadScene(int _index)
    {
        StartCoroutine(LoadInScene(_index));
    }
    public void ReloadScene(Vector3 _resetPos, bool _flipX)
    {
        StartCoroutine(ReloadThisScene(_resetPos, _flipX));
    }
    IEnumerator DogCutscene()
    {
        isInCutscene = true;
        TextMeshProUGUI cherryText = UIController.cherryLoadScreenText;
        GameObject _loadingScreen = UIController.loadingScreen;
        _loadingScreen.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        cherryText.text = "x "+ cherryCount;
        UIController.cherryCountObj.SetActive(true);
        cherryCount++;
        cherryText.text = "x " + cherryCount;
        yield return new WaitForSeconds(.15f);
        cherryCount++;
        cherryText.text = "x " + cherryCount;
        yield return new WaitForSeconds(.15f);
        cherryCount++;
        cherryText.text = "x " + cherryCount;
        yield return new WaitForSeconds(.15f);
        cherryCount++;
        cherryText.text = "x " + cherryCount;
        yield return new WaitForSeconds(.15f);
        cherryCount++;
        cherryText.text = "x " + cherryCount;
        UIController.UpdateCherryText(cherryCount);
        yield return new WaitForSeconds(1.5f);
        UIController.cherryCountObj.SetActive(false);
        //add cherry addition animation here
        //load cutscene where dog says last lines; transition to real scene after
        SceneManager.LoadScene(6);
        _loadingScreen.GetComponent<Animator>().Play("LoadingScreen_FadeOut");
    }
    IEnumerator LoadInSceneAndPosition(int _index, Vector3 _position, bool _flipX)
    {
        isLoading = true;
        GameObject _loadingScreen = UIController.loadingScreen;
        _loadingScreen.SetActive(true);
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene(_index);
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        yield return new WaitForSeconds(.2f);
        isInCutscene = false;
        GameObject _player = Instantiate(playerPrefab) as GameObject;
        Camera.main.GetComponent<SmoothCameraFollow>().target = _player.transform;
        _player.GetComponent<Player>().gm = this;
        _player.transform.position = _position;
        _player.GetComponentInChildren<SpriteRenderer>().flipX = _flipX;
        Camera.main.transform.position = new Vector3 (_position.x, _position.y, -10f);
        _loadingScreen.GetComponent<Animator>().Play("LoadingScreen_FadeOut");
        isLoading = false;
    }
    IEnumerator LoadInScene(int _index)
    {
        isLoading = true;
        GameObject _loadingScreen = UIController.loadingScreen;
        _loadingScreen.SetActive(true);
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene(_index);
        yield return new WaitForSeconds(.1f);
        Camera.main.transform.position = new Vector3(GameObject.FindGameObjectWithTag("Player").transform.position.x, GameObject.FindGameObjectWithTag("Player").transform.position.y, -10f);
        yield return new WaitForSeconds(.1f);
        _loadingScreen.GetComponent<Animator>().Play("LoadingScreen_FadeOut");
        isLoading = false;
    }
    IEnumerator ReloadThisScene(Vector3 _resetPos, bool _flipX)
    {
        isLoading = true;
        GameObject _loadingScreen = UIController.loadingScreen;
        _loadingScreen.SetActive(true);
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        yield return new WaitForSeconds(.2f);
        isInCutscene = false;
        GameObject _player = Instantiate(playerPrefab) as GameObject;
        Camera.main.GetComponent<SmoothCameraFollow>().target = _player.transform;
        _player.GetComponent<Player>().gm = this;
        _player.transform.position = _resetPos;
        _player.GetComponentInChildren<SpriteRenderer>().flipX = _flipX;
        Camera.main.transform.position = new Vector3(_resetPos.x, _resetPos.y, -10f);
        _loadingScreen.GetComponent<Animator>().Play("LoadingScreen_FadeOut");
        isLoading = false;
    }
    IEnumerator SimpleFade()
    {
        GameObject _loadingScreen = UIController.loadingScreen;
        yield return new WaitForSeconds(.5f);
        _loadingScreen.GetComponent<Animator>().Play("LoadingScreen_FadeOut");
    }

}
