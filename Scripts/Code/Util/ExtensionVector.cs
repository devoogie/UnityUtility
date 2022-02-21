using UnityEngine;


public static class ExtensionVector
{
    public static Vector3 ToXZ(this Vector2 origin)
    {
        return new Vector3(origin.x,0,origin.y);
    }
    public static Vector3 ToXY(this Vector3 origin)
    {
        return new Vector3(origin.x,origin.z);
    }


}
