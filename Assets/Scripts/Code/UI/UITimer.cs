using System.Collections;
using System.Collections.Generic;
using MEC;
using TMPro;
using UnityEngine;

public class UITimer : UI<UITimer>
{
    [SerializeField]
    private TextMeshProUGUI text;
    public override void OnDespawn()
    {

    }

    public override void OnInitialize()
    {

    }

    public override void OnSpawn()
    {

    }
    public void SetTimer(int time)
    {
        Timing.RunCoroutine(timer(time));
    }
    IEnumerator<float> timer(int time)
    {
        while(time > 0)
        {
            time--;
            text.text = time.ToString();
            yield return Timing.WaitForSeconds(1);
        }
    }
    
    
}
