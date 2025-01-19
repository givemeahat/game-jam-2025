using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialImage : MonoBehaviour
{
    bool hasPressedA;
    bool hasPressedD;
    bool hasPressedSpace;
    bool hasPressedEnter;

    public GameObject image;

    // Start is called before the first frame update
    void Start()
    {
        if (!GameObject.FindGameObjectWithTag("GameController").GetComponent<GM>().hasFinishedTut)
        {
            image.SetActive(true);
        }
        else this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) hasPressedA = true;
        if (Input.GetKeyDown(KeyCode.D)) hasPressedD = true;
        if (Input.GetKeyDown(KeyCode.Space)) hasPressedSpace = true;
        if (Input.GetKeyDown(KeyCode.Return)) hasPressedEnter = true;
        if (hasPressedA && hasPressedD && hasPressedEnter && hasPressedSpace)
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GM>().hasFinishedTut = true;
            this.gameObject.SetActive(false);
        }
    }
}
