using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Item : PoolableMono
{
    public bool isCollected;

}
public class ItemExp : Item
{
    public int exp;
    public override void OnDespawn()
    {
        exp = 0;
        isCollected = false;
        transform.localScale = Vector3.one;
    }
    public void SetExp(int exp)
    {
        this.exp = exp + DataManager.InGame.GetApply(UpgradeType.GoldEarn).ToCeil();
        transform.localScale = Vector3.one * ( 1 + exp * 0.05f);
    }
    public override void OnInitialize()
    {
        
        
    }

    public override void OnSpawn()
    {
    }
}
