using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CreateCharacter : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_InputField characterName;

    void Start()
    {
        panel.SetActive(false);
    }
    
    public void ShowPanel()
    {
        characterName.text = string.Empty;
        panel.SetActive(true);
    }

    public void Save()
    {
        Game.Instance.characters.Add(characterName.text);
        Game.Instance.UpdateCharacterList();
        panel.SetActive(false);
    }

    public void HidePanel()
    {
        panel.SetActive(false);
    }
}
