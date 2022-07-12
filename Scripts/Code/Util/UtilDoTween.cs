using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if DoTween
using DG.Tweening;

public static class TweenUtility
{
    public static void Shake(this Transform transform, float duration = 0.5f, float strength = 1, int vibrato = 10, TweenCallback endCallback = null)
    {
        transform.DOKill();
        transform.Identity();
        transform.DOShakePosition(duration, strength, vibrato).OnComplete(endCallback);
    }
    public static void Zoom(this Transform transform, int delay)
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(1, 0.35f)
                     .SetDelay(delay * 0.25f)
                     .SetEase(Ease.OutBack);
    }
    public static void Drop(this Transform transform, int delay)
    {
        transform.localScale = Vector3.one * 1.25f;
        transform.DOScale(1, 0.35f)
                     .SetEase(Ease.InOutBack)
                     .SetDelay(0.25f * delay);
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
#endif