using System.Collections;
using System.Collections.Generic;
using MEC;
using UnityEngine;

public class UIToast : UIPopup
{
    
    enum Text
    {
        Toast,
    }
    public override void OnDespawn()
    {

    }

    public override void OnInitialize()
    {
        BindText(typeof(Text));
    }
    public void SetText(string toastText)
    {
        GetText((int)Text.Toast).text = toastText;
        AutoClose();
    }
    public override void OnSpawn()
    {
        
    }

    private void AutoClose()
    {
        Timing.KillCoroutines(gameObject);
        Timing.RunCoroutine(close(), gameObject);
    }

    IEnumerator<float> close()
    {
        yield return Timing.WaitForSeconds(Define.Time.Close_Toast);
        Despawn();
    }
}
