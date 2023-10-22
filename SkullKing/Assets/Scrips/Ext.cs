using UnityEngine;

public static class Ext
{
    public static Vector2 SetX(this Vector2 v, float x) => new Vector2(x, v.y);
    public static Vector2 SetY(this Vector2 v, float y) => new Vector2(v.x, y);
}