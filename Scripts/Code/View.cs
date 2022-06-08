using System.Collections;
using System.Collections.Generic;

public abstract class UIFullScreen : UI
{
    public override void OnMonoShow()
    {
        base.OnMonoShow();
        if(isChild)
            return;
        rectTransform.Identity(UIManager.Instance.Main.transform);

    }
}
