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

    private int currentHealth;
    private int maxHealth;

    private float turnTime;
    private float turnRate;

    public void Setup(string name, int health, float turnRate)
    {
        nameText.text = name;

        currentHealth = health;
        maxHealth = health;

        turnTime = Random.Range(0.0f, turnRate / 2.0f);
        this.turnRate = turnRate;
        speedText.text = $"Speed: {(int)(turnRate * 100.0f):D2}";
    }

    private void Update()
    {
        turnTime += Time.deltaTime;

        timeSlider.value = turnTime / turnRate;

        if (turnTime >= turnRate)
        {
            turnTime = 0;
            CombatManager.Instance.Attack();
        }
    }
}
