using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCard : MonoBehaviour
{
    [SerializeField]
    private Image outerCard;
    [SerializeField]
    private TMP_Text nameText;
    [Header("Stats")]
    [SerializeField] private TMP_Text attackText;
    [SerializeField] private TMP_Text defenseText;
    [SerializeField] private TMP_Text speedText;
    [SerializeField] private TMP_Text hitpointText;

    public bool isSelected;

    public MonsterScriptableObject character;

    public void SetName(MonsterScriptableObject character)
    {
        this.character = character;
        nameText.text = character.Name;
        attackText.text = character.Attack.ToString();
        defenseText.text = character.Defense.ToString();
        speedText.text = character.Speed.ToString();
        hitpointText.text = character.HitPoints.ToString();

    }

    public void Click()
    {

        isSelected = !isSelected;

        if (isSelected && Game.Instance.selectedCharacters.Count == 4)
        {
            isSelected = false;
        }

        if (isSelected)
        {
            outerCard.color = Color.yellow;
            Game.Instance.selectedCharacters.Add(character);
            Game.Instance.UpdateDungeonButton();
        }
        else
        {
            outerCard.color = Color.white;
            Game.Instance.selectedCharacters.Remove(character);
            Game.Instance.UpdateDungeonButton();
        }
    }
    
}
