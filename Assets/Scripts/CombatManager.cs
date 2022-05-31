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
    [Header("Monsters")]
    [SerializeField]
    private MonsterScriptableObject[] monsters;
    [SerializeField]
    private Sprite playerSprite;

    private List<GameObject> enemies;
    private List<GameObject> players;

    public bool IsInCombat;
    public bool IsCombatPaused;

    private Vector2Int combatLocation;

    public enum Sides
    {
        Enemies,
        Players
    }

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

        enemies = new List<GameObject>();
        players = new List<GameObject>();

        var count = Random.Range(1, 4);
        for (int i = 0; i < count; i++)
        {
            var monster = monsters[Random.Range(0, monsters.Length)];
            var go = Instantiate(characterPrefab, Vector3.one, Quaternion.identity, enemyVerticalGroup.transform);
            go.GetComponent<CombatCharacterController>().Setup(monster, 2.0f, Sides.Players);
            enemies.Add(go);
        }

        for (int i = 0; i < Game.Instance.selectedCharacters.Count; i++)
        {
            var go2 = Instantiate(characterPrefab, Vector3.one, Quaternion.identity, playerVerticalGroup.transform);

            go2.GetComponent<CombatCharacterController>().Setup(Game.Instance.selectedCharacters[i], 2.0f, Sides.Enemies);
            players.Add(go2);
        }
    }

    public void Attack(CombatCharacterController from, Sides to)
    {
        var attacker = from.characterDetails;
        CombatCharacterController defender;

        if (to == Sides.Enemies)
        {
            var go = enemies[Random.Range(0, enemies.Count)];
            defender = go.GetComponent<CombatCharacterController>();
        }
        else
        {
            var go = players[Random.Range(0, players.Count)];
            defender = go.GetComponent<CombatCharacterController>();
        }

        var chanceToHit = Mathf.Clamp(100 + attacker.Attack - defender.characterDetails.Defense, 10, 100);
        var roll = Random.Range(1, 101);
        var damage = Random.Range(1, 7);

        Debug.Log($"{attacker.Name} attacks {defender.characterDetails.Name} with {chanceToHit}% and rolls {roll} and {damage} damage");

        if (roll <= chanceToHit)
        {
            defender.TakeDamage(damage);
            if (defender.currentHitPoints <= 0)
            {
                if (to == Sides.Players)
                {
                    defender.TakeDamage(-1);
                    return;
                }

                enemies.Remove(defender.gameObject);

                Destroy(defender.gameObject);

                if (enemies.Count == 0)
                {
                    CombatOver();
                }
            }
        }
    }

    private void CombatOver()
    {
        foreach (var go in enemies)
        {
            Destroy(go);
        }
        foreach (var go in players)
        {
            Destroy(go);
        }

        combatPanel.SetActive(false);
        IsInCombat = false;

        DungeonManager.Instance.RemoveEncounter(combatLocation.x, combatLocation.y);
    }
}
