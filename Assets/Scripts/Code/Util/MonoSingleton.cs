using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T singleton;
    public static T Instance
    {
        get 
        {
            if (singleton == null)
            {
                singleton = GameObject.FindObjectOfType<T>();
                if (singleton == null)
                {
                    GameObject monoObject = new GameObject(typeof(T).ToString());
                    monoObject.AddComponent<T>();
                    singleton = monoObject.GetComponent<T>();
                }
            }
            return singleton;
        }
    }
    protected virtual void Awake()
    {
        if (singleton != null && singleton != this)
        {
            Destroy(gameObject);
            return;
        
        }
        if(singleton == null)
            Initialize();
        singleton = this as T;
    }
    // [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public abstract void Initialize();

}