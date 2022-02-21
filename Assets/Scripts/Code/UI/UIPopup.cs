using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class UIPopup : UI<PoolableMono>
{
    public override void OnMonoSpawn()
    {
        base.OnMonoSpawn();
        rectTransform.Identity(UIManager.Main.transform);
    }
    public override void OnMonoDespawn()
    {
        base.OnMonoDespawn();
        UIPopup.Close(name,this);
    }
    public void OnClickClose()
    {
        Despawn();
    }
}

public partial class UIPopup
{
    public static bool Exist<T>()
    {
        return UIManager.Instance.popups.ContainsKey(typeof(T).ToString());
    }
    public static T FindOrAdd<T>() where T : UIPopup
    {
        bool result = UIManager.Instance.popups.TryGetValue(typeof(T).ToString(), out UIPopup value);
        if (result == false)
        {
            value = AddForce<T>();
            UIManager.Instance.popups.Add(typeof(T).ToString(),value);
        }
        value.OnMonoSpawn();
        return value as T;
    }
    
    public static T AddForce<T>() where T : UIPopup
    {
        var popup = PoolManager.Spawn<T>();
        UIManager.Instance.opens.Add(popup);
        return popup as T;
    }
    public void CloseTop()
    {
        if(UIManager.Instance.opens.Count > 0)
            UIManager.Instance.opens[UIManager.Instance.opens.Count - 1].Despawn();
    }
    public static void Close<T>(string name,T closePopup) where T : UIPopup
    {
        UIManager.Instance.popups.Remove(name);
        UIManager.Instance.opens.Remove(closePopup);
    }

}