using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerCanvasController : MonoBehaviour
{
    public TextMeshProUGUI cherryGetText;
    public TextMeshProUGUI digText;
    public TextMeshProUGUI breakText;
    public TextMeshProUGUI talkText;
    public TextMeshProUGUI grabText;

    //---cherry get---
    public void CherryGet()
    {
        cherryGetText.gameObject.SetActive(true);
    }
    //---dig---
    public void ShowDigText()
    {
        digText.gameObject.SetActive(true);
    }
    public void HideDigText()
    {
        digText.GetComponent<Animator>().Play("Talk_Disappear");
    }
    //---break---
    public void ShowBreakText()
    {
        breakText.gameObject.SetActive(true);
    }
    public void HideBreakText()
    {
        breakText.GetComponent<Animator>().Play("Talk_Disappear");
    }
    //---talk---
    public void ShowTalkText()
    {
        talkText.gameObject.SetActive(true);
    }
    public void HideTalkText()
    {
        talkText.GetComponent<Animator>().Play("Talk_Disappear");
    }
    //---grab---
    public void ShowGrabText()
    {
        grabText.gameObject.SetActive(true);
    }
    public void HideGrabText()
    {
        grabText.GetComponent<Animator>().Play("Talk_Disappear");
    }
}
