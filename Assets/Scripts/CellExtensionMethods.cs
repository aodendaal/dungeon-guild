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
        var count = source.Count();

        if (count == 0)
            throw new ArgumentException("Source contains no elements to randomly select");

        var index = Random.Range(0, count);

        return source.ElementAt(index);
    }
}