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
            nameText.text = "Doggy";
        }
        if (isBear)
        {
            nameText.text = "Bear";
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
                if (SceneManager.GetActiveScene().buildIndex == 5)
                {
                    GameObject.FindGameObjectWithTag("GameController").GetComponent<GM>().DogFadeCutscene();
                }
                else if (SceneManager.GetActiveScene().buildIndex == 6)
                {
                    GameObject.FindGameObjectWithTag("GameController").GetComponent<GM>().LoadScene(4);
                }
                return;
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
}
