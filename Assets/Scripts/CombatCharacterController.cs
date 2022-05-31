using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;

public class CombatCharacterController : MonoBehaviour
{
    [SerializeField]
    private Slider timeSlider;
    [SerializeField]
    private TMP_Text speedText;
    [SerializeField]
    private TMP_Text nameText;
    [SerializeField]
    private Slider healthSlider;
    [SerializeField]
    private Image portrait;

    public MonsterScriptableObject characterDetails;
    public int currentHitPoints;

    private float turnTime;
    private float turnRate;

    private CombatManager.Sides attackingSide;

    public void Setup(MonsterScriptableObject monster, float turnRate, CombatManager.Sides attacks)
    {
        this.characterDetails = monster;
        nameText.text = monster.Name;

        portrait.sprite = monster.portrait;

        currentHitPoints = monster.HitPoints;

        turnTime = Random.Range(0.0f, turnRate / 2.0f);
        this.turnRate = turnRate;
        speedText.text = $"Speed: {(int)(turnRate * 100.0f):D2}";

        attackingSide = attacks;
    }

    void Update()
    {
        if (CombatManager.Instance.IsCombatPaused)
        {
            return;
        }

        turnTime += Time.deltaTime;

        timeSlider.value = turnTime / turnRate;

        if (turnTime >= turnRate)
        {
            turnTime = 0;
            CombatManager.Instance.Attack(this, attackingSide);
        }
    }

    public void TakeDamage(int points)
    {
        currentHitPoints -= points;
        if (currentHitPoints < 0)
        {
            currentHitPoints = 0;
        }

        healthSlider.value = (float)currentHitPoints / (float)characterDetails.HitPoints;
    }
}
