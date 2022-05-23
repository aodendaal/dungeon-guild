using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterButton : MonoBehaviour
{

    public void SetName(string name)
    {
        var go = transform.GetChild(0);
        var text = go.GetComponent<TMP_Text>();
        text.text = name;
    }
    
}
