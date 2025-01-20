using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class OverrideDialogueSystem : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI currentLine;

    public GameObject character;
    public bool isWorm;
    public bool isDog;
    public bool isBear;
    public bool isDragon;

    private string characterName;

    public string[] lines;
    public int lineCount = 0;

    public void Awake()
    {
        currentLine.text = lines[lineCount];
        currentLine.GetComponent<TextMeshProEffect>().Play();
        lineCount++;
        if (isDog)
        {
            nameText.text = "Bongo";
        }
        if (isBear)
        {
            nameText.text = "Daisy";
        }
        if (isDragon)
        {
            nameText.text = "Boots";
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (lineCount == lines.Length)
            {
                Debug.Log("cutscene over");
                this.gameObject.SetActive(false);
                GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>().Play("Worm_Yippee_Static");
                if (isDog)
                {
                    if (SceneManager.GetActiveScene().buildIndex == 5)
                    {
                        GameObject.FindGameObjectWithTag("GameController").GetComponent<GM>().DogFadeCutscene();
                    }
                    else if (SceneManager.GetActiveScene().buildIndex == 6)
                    {
                        GameObject _gm = GameObject.FindGameObjectWithTag("GameController");
                        _gm.GetComponent<GM>().newWayDownS4 = true;
                        _gm.GetComponent<GM>().currentQuest = GM.Questline.NONE;
                        _gm.GetComponent<GM>().LoadSceneAndPosition(4, new Vector3(-7.8f, 4.48f, 0f), false);
                    }
                    return;
                }
                if (isBear)
                {
                    if (SceneManager.GetActiveScene().buildIndex == 13)
                    {
                        GameObject.FindGameObjectWithTag("GameController").GetComponent<GM>().LoadScene(14);
                    }
                    else if (SceneManager.GetActiveScene().buildIndex == 14)
                    {
                        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>().Play("Worm_Yippee");
                        GameObject _gm = GameObject.FindGameObjectWithTag("GameController");
                        _gm.GetComponent<GM>().newHallways = true;
                        _gm.GetComponent<GM>().hasObtainedBear = true;
                        _gm.GetComponent<GM>().currentQuest = GM.Questline.NONE;
                        _gm.GetComponent<GM>().LoadSceneAndPosition(10, new Vector3(-19.8700008f, -10.2200003f, 0f), false);
                    }
                }
                if (isWorm)
                {
                    GM _gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GM>();
                    _gm.LoadSceneAndPosition(1, new Vector3(-5.74f, 0.63f, 0), false);
                    _gm.UIController.cherryText.gameObject.SetActive(true);
                }
            }
            if (isBear && lineCount == 4 && !GameObject.FindGameObjectWithTag("GameController").GetComponent<GM>().hasObtainedBear && GameObject.FindGameObjectWithTag("GameController").GetComponent<GM>().finishedBearQuest)
            {
                nameText.text = " ";
            }
            if (isWorm && lineCount == 3)
            {
                Debug.Log("Wormy walks to tree.");
                StartCoroutine(WormyWalks());
            }
            if (isWorm && lineCount == 4)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>().Play("Worm_Yippee_Static");
            }
            ProgressConversation();
        }
    }

    public void ProgressConversation()
    {
        currentLine.text = lines[lineCount];
        currentLine.GetComponent<TextMeshProEffect>().Play();
        lineCount++;
    }

    IEnumerator WormyWalks()
    {
        GameObject _wormy = GameObject.FindGameObjectWithTag("Player");
        Vector3 _startPos = new Vector3(-5.74f, .63f, 0f);
        Vector3 _endPos = new Vector3(-10.04f, .63f, 0f);
        float _xPos = -5.74f;
        float _time = 2f;
        float _currentTime = 0f;
        _wormy.GetComponentInChildren<SpriteRenderer>().flipX = true;
        _wormy.GetComponentInChildren<Animator>().Play("Worm_Walk");
        while (_currentTime <= _time)
        {
            _xPos = Mathf.Lerp(_startPos.x, _endPos.x, _currentTime / _time);
            _wormy.transform.position = new Vector3(_xPos, _wormy.transform.position.y, 0f);
            _currentTime += Time.deltaTime;
            yield return null;
        }
        _wormy.GetComponentInChildren<Animator>().Play("Worm_Idle");
        _wormy.transform.position = _endPos;
    }
}
