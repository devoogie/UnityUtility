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
    public T Show()
    {
        TryAdd();
        return Pop();
    }
    protected virtual T Pop()
    {
        var spawn = _pooled.Pop();
        _spawned.Add(spawn);
        spawn.OnShow();
        return spawn;
    }
    private void TryAdd()
    {
        if (CheckPool())
            return;
        Add();
    }

    protected virtual T Add()
    {
        var create = (T)Clone();
        create.OnCreate();
        _pooled.Push(create);
        return create;
    }

    private bool CheckPool()
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
    public virtual void Hide(T a)
    {
        a.OnHide();
        _spawned.Remove(a);
        if(_pooled.Contains(a))
            return;
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
    void OnCreate();
    void OnShow();
    void OnHide();
    void OnClear();
}