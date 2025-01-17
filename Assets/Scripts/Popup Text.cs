using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopupText : MonoBehaviour
{
    public TextMeshProEffect effect;

    void SetInactive()
    {
        effect.enabled = false;
        this.gameObject.SetActive(false);
    }
    void ActivateEffect()
    {
        effect.enabled = true;
    }
}
