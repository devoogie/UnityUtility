using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pool<T> where T : IPoolObject
{
    protected Stack<T> _pooled;
    protected List<T> _spawned;
    protected T _origin;
    public int SpawnCount => _spawned.Count;
    public Pool(T origin)
    {
        _pooled = new Stack<T>();
        _spawned = new List<T>();
        this._origin = origin;
    }
    public T Spawn()
    {
        CheckAddPool();
        return SpawnFromDisable();
    }
    private T SpawnFromDisable()
    {
        var spawn = _pooled.Pop();
        _spawned.Add(spawn);
        spawn.OnSpawn();
        return spawn;
    }
    private void CheckAddPool()
    {
        if (CheckSpawnable())
            return;
        Add();
    }

    private void Add()
    {
        var create = (T)Clone();
        create.OnInitialize();
        _pooled.Push(create);
    }

    private bool CheckSpawnable()
    {
        while(_pooled.Count > 0)
        {
            if(_pooled.Peek() == null)
                _pooled.Pop();
            else
                break;
        }
        return _pooled.Count > 0;
    }
    public void Despawn(T a)
    {
        a.OnDespawn();
        _spawned.Remove(a);
        _pooled.Push(a);
    }
    public virtual void Clear()
    {
        for(int i = _spawned.Count - 1; i >= 0; i--)
        {
            T t = _spawned[i];
            t.OnClear();
        }
        _spawned.Clear();
        _pooled.Clear();
    }
    protected abstract T Clone();
}
public interface IPoolObject
{
    void OnInitialize();
    void OnSpawn();
    void OnDespawn();
    void OnClear();
}