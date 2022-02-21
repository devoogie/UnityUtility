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

}
