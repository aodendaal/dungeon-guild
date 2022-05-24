using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Game : MonoBehaviour
{
    #region Singleton

    public static Game Instance;

    void Awake()
    {
        Instance = this;
    }

    #endregion

    [SerializeField] private GameObject characterButton;
    [SerializeField] private GameObject characterListPanel;
    [SerializeField] private GameObject player;

    public List<string> characters;

    private void Start()
    {
        DungeonManager.Instance.GenerateDungeon();

        var startPosition = DungeonManager.Instance.GetRandomWalkablePosition();
        player.transform.position = new Vector3(startPosition.x, 0.0f, startPosition.y);
        DungeonManager.Instance.VisitCell(startPosition.x, startPosition.y);
    }

    public void UpdateCharacterList()
    {
        foreach (Transform child in characterListPanel.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (var name in characters)
        {
            var go = Instantiate(characterButton, new Vector3(0, 0, 0), quaternion.identity);
            go.transform.parent = characterListPanel.transform;

            go.GetComponent<CharacterButton>().SetName(name);
        }
    }
}
