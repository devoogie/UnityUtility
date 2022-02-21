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
        var pools = Resources.LoadAll<PoolableMono>("Pool");
        foreach (var resource in pools)
        {
            var pool = new PoolMono(resource);
            Pools.Add(resource.name, pool);
        }
    }
    public static void Despawn<T>(T despawn) where T : PoolableMono
    {
        var pool = Instance.Pools[despawn.name];
        pool.Despawn(despawn);
        despawn.transform.Identity(pool.Branch);
    }
    public static T Spawn<T>() where T : PoolableMono
    {
        var pool = Instance.Pools[typeof(T).ToString()];
        var poolableMono = pool.Spawn();
        poolableMono.OnMonoSpawn();
        return poolableMono as T;
    }
    public static T Spawn<T>(System.Enum name) where T : PoolableMono
    {
        return Spawn<T>(name.ToString());
    }
    public static T Spawn<T>(string name) where T : PoolableMono
    {
        var pool = Instance.Pools[name];
        var poolableMono = pool.Spawn();
        poolableMono.OnMonoSpawn();
        return poolableMono as T;
    }
    public static int GetCount<T>() where T : PoolableMono
    {
        var pool = Instance.Pools[typeof(T).ToString()];
        return pool.SpawnCount;
    }
}