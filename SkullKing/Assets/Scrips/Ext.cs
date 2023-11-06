using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Ext
{
    public static Vector2 SetX(this Vector2 v, float x) => new Vector2(x, v.y);
    public static Vector2 SetY(this Vector2 v, float y) => new Vector2(v.x, y);
    public static IEnumerable<int> RangeTo(this int a, int b)
    {
        if (a > b) yield break;
        while (a <= b)
        {
            yield return a;
            a++;
        }
        yield break;
    }
}