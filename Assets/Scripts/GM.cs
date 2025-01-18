using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

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
    public bool hasTalkedToBear;
    public bool finishedBearQuest;
    public bool hasObtainedBear;
    public bool hasTalkedToDragon;
    public bool finishedDragonQuest;
    public bool hasObtainedDragon;

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
    IEnumerator DogCutscene()
    {
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
        GameObject _loadingScreen = UIController.loadingScreen;
        _loadingScreen.SetActive(true);
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene(_index);
        yield return new WaitForSeconds(.1f);
        GameObject.FindGameObjectWithTag("Player").transform.position = _position;
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<SpriteRenderer>().flipX = _flipX;
        _loadingScreen.GetComponent<Animator>().Play("LoadingScreen_FadeOut");
    }
    IEnumerator LoadInScene(int _index)
    {
        GameObject _loadingScreen = UIController.loadingScreen;
        _loadingScreen.SetActive(true);
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene(_index);
        yield return new WaitForSeconds(.1f);
        _loadingScreen.GetComponent<Animator>().Play("LoadingScreen_FadeOut");
    }
    IEnumerator SimpleFade()
    {
        GameObject _loadingScreen = UIController.loadingScreen;
        yield return new WaitForSeconds(.5f);
        _loadingScreen.GetComponent<Animator>().Play("LoadingScreen_FadeOut");
    }

}
