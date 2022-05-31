using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Monster", menuName = "ScriptableObjects/Monster", order = 1)]
public class MonsterScriptableObject : ScriptableObject
{
    public string Name;
    public Sprite portrait;
    public int Speed;
    public int Attack;
    public int Defense;
    public int HitPoints;
}
