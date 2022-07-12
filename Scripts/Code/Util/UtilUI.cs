using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class Utility
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
    public static void Identity(this RectTransform rectTransform, Transform parent)
    {
        rectTransform.SetParent(parent);
        rectTransform.Identity();
    }
    public static void Copy(this RectTransform origin, RectTransform target)
    {
        origin.anchorMin = target.anchorMin;
        origin.anchorMax = target.anchorMax;
        origin.anchoredPosition = target.anchoredPosition;
        origin.sizeDelta = target.sizeDelta;
        origin.pivot = target.pivot;
    }
    public static bool IsPointerOver(this RectTransform rectTransform, Vector2 screenPosition)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(rectTransform, screenPosition, Camera.main);
    }
}
