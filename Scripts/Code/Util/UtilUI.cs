using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public static partial class Util
{
    public static void Identity(this RectTransform rectTransform)
    {
        rectTransform.localPosition = Vector3.zero;
        rectTransform.localScale = Vector3.one;
        rectTransform.localRotation = Quaternion.identity;
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector2.one;
        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;
        rectTransform.anchoredPosition = Vector2.zero;
        rectTransform.sizeDelta = Vector2.zero;
    }
    public static void Identity(this RectTransform rectTransform,Transform parent)
    {
        rectTransform.SetParent(parent);
        rectTransform.Identity();
    }
    // public static void Transition(this RectTransform from,RectTransform to,float duration,Direction direction,System.Action onComplete)
    // {
    //     Vector2 fromMin;
    //     Vector2 fromMax;
    //     Vector2 toMin;
    //     Vector2 toMax;
    //     Vector2 toSetMin;
    //     Vector2 toSetMax;

    //     toMin = new Vector2(0, 0);
    //     toMax = new Vector2(1, 1);
    //     switch (direction)
    //     {
    //         case Direction.Right:
    //             toSetMin = new Vector2(-1, 0);
    //             toSetMax = new Vector2(0, 1);
    //             fromMin = new Vector2(1, 0);
    //             fromMax = new Vector2(2, 1);
    //             break;
    //         default:
    //         case Direction.Left:
    //             toSetMin = new Vector2(1, 0);
    //             toSetMax = new Vector2(2, 1);
    //             fromMin = new Vector2(-1, 0);
    //             fromMax = new Vector2(0, 1);
    //             break;
    //     }

    //     from.Identity();
    //     to.anchorMin = toSetMin;
    //     to.anchorMax = toSetMax;
    //     to.sizeDelta = Vector2.zero;
    //     from.DOKill();
    //     to.DOKill();
    //     from.DOAnchorMin(fromMin, duration).SetEase(Ease.OutExpo).OnUpdate(() => from.offsetMin = Vector2.zero);
    //     from.DOAnchorMax(fromMax, duration).SetEase(Ease.OutExpo).OnUpdate(() => from.offsetMax = Vector2.zero);
    //     to.DOAnchorMin(toMin, duration).SetEase(Ease.OutExpo).OnUpdate(() => to.offsetMin = Vector2.zero);
    //     to.DOAnchorMax(toMax, duration).SetEase(Ease.OutExpo).OnUpdate(() => to.offsetMax = Vector2.zero).OnComplete(() => onComplete?.Invoke());
    // }
    // public static void TransitionIn(this RectTransform to, float duration, Direction direction, System.Action onComplete)
    // {
    //     Vector2 toMin;
    //     Vector2 toMax;
    //     Vector2 toSetMin;
    //     Vector2 toSetMax;

    //     toMin = new Vector2(0, 0);
    //     toMax = new Vector2(1, 1);
    //     switch (direction)
    //     {
    //         case Direction.Right:
    //             toSetMin = new Vector2(-1, 0);
    //             toSetMax = new Vector2(0, 1);
    //             break;
    //         case Direction.Up:
    //             toSetMin = new Vector2(0, -1);
    //             toSetMax = new Vector2(1, 0);
    //             break;
    //         case Direction.Down:
    //             toSetMin = new Vector2(0, 1);
    //             toSetMax = new Vector2(1, 2);
    //             break;
    //         default:
    //         case Direction.Left:
    //             toSetMin = new Vector2(1, 0);
    //             toSetMax = new Vector2(2, 1);
    //             break;
    //     }

    //     to.anchorMin = toSetMin;
    //     to.anchorMax = toSetMax;
    //     to.sizeDelta = Vector2.zero;
    //     to.DOKill();
    //     to.DOAnchorMin(toMin, duration).SetEase(Ease.OutExpo).OnUpdate(() => to.offsetMin = Vector2.zero);
    //     to.DOAnchorMax(toMax, duration).SetEase(Ease.OutExpo).OnUpdate(() => to.offsetMax = Vector2.zero).OnComplete(() => onComplete?.Invoke());
    // }
    // public static void TransitionOut(this RectTransform from, float duration, Direction direction, System.Action onComplete)
    // {
    //     Vector2 fromMin;
    //     Vector2 fromMax;
    //     switch (direction)
    //     {
    //         case Direction.Right:
    //             fromMin = new Vector2(1, 0);
    //             fromMax = new Vector2(2, 1);
    //             break;
    //         case Direction.Up:
    //             fromMin = new Vector2(0, 1);
    //             fromMax = new Vector2(1, 2);
    //             break;
    //         case Direction.Down:
    //             fromMin = new Vector2(0, -1);
    //             fromMax = new Vector2(1, 0);
    //             break;
    //         default:
    //         case Direction.Left:
    //             fromMin = new Vector2(-1, 0);
    //             fromMax = new Vector2(0, 1);
    //             break;
    //     }

    //     from.Identity();
    //     from.DOKill();
    //     from.DOAnchorMin(fromMin, duration).SetEase(Ease.OutExpo).OnUpdate(() => from.offsetMin = Vector2.zero);
    //     from.DOAnchorMax(fromMax, duration).SetEase(Ease.OutExpo).OnUpdate(() => from.offsetMax = Vector2.zero).OnComplete(() => onComplete?.Invoke());
    // }
    public static void Copy(this RectTransform origin, RectTransform target)
    {
        origin.anchorMin = target.anchorMin;
        origin.anchorMax = target.anchorMax;
        origin.anchoredPosition = target.anchoredPosition;
        origin.sizeDelta = target.sizeDelta;
        origin.pivot = target.pivot;
    }
    public static void PopDrop(this RectTransform rectTransform, int delay, float scale = 1, float startScale = 1.25f, TweenCallback onComplete = null)
    {
        rectTransform.DOKill();
        rectTransform.localScale = Vector3.one * startScale * scale;
        rectTransform.DOScale(scale, 0.35f)
                     .SetEase(Ease.InOutBack)
                     .SetDelay(0.2f * delay)
                     .OnComplete(onComplete);
    }
    public static void PopZoom(this RectTransform rectTransform, int delay, float scale = 1, float startScale = 0, TweenCallback onComplete = null)
    {
        rectTransform.DOKill();
        rectTransform.DOScale(scale, 0.35f)
                     .SetDelay(delay * 0.2f)
                     .SetEase(Ease.OutBack)
                     .OnStart(() => rectTransform.localScale = Vector3.one * startScale)
                     .OnComplete(onComplete);
    }
    public static bool IsPointerOver(this RectTransform rectTransform, Vector2 screenPosition)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(rectTransform, screenPosition, Camera.main);
    }
}
