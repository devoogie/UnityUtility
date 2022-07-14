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

    public static void Transition(this RectTransform from, RectTransform to, float duration, Direction direction, System.Action onComplete)
    {
        Vector2 fromMin;
        Vector2 fromMax;
        Vector2 toMin;
        Vector2 toMax;
        Vector2 toSetMin;
        Vector2 toSetMax;

        toMin = new Vector2(0, 0);
        toMax = new Vector2(1, 1);
        switch (direction)
        {
            case Direction.Right:
                toSetMin = new Vector2(-1, 0);
                toSetMax = new Vector2(0, 1);
                fromMin = new Vector2(1, 0);
                fromMax = new Vector2(2, 1);
                break;
            default:
            case Direction.Left:
                toSetMin = new Vector2(1, 0);
                toSetMax = new Vector2(2, 1);
                fromMin = new Vector2(-1, 0);
                fromMax = new Vector2(0, 1);
                break;
        }

        from.Identity();
        to.anchorMin = toSetMin;
        to.anchorMax = toSetMax;
        to.sizeDelta = Vector2.zero;
        from.DOKill();
        to.DOKill();
        from.DOAnchorMin(fromMin, duration).SetEase(Ease.OutExpo).OnUpdate(() => from.offsetMin = Vector2.zero);
        from.DOAnchorMax(fromMax, duration).SetEase(Ease.OutExpo).OnUpdate(() => from.offsetMax = Vector2.zero);
        to.DOAnchorMin(toMin, duration).SetEase(Ease.OutExpo).OnUpdate(() => to.offsetMin = Vector2.zero);
        to.DOAnchorMax(toMax, duration).SetEase(Ease.OutExpo).OnUpdate(() => to.offsetMax = Vector2.zero).OnComplete(() => onComplete?.Invoke());
    }
    public static void TransitionIn(this RectTransform to, float duration, Direction direction, System.Action onComplete)
    {
        Vector2 toMin;
        Vector2 toMax;
        Vector2 toSetMin;
        Vector2 toSetMax;

        toMin = new Vector2(0, 0);
        toMax = new Vector2(1, 1);
        switch (direction)
        {
            case Direction.Right:
                toSetMin = new Vector2(-1, 0);
                toSetMax = new Vector2(0, 1);
                break;
            case Direction.Up:
                toSetMin = new Vector2(0, -1);
                toSetMax = new Vector2(1, 0);
                break;
            case Direction.Down:
                toSetMin = new Vector2(0, 1);
                toSetMax = new Vector2(1, 2);
                break;
            default:
            case Direction.Left:
                toSetMin = new Vector2(1, 0);
                toSetMax = new Vector2(2, 1);
                break;
        }

        to.anchorMin = toSetMin;
        to.anchorMax = toSetMax;
        to.sizeDelta = Vector2.zero;
        to.DOKill();
        to.DOAnchorMin(toMin, duration).SetEase(Ease.OutExpo).OnUpdate(() => to.offsetMin = Vector2.zero);
        to.DOAnchorMax(toMax, duration).SetEase(Ease.OutExpo).OnUpdate(() => to.offsetMax = Vector2.zero).OnComplete(() => onComplete?.Invoke());
    }
    public static void TransitionOut(this RectTransform from, float duration, Direction direction, System.Action onComplete)
    {
        Vector2 fromMin;
        Vector2 fromMax;
        switch (direction)
        {
            case Direction.Right:
                fromMin = new Vector2(1, 0);
                fromMax = new Vector2(2, 1);
                break;
            case Direction.Up:
                fromMin = new Vector2(0, 1);
                fromMax = new Vector2(1, 2);
                break;
            case Direction.Down:
                fromMin = new Vector2(0, -1);
                fromMax = new Vector2(1, 0);
                break;
            default:
            case Direction.Left:
                fromMin = new Vector2(-1, 0);
                fromMax = new Vector2(0, 1);
                break;
        }

        from.Identity();
        from.DOKill();
        from.DOAnchorMin(fromMin, duration).SetEase(Ease.OutExpo).OnUpdate(() => from.offsetMin = Vector2.zero);
        from.DOAnchorMax(fromMax, duration).SetEase(Ease.OutExpo).OnUpdate(() => from.offsetMax = Vector2.zero).OnComplete(() => onComplete?.Invoke());
    }
}
#endif