using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PoolMono : Pool<PoolableMono>
{
    public Transform Branch {get;private set;}
    protected override PoolableMono Clone()
    {
        var clone = GameObject.Instantiate<PoolableMono>(_origin,Branch);
        clone.name = clone.name.Replace("(Clone)", "");
        clone.gameObject.SetActive(false);
        return clone;
    }


    public PoolMono(PoolableMono resource):base(resource)
    {
        Branch = new GameObject(resource.name).transform;
        Branch.SetParent(PoolManager.Root);
    }

    protected override PoolableMono SpawnFromDisable()
    {
        var spawn = base.SpawnFromDisable();
        spawn.OnMonoSpawn();
        return spawn;
    }


    protected override PoolableMono Add()
    {
        var create = base.Add();
        create.OnMonoInitialize();
        return create;
    }
    
    public override void Despawn(PoolableMono a)
    {
        base.Despawn(a);
        a.OnMonoDespawn();
    }
}
