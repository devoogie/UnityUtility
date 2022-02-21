using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> where T : class, new()
{
    protected static T singleton = null;
    public static T Instance
    {
        get
        {
            if (IsNull)
                singleton = new T();
            return singleton;
        }
    }

    public static bool IsNull =>  singleton == null;

    
}
