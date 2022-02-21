using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInGamePopup : UIPopup
{
    enum Text
    {
        Gold
    }
    public override void OnInitialize()
    {
        BindText(typeof(Text));
    }
    public void SetGold()
    {
        var goldText = GetText((int)Text.Gold);
        // goldText.text = DataManager.Instance.Gold.ToString();
    }

    public override void OnSpawn()
    {
        
    }

    public override void OnDespawn()
    {

    }
}
