using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public static partial class Util
{
    public static void Identity(this Transform transform)
    {
        transform.localPosition = Vector3.zero;
        transform.localScale = Vector3.one;
        transform.localRotation = Quaternion.identity;
    }
    public static void Identity(this Transform transform,Transform parent)
    {
        transform.SetParent(parent);
        transform.Identity();
    }
    public static void Copy(this Transform transform,Transform target)
    {
        transform.localPosition = target.localPosition;
        transform.localScale = target.localScale;
        transform.localRotation = target.localRotation;
    }
    public static void PopDrop(this Transform transform, int delay = 0, float scale = 1, float startScale = 1.25f, TweenCallback onComplete = null)
    {
        transform.DOKill();
        transform.localScale = Vector3.one * startScale * scale;
        transform.DOScale(scale, 0.35f)
                     .SetEase(Ease.InOutBack)
                     .SetDelay(0.2f * delay)
                     .OnComplete(onComplete);
    }
    public static void PopZoom(this Transform transform, int delay = 0, float scale = 1, float startScale = 0, TweenCallback onComplete = null)
    {
        transform.DOKill();
        transform.DOScale(scale, 0.35f)
                     .SetDelay(delay * 0.2f)
                     .SetEase(Ease.OutBack)
                     .OnStart(() => transform.localScale = Vector3.one * startScale)
                     .OnComplete(onComplete);
    }
}
