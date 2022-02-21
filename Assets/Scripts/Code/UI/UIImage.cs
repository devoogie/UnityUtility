using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIImage : UI<UIImage>
{
    public Image image;
    public void SetImage(Sprite sprite)
    {
        image.sprite = sprite;
    }
    public override void OnDespawn()
    {

    }

    public override void OnInitialize()
    {

    }

    public override void OnSpawn()
    {

    }
}
