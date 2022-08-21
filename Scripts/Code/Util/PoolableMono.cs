using System.Collections.Generic;
using UnityEngine;

public abstract class PoolableMono : MonoBehaviour, IPoolableMono
{
    public void Hide()
    {
        if (this == null)
            return;
        PoolManager.Hide(this);
    }
    public abstract void OnCreate();
    public abstract void OnSpawn();
    public abstract void OnHide();
    public void OnClear()
    {
        Destroy(gameObject);
    }

    public List<PoolableMono> children = new List<PoolableMono>();
    
    public virtual void OnMonoHide()
    {
        if(statePool == StatePool.Hide)
            return;
        statePool = StatePool.Hide;
        gameObject.SetActive(false);
        foreach (var child in children)
        {
            if(child == this)
                continue;
            child.OnHide();
            child.OnMonoHide();
        }
    }
    bool isInitialized = false;
    StatePool statePool = StatePool.Hide;
    public StatePool StatePool => statePool;
    protected bool isChild = false;
    public void OnMonoCreate()
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
            child.OnMonoCreate();
        }
    }
    public virtual void OnMonoShow()
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
            child.OnMonoShow();
        }
    }
    
}
public enum StatePool
{
    Spawn,
    Hide
}
public interface IPoolableMono : IPoolObject
{
    void OnMonoCreate();
    void OnMonoShow();
    void OnMonoHide();
    void Hide();
}
