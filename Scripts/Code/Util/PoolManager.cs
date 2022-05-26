using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoSingleton<PoolManager>
{
    public static Transform Root => Instance.transform;
    public static System.Action OnEventReset;
    public Dictionary<string, PoolMono> Pools = new Dictionary<string, PoolMono>();
    public override void Initialize()
    {
        OnEventReset?.Invoke();
        Pools.Clear();
        AddPool("Pool");
        // AddPool("Pool/Bullet");
        
    }
    void AddPool(string path)
    {
        var pools = Resources.LoadAll<PoolableMono>(path);
        foreach (var resource in pools)
        {
            var pool = new PoolMono(resource);
            Pools.Add(resource.name, pool);
        }
    }
    public static void Hide<T>(T destroy) where T : PoolableMono
    {
        var pool = Instance.Pools[destroy.name];
        pool.Hide(destroy);
        destroy.transform.Identity(pool.Branch);
    }
    public static void HideAll(string name)
    {
        var pool = Instance.Pools[name];
        pool.Clear();
    }
    public static T Show<T>() where T : PoolableMono
    {
        var pool = Instance.Pools[typeof(T).ToString()];
        var poolableMono = pool.Show();
        return poolableMono as T;
    }
    public static T Show<T>(System.Enum name) where T : PoolableMono
    {
        return Show<T>(name.ToString());
    }
    public static T Show<T>(string name) where T : PoolableMono
    {
        var pool = Instance.Pools[name];
        var poolableMono = pool.Show();
        return poolableMono as T;
    }
    public static int GetCount(string key)
    {
        var pool = Instance.Pools[key];
        return pool.SpawnCount;
    }
}