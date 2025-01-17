using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerCanvasController : MonoBehaviour
{
    public TextMeshProUGUI cherryGetText;
    public void CherryGet()
    {
        cherryGetText.gameObject.SetActive(true);
    }
}
