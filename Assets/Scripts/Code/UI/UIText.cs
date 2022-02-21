using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MEC;
using TMPro;
using UnityEngine;

public class UIText : UI<UIText>
{
    public TextMeshProUGUI uiText;
    private float duration;
    

    private static Vector2 ScreenSize => new Vector2(Screen.width, Screen.height);
    

    public void SetColor(Color color)
    {
        uiText.color = color;
    }
    public void SetText(string text)
    {
        uiText.text = text;
    }
    public void SetDuration(float duration)
    {
        this.duration = duration;
    }
    public void SetPosition(Vector2 position)
    {
        rectTransform.position = position;
    }
    public void MoveJump()
    {
        rectTransform.DOKill();
        float jumpPower = Screen.height * 0.05f;
        Vector2 circle = Random.insideUnitCircle;
        circle.x = Mathf.Abs(circle.x);
        circle.y = Mathf.Abs(circle.y);
        Vector2 jumpDestination = rectTransform.position.Add(circle * jumpPower) ;
        rectTransform.DOJump(jumpDestination,jumpPower,1,duration,true).OnComplete(Despawn);
    }
    public void MoveDirection(Vector2 moveDirection)
    {
        rectTransform.DOKill();
        var adjustMoveDirection = Screen.height * moveDirection * 0.15f;
        Vector2 vector2 = rectTransform.position.Add(adjustMoveDirection);
        rectTransform.DOMove(vector2, duration, true).OnComplete(Despawn);
    }
    
    public static UIText Show(string text, Color color, Vector3 position, float duration = 1)
    {
        var textEffect = PoolManager.Spawn<UIText>();
        textEffect.SetText(text);
        textEffect.SetColor(color);
        textEffect.SetDuration(duration);
        textEffect.SetPosition(position);
        Wait.Second(textEffect.Despawn,duration);
        return textEffect;
    }

    public static UIText ShowMove(string text, Color color, Vector3 position, float duration = 1,bool isUp = true)
    {
        var textEffect = PoolManager.Spawn<UIText>();
        textEffect.SetText(text);
        textEffect.SetColor(color);
        textEffect.SetDuration(duration);
        textEffect.SetPosition(position);
        if (isUp)
            textEffect.MoveDirection(Vector2.up);
        else
            textEffect.MoveJump();
        return textEffect;
    }

    public override void OnInitialize()
    {

    }

    public override void OnSpawn()
    {
        rectTransform.SetParent(UIManager.Popover.transform);
        rectTransform.localScale = Vector3.one;
        rectTransform.sizeDelta = ScreenSize;
        uiText.text = string.Empty;
        uiText.color = Color.white;
    }
    public override void OnDespawn()
    {

    }
}
