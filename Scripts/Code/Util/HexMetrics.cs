using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public static class HexTile
{

    public const float outerRadius = 1f;

    public const float innerRadius = outerRadius * 0.866025404f;
    public static Vector3[] corners = {
        new Vector3(0f, 0f, outerRadius),
        new Vector3(innerRadius, 0f, 0.5f * outerRadius),
        new Vector3(innerRadius, 0f, -0.5f * outerRadius),
        new Vector3(0f, 0f, -outerRadius),
        new Vector3(-innerRadius, 0f, -0.5f * outerRadius),
        new Vector3(-innerRadius, 0f, 0.5f * outerRadius)
    };
    private static Vector2Int[] AroundOdd = new Vector2Int[]{
        new Vector2Int(-1,1),
        new Vector2Int(0,1),
        new Vector2Int(1,0),
        new Vector2Int(0,-1),
        new Vector2Int(-1,-1),
        new Vector2Int(-1,0),
    };
    private static Vector2Int[] AroundEven = new Vector2Int[]{
        new Vector2Int(0,1),
        new Vector2Int(1,1),
        new Vector2Int(1,0),
        new Vector2Int(1,-1),
        new Vector2Int(0,-1),
        new Vector2Int(-1,0),
    };
    public static List<Vector2Int> GetAroundHex(this Vector2Int coordinate)
    {
        var isEvenIndex = coordinate.y % 2 == 0;
        var around = (isEvenIndex ? AroundEven : AroundOdd);
        var result= new List<Vector2Int>();
        for (int i = 0; i < around.Length; i++)
        {
            result.Add(coordinate + around[i]);
        }
        return result;
    }
    public static List<Vector2Int> GetAroundHexWithBound(this Vector2Int coordinate, Vector2Int area)
    {
        var  result = coordinate.GetAroundHex();
        result.RemoveAll(x => x.x < 0 || x.x >= area.x || x.y < 0 || x.y >= area.y);
        return result;
    }
    public static List<Vector2Int> GetAroundHexWithBound(this Vector2Int coordinate, Vector2Int area,int range)
    {
        var  result = coordinate.GetAroundHex(range);
        result.RemoveAll(x => x.x < 0 || x.x >= area.x || x.y < 0 || x.y >= area.y);
        return result;
    }

    public static List<Vector2Int> GetAroundHex(this Vector2Int coordinate,int range)
    {
        var result = GetAroundHex(coordinate);
        for (int i = 1; i < range; i++)
        {
            int count = result.Count;
            for (int i1 = 0; i1 < count; i1++)
            {
                Vector2Int item = result[i1];
                var around = GetAroundHex(item);
                foreach (Vector2Int aroundItem in around)
                {
                    if (result.Contains(aroundItem) == false)
                    {
                        result.Add(aroundItem);
                    }
                }
            }
        }
        return result;
    }
    public static float sqrMagnitudeHex(this Vector2Int next ,Vector2Int destination,Vector2Int area)
    {
        float xDestination = destination.x;
        if (xDestination % area.y != 0)
        {
            xDestination = xDestination - 0.5f;
        }
        float xNext = next.x;
        if (xNext % area.y != 0)
        {
            xNext = xNext - 0.5f;
        }
        float xHeuristic = Mathf.Abs(xDestination - xNext);
        float yHeuristic = Mathf.Abs(destination.y - next.y);

        return xHeuristic + yHeuristic;
    }
    public static int DistanceHex(this Vector2Int current ,Vector2Int to)
    {
        int dx = to.x - current.x;     // signed deltas
        int dy = to.y - current.y;
        int x = Mathf.Abs(dx);  // absolute deltas
        int y = Mathf.Abs(dy);
        // special case if we start on an odd row or if we move into negative x direction
        if ((current.y & 1) == 0)
            x = Mathf.Max(0, x - (y+1) / 2);
        else
            x = Mathf.Max(0, x - (y) / 2);
        return x + y;
    }

}
