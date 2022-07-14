using UnityEngine;
public class Bezier
{
    public static Vector3 Get(float progress, params Vector3[] positions)
    {
        if (positions.Length == 1)
            return positions[0];
        if (positions.Length == 0)
            return Vector3.zero;
        var results = new Vector3[positions.Length - 1];
        for (int i = 0; i < positions.Length - 1; i++)
        {
            var current = positions[i];
            var next = positions[i + 1];
            var lerp = Vector3.Lerp(current, next, progress);
            results[i] = lerp;
        }
        return Get(progress, results);
    }
}