using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementManager : MonoBehaviour
{
    [Header("Player Character")]
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject model;

    [Header("Locations")]
    [SerializeField]
    private TMPro.TMP_Text[] locationTexts;

    [Header("Sounds")]
    [SerializeField]
    private AudioClip unvisitedClip;
    [SerializeField]
    private AudioClip visitedClip;
    [SerializeField]
    private AudioClip combatClip;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = player.GetComponent<AudioSource>();
    }

    public void MoveUp(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        if (CombatManager.Instance.IsInCombat)
        {
            return;
        }

        var cell = DungeonManager.Instance.GetCell(player.transform.position.x, player.transform.position.z + 1.0f);

        if (cell.IsWalkable)
        {
            player.transform.position += new Vector3(0.0f, 0.0f, 1.0f);
            model.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);

            RunEncounter();
        }
    }

    public void MoveDown(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        if (CombatManager.Instance.IsInCombat)
        {
            return;
        }

        var cell = DungeonManager.Instance.GetCell(player.transform.position.x, player.transform.position.z - 1.0f);

        if (cell.IsWalkable)
        {
            player.transform.position += new Vector3(0.0f, 0.0f, -1.0f);
            model.transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);

            RunEncounter();
        }
    }

    public void MoveLeft(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        if (CombatManager.Instance.IsInCombat)
        {
            return;
        }

        var cell = DungeonManager.Instance.GetCell(player.transform.position.x - 1.0f, player.transform.position.z);

        if (cell.IsWalkable)
        {
            player.transform.position += new Vector3(-1.0f, 0.0f, 0.0f);
            model.transform.rotation = Quaternion.Euler(0.0f, 270.0f, 0.0f);

            RunEncounter();
        }
    }

    public void MoveRight(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        if (CombatManager.Instance.IsInCombat)
        {
            return;
        }

        var cell = DungeonManager.Instance.GetCell(player.transform.position.x + 1.0f, player.transform.position.z);

        if (cell.IsWalkable)
        {
            player.transform.position += new Vector3(1.0f, 0.0f, 0.0f);
            model.transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);

            RunEncounter();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (CombatManager.Instance.IsInCombat)
        {
            return;
        }

        UpdateLocationText();
    }

    private void RunEncounter()
    {
        var x = player.transform.position.x;
        var y = player.transform.position.z;

        if (DungeonManager.Instance.ContainsEncounter(x, y))
        {
            DungeonManager.Instance.VisitCell(x, y);

            audioSource.clip = combatClip;
            audioSource.Play();
            CombatManager.Instance.StartCombat(x, y);
        }
        else
        {
            if (!DungeonManager.Instance.GetCell(x, y).IsVisited)
            {
                DungeonManager.Instance.VisitCell(x, y);

                audioSource.clip = unvisitedClip;
                audioSource.Play();
            }
            else
            {
                audioSource.clip = visitedClip;
                audioSource.Play();
            }
        }
    }

    private void UpdateLocationText()
    {
        foreach (var text in locationTexts)
        {
            text.text = $"Floor: 00 / X {(int)player.transform.position.x:D2} / Y {(int)player.transform.position.z:D2}";
        }
    }
}
