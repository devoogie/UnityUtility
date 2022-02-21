using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSatellite : Item
{
    public override void OnDespawn()
    {
        isCollected = false;
        transform.localScale = Vector3.one;
    }
    public void SetExp(int exp)
    {
        transform.localScale = Vector3.one * ( 1 + exp * 0.05f);
    }
    public override void OnInitialize()
    {
        
        
    }

    public override void OnSpawn()
    {
    }
}
