using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerCanvasController : MonoBehaviour
{
    public TextMeshProUGUI cherryGetText;
    public TextMeshProUGUI talkText;
    public TextMeshProUGUI digText;

    public void CherryGet()
    {
        cherryGetText.gameObject.SetActive(true);
    }
    public void ShowDigText()
    {
        digText.gameObject.SetActive(true);
    }
    public void HideDigText()
    {
        digText.GetComponent<Animator>().Play("Talk_Disappear");
    }
    public void ShowTalkText()
    {
        talkText.gameObject.SetActive(true);
    }
    public void HideTalkText()
    {
        talkText.GetComponent<Animator>().Play("Talk_Disappear");
    }
}
