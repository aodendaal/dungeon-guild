using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterStatView : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text hitpointText;
    
    private MonsterScriptableObject character;

    // Update is called once per frame
    void Update()
    {
        if (character == null)
            return;

        hitpointText.text = $"{character.CurrentHitPoints}/{character.HitPoints}";
    }

    public void SetCharacter(MonsterScriptableObject character)
    {
        this.character = character;
        nameText.text = this.character.Name;
    }
}
