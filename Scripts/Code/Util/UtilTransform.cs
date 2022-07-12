using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class Utility
{
    public static void Identity(this Transform transform)
    {
        transform.localPosition = Vector3.zero;
        transform.localScale = Vector3.one;
        transform.localRotation = Quaternion.identity;
    }
    public static void Identity(this Transform transform,Transform parent)
    {
        transform.SetParent(parent);
        transform.Identity();
    }
    public static void Copy(this Transform transform,Transform target)
    {
        transform.localPosition = target.localPosition;
        transform.localScale = target.localScale;
        transform.localRotation = target.localRotation;
    }
    
}
