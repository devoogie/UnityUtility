using System.Security.Cryptography.X509Certificates;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class UI<T> : PoolableMono
{
    public RectTransform rectTransform => transform as RectTransform;
    Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();
   
    protected void Bind<TBind>(Type type) where TBind : UnityEngine.Object
    {
        var names = Enum.GetNames(type);
        var objects = new UnityEngine.Object[names.Length];
        _objects.Add(typeof(TBind), objects);

        for (int i = 0; i < names.Length; i++)
        {
            if (typeof(TBind) == typeof(GameObject))
                objects[i] = Util.FindChild(gameObject, names[i], false);
            else
                objects[i] = Util.FindChild<TBind>(gameObject, names[i], false);
        }
    }
    protected TBind GetBind<TBind>(int index) where TBind : UnityEngine.Object
    {
        if (_objects.TryGetValue(typeof(TBind), out var objects) == false)
            return null;

        return objects[index] as TBind;
    }
    protected void BindText(Type type) => Bind<TextMeshProUGUI>(type);
    protected void BindImage(Type type) => Bind<Image>(type);
    protected void BindButton(Type type) => Bind<Button>(type);
    protected TextMeshProUGUI GetText(int index) { return GetBind<TextMeshProUGUI>(index); }
    protected Image GetImage(int index) { return GetBind<Image>(index); }
    protected Button GetButton(int index) { return GetBind<Button>(index); }
    public override void OnMonoSpawn()
    {
        base.OnMonoSpawn();
        rectTransform.SetParent(UIManager.Main.transform);
        rectTransform.localScale = Vector3.one;
        rectTransform.rotation = Quaternion.identity;
    }
    public virtual void UpdateInfo()
    {

    }

}