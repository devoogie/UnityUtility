using System.Collections.Generic;
using UnityEngine;

public abstract class PoolableMono : MonoBehaviour, IPoolableMono
{
    public void Despawn()
    {
        if (this == null)
            return;
        PoolManager.Despawn(this);
    }
    public abstract void OnCreate();
    public abstract void OnSpawn();
    public abstract void OnDespawn();
    public void OnClear()
    {
        Destroy(gameObject);
    }

    public List<PoolableMono> children = new List<PoolableMono>();
    
    public virtual void OnMonoDespawn()
    {
        if(statePool == StatePool.Despawn)
            return;
        statePool = StatePool.Despawn;
        gameObject.SetActive(false);
        foreach (var child in children)
        {
            if(child == this)
                continue;
            child.OnDespawn();
            child.OnMonoDespawn();
        }
    }
    bool isInitialized = false;
    StatePool statePool = StatePool.Despawn;
    protected bool isChild = false;
    public void OnMonoInitialize()
    {
        if(isInitialized)
            return;
        isInitialized = true;
        var components = transform.GetComponentsInChildren<PoolableMono>(true);
        foreach (var child in components)
        {
            if(child == this)
                continue;
            if(child.isChild)
                continue;
            child.isChild = true;
            children.Add(child);        
            child.OnCreate();
            child.OnMonoInitialize();
        }
    }
    public virtual void OnMonoSpawn()
    {
        if(statePool == StatePool.Spawn)
            return;
        statePool = StatePool.Spawn;
        gameObject.SetActive(true);
        foreach (var child in children)
        {
            if(child == this)
                continue;
            child.OnSpawn();
            child.OnMonoSpawn();
        }
    }
    
}
public enum StatePool
{
    Spawn,
    Despawn
}
public interface IPoolableMono : IPoolObject
{
    void OnMonoInitialize();
    void OnMonoSpawn();
    void OnMonoDespawn();
}
