using System.Security.Cryptography.X509Certificates;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class UI<T> : PoolableMono
{
    public static System.Action OnUpdateInfo;
    public RectTransform rectTransform => transform as RectTransform;
    Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();

    protected void Bind<TBind, TEnum>() where TBind : UnityEngine.Object
    {
        var type = typeof(TEnum);
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
    protected void BindText<TEnum>() => Bind<TextMeshProUGUI, TEnum>();
    protected void BindImage<TEnum>() => Bind<Image, TEnum>();
    protected void BindButton<TEnum>() => Bind<Button, TEnum>();
    protected TextMeshProUGUI GetText(System.Enum index) { return GetBind<TextMeshProUGUI>(Convert.ToInt32(index)); }
    protected Image GetImage(System.Enum index) { return GetBind<Image>(Convert.ToInt32(index)); }
    protected Button GetButton(System.Enum index) { return GetBind<Button>(Convert.ToInt32(index)); }
    public override void OnMonoSpawn()
    {
        base.OnMonoSpawn();
        rectTransform.SetParent(UIManager.Main.transform);
        rectTransform.localScale = Vector3.one;
        rectTransform.rotation = Quaternion.identity;
    }
    void OnEnable()
    {
        OnUpdateInfo += UpdateInfo;
    }
    void OnDisable()
    {
        OnUpdateInfo -= UpdateInfo;

    }
    public abstract void UpdateInfo();

}
