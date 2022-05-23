using RogueSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Random = UnityEngine.Random;

public static class CellExtensionMethods
{
    public static Cell GetRandomCell(this IEnumerable<Cell> source)
    {
        var index = Random.Range(0, source.Count());

        return source.ElementAt(index);
    }
}