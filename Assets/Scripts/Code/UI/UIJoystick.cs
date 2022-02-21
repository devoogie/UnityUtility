using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIJoystick : UIPopup
{
    enum Image
    {
        Fill
    }
    public override void OnDespawn()
    {
        BindImage(typeof(Image));
    }

    public override void OnInitialize()
    {

    }

    public override void OnSpawn()
    {

    }
    public void SetJoyFill(float amount)
    {
        var image = GetImage((int)Image.Fill);
        image.fillAmount = amount;
    }
}
