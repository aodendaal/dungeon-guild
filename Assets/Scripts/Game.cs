using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    #region Singleton

    public static Game Instance;

    void Awake()
    {
        Instance = this;
    }

    #endregion

    [Header("Main Menu")]
    [SerializeField] private GameObject startMenuPanel;
    [SerializeField] private GameObject characterCard;
    [SerializeField] private GameObject characterListPanel;
    [SerializeField] private Button enterDungeonButton;
    [Header("Dungeon")]
    [SerializeField] private GameObject playerGameObject;
    [SerializeField] private PlayerInput playerInput;

    public List<MonsterScriptableObject> characters;
    [HideInInspector]
    public List<MonsterScriptableObject> selectedCharacters;

    public void UpdateDungeonButton()
    {
        if (selectedCharacters.Count == 0)
        {
            enterDungeonButton.interactable = false;
        }
        else
        {
            enterDungeonButton.interactable = true;
        }
    }

    public void UpdateCharacterList()
    {
        foreach (Transform child in characterListPanel.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (var character in characters)
        {
            var go = Instantiate(characterCard, new Vector3(0, 0, 0), quaternion.identity, characterListPanel.transform);

            go.GetComponent<CharacterCard>().SetName(character);
        }
    }

    public void EnterDungeon()
    {
        startMenuPanel.SetActive(false);
     
        DungeonManager.Instance.GenerateDungeon();

        var startPosition = DungeonManager.Instance.GetRandomWalkablePosition();
        playerGameObject.transform.position = new Vector3(startPosition.x, 0.0f, startPosition.y);
        DungeonManager.Instance.VisitCell(startPosition.x, startPosition.y);

        playerInput.SwitchCurrentActionMap("World");
    }
}
