using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public static partial class Util
{
    public static void Shake(this Transform transform, float duration = 0.5f,float strength = 1,int vibrato = 10,TweenCallback endCallback = null)
    {
        transform.DOKill();
        transform.Identity();
        transform.DOShakePosition(duration,strength,vibrato).OnComplete(endCallback);
    }
    public static void Zoom(this Transform transform, int delay)
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(1,0.35f)
                     .SetDelay(delay*0.25f)
                     .SetEase(Ease.OutBack);
    }
    public static void Drop(this Transform transform, int delay)
    {
        transform.localScale = Vector3.one*1.25f;
        transform.DOScale(1,0.35f)
                     .SetEase(Ease.InOutBack)
                     .SetDelay(0.25f*delay);
    }

}
