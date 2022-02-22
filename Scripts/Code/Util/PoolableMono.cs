using System.Collections.Generic;
using UnityEngine;

public abstract class PoolableMono : MonoBehaviour, IPoolableMono
{
    public virtual void OnMonoSpawn()
    {
        gameObject.SetActive(true);
    }
    public void Despawn()
    {
        if (this == null)
            return;
        PoolManager.Despawn(this);
        OnMonoDespawn();
    }
    public abstract void OnInitialize();
    public abstract void OnSpawn();
    public abstract void OnDespawn();

    public virtual void OnMonoDespawn()
    {
        gameObject.SetActive(false);
    }
    public void OnClear()
    {
        Destroy(gameObject);
    }
}
public interface IPoolableMono : IPoolObject
{
    void OnMonoSpawn();
    void OnMonoDespawn();
}
