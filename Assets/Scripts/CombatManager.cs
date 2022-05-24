using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CombatManager : MonoBehaviour
{
    #region Singleton

    public static CombatManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    [Header("Panels")]
    [SerializeField]
    private GameObject combatPanel;
    [SerializeField]
    private GameObject combatWarningPanel;
    [Header("Groups")]
    [SerializeField]
    private GameObject enemyVerticalGroup;
    [SerializeField]
    private GameObject playerVerticalGroup;
    [Header("Prefabs")]
    [SerializeField]
    private GameObject characterPrefab;

    private List<GameObject> characters;

    public bool IsInCombat;

    private Vector2Int combatLocation;

    // Start is called before the first frame update
    void Start()
    {
        combatPanel.SetActive(false);
    }

    public void StartCombat(float x, float y)
    {
        combatLocation = new Vector2Int((int)x, (int)y);

        IsInCombat = true;

        combatPanel.SetActive(true);

        characters = new List<GameObject>();

        var go = Instantiate(characterPrefab, Vector3.one, Quaternion.identity, enemyVerticalGroup.transform);
        go.GetComponent<CombatCharacterController>().Setup("Goblin 1", 10, 2.0f);
        characters.Add(go);

        var go2 = Instantiate(characterPrefab, Vector3.one, Quaternion.identity, playerVerticalGroup.transform);
        go2.GetComponent<CombatCharacterController>().Setup("Player", 10, 2.0f);
        characters.Add(go2);
    }

    public void Attack()
    {
        foreach (var go in characters)
        {
            Destroy(go);
        }

        combatPanel.SetActive(false);
        IsInCombat = false;

        DungeonManager.Instance.RemoveEncounter(combatLocation.x, combatLocation.y);
    }


}
