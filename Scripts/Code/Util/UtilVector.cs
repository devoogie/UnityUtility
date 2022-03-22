using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Direction
{
    Center = 0,
    Down,
    Up,
    Right,
    Left,
}
public static partial class Util
{
    public readonly static Vector2 SlopeVector2 = new Vector2(-1, -1);

    public static Vector2 Add(this Vector2 vector2, Vector3 vector3)
    {
        vector2.x += vector3.x;
        vector2.y += vector3.y;
        return vector2;
    }
    public static Vector2 ToVector2(this Vector3 vector3)
    {
        return vector3;
    }
    public static Vector3 Abs(this Vector3 vector3)
    {
        vector3.x = Mathf.Abs(vector3.x);
        vector3.y = Mathf.Abs(vector3.y);
        vector3.z = Mathf.Abs(vector3.z);

        return vector3;
    }
    public static Vector2 Abs(this Vector2 vector2)
    {
        vector2.x = Mathf.Abs(vector2.x);
        vector2.y = Mathf.Abs(vector2.y);

        return vector2;
    }
    public static Vector3 Clamp(this Vector3 vector3, float min, float max)
    {
        vector3.x = Mathf.Clamp(vector3.x, min, max);
        vector3.y = Mathf.Clamp(vector3.y, min, max);
        vector3.z = Mathf.Clamp(vector3.z, min, max);

        return vector3;
    }
    public static Vector2 Clamp(this Vector2 vector2, float min, float max)
    {
        vector2.x = Mathf.Clamp(vector2.x, min, max);
        vector2.y = Mathf.Clamp(vector2.y, min, max);

        return vector2;
    }
    public static Vector2 ClampX(this Vector2 vector2, float value)
    {
        vector2.x = Mathf.Clamp(vector2.x, -value, value);
        return vector2;
    }
    public static Vector2 ClampY(this Vector2 vector2, float value)
    {
        vector2.y = Mathf.Clamp(vector2.y, -value, value);
        return vector2;
    }
    public static Vector3 ToVector3(this Vector2 vector2)
    {
        return vector2;
    }
    public static Vector3 Add(this Vector3 vector3, Vector2 vector2)
    {
        vector3.x += vector2.x;
        vector3.y += vector2.y;

        return vector3;
    }
    public static Vector3 Add(this Vector3 vector3, Vector3 vector2)
    {
        vector3.x += vector2.x;
        vector3.y += vector2.y;

        return vector3;
    }
    public static Direction ToDirection(this Vector2 vector2)
    {
        float angle = Vector2.SignedAngle(vector2, SlopeVector2);
        return angle.ToDirection();
    }
    public static Direction ToDirection(this float angle)
    {
        angle = Mathf.Repeat(angle, 360);

        return angle switch
        {
            < 90 => Direction.Left,
            < 180 => Direction.Up,
            < 270 => Direction.Right,
            <= 360 => Direction.Down,
            _ => Direction.Center,
        };
    }
    public static int ToIndex(this Vector2Int coordinate, Vector2Int Area)
    {
        return (coordinate.y * Area.x) + coordinate.x;
    }
    public static Vector2 ToVector2(this Direction direction)
    {
        var destination = Vector2.zero;
        switch (direction)
        {
            case Direction.Up:
                destination = Vector2.up;
                break;
            case Direction.Down:
                destination = Vector2.down;
                break;
            case Direction.Left:
                destination = Vector2.left;
                break;
            case Direction.Right:
                destination = Vector2.right;
                break;
            default:
                break;
        }
        return destination;
    }
    public static Vector2Int ToVector2Int(this Direction direction)
    {
        var destination = Vector2Int.zero;
        switch (direction)
        {
            case Direction.Up:
                destination = Vector2Int.up;
                break;
            case Direction.Down:
                destination = Vector2Int.down;
                break;
            case Direction.Left:
                destination = Vector2Int.left;
                break;
            case Direction.Right:
                destination = Vector2Int.right;
                break;
            default:
                break;
        }
        return destination;
    }
    public static Vector2 ToVector2(this Vector2Int vector2)
    {
        return new Vector2(vector2.x, vector2.y);
    }
    public static Vector2Int ToVector2Int(this Vector2 vector2)
    {
        return new Vector2Int(Mathf.RoundToInt(vector2.x), Mathf.RoundToInt(vector2.y));
    }
    // public static Vector3 operator +(Vector3 vector3, Vector2 vector2)
    // {
    //     vector3.x += vector2.x;
    //     vector3.y += vector2.y;
    //     return vector3;
    // }
    public static Vector2[] ToCircle(this float radius, int step,float angle = 0)
    {
        var circle = new Vector2[step];
        for (int i = 0; i < step; i++)
        {
            float ratio = (float)i / (float)step;
            float currentRadian = ratio * Mathf.PI * 2f + angle;

            float x = Mathf.Cos(currentRadian) * radius;
            float y = Mathf.Sin(currentRadian) * radius;

            circle[i] = new Vector2(x, y);
        }
        return circle;
    }
}
