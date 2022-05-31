using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RogueSharp;
using System;
using System.Linq;


public class DungeonManager : MonoBehaviour
{
    #region Singleton

    public static DungeonManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    GameMap map;

    [SerializeField]
    GameObject tilePrefab;
    [SerializeField]
    GameObject encounterPrefab;
    List<Vector2Int> encounters;

    public void GenerateDungeon()
    {
        var r = new RogueSharp.Random.DotNetRandom(1234);
        var strategy = new RogueSharp.MapCreation.BinaryTreeAlgorithmCreationStrategy<GameMap, GameCell>(21, 21, r);

        map = strategy.CreateMap();
        PlaceDungeonTiles();
        GenerateEncounters();
    }

    private void GenerateEncounters()
    {
        encounters = new List<Vector2Int>();

        for (int i = 0; i < 8; i++)
        {
            Vector2Int pos;
            do
            {
                pos = GetRandomWalkablePosition();
            } while (encounters.Contains(pos));
            encounters.Add(pos);

            var go = Instantiate(encounterPrefab, new Vector3(pos.x, 0.0f, pos.y), Quaternion.identity);
            go.name = $"Encounter ({pos.x},{pos.y})";
            go.transform.SetParent(transform);
        }
    }

    private void PlaceDungeonTiles()
    {
        foreach (var cell in map.GetAllCells())
        {
            if (cell.IsWalkable)
            {
                var go = Instantiate(tilePrefab, new Vector3(cell.X, 0, cell.Y), Quaternion.identity);
                go.name = $"Cell ({cell.X},{cell.Y})";
                go.transform.SetParent(transform);
            }
        }
    }

    public GameCell GetCell(int x, int y)
    {
        return map.GetCell(x, y);
    }

    public GameCell GetCell(float x, float y)
    {
        return GetCell((int)x, (int)y);
    }

    public Vector2Int GetRandomWalkablePosition()
    {
        var cell = map.GetAllCells().Where(c => c.IsWalkable && !encounters.Contains(new Vector2Int(c.X, c.Y))).GetRandomCell();

        return new Vector2Int(cell.X, cell.Y);
    }

    public void VisitCell(int x, int y)
    {
        map.GetCell(x, y).IsVisited = true;
        var go = transform.Find($"Cell ({x},{y})").gameObject;
        go.GetComponent<UpdateMaterialColor>().MarkAsVisited();
    }

    public void VisitCell(float x, float y)
    {
        VisitCell((int)x, (int)y);
    }

    public bool ContainsEncounter(float x, float y)
    {
        return encounters.Contains(new Vector2Int((int)x, (int)y));
    }

    public void RemoveEncounter(int x, int y)
    {
        encounters.Remove(new Vector2Int(x, y));
        var go = transform.Find($"Encounter ({x},{y})").gameObject;
        Destroy(go);
    }
}
