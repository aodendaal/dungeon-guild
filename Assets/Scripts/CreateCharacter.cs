using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CreateCharacter : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_InputField characterName;
    [SerializeField] private Sprite characterSprite;

    void Start()
    {
        panel.SetActive(false);
    }
    
    public void ShowPanel()
    {
        characterName.text = string.Empty;
        panel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(characterName.gameObject);
    }

    public void Save()
    {
        var newCharacter = ScriptableObject.CreateInstance<MonsterScriptableObject>();

        newCharacter.Name = characterName.text;
        newCharacter.portrait = characterSprite;
        newCharacter.Speed = 20;
        newCharacter.Attack = 20;
        newCharacter.Defense = 20;
        newCharacter.HitPoints = 20;

        Game.Instance.characters.Add(newCharacter);
        Game.Instance.UpdateCharacterList();
        panel.SetActive(false);
    }

    public void HidePanel()
    {
        panel.SetActive(false);
    }
}
