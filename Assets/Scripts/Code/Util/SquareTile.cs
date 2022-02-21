using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SquareTile
{
    public static Vector2Int[] GetNeighborsSquare(this Vector2Int position)
    {
        return new Vector2Int[]
        {
            position + Vector2Int.up,
            position + Vector2Int.down,
            position + Vector2Int.right,
            position + Vector2Int.left,
        };
    }
    




}
