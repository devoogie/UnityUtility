using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField]private Canvas main;
    public static Canvas Main => Instance.main;
    [SerializeField]private Canvas popover;
    public static Canvas Popover => Instance.popover;

    public Image hpBarImage;
    public TextMeshProUGUI hpbarText;
    
    public List<UIPopup> opens;
    public Dictionary<string,UIPopup> popups;

    public int hpMax;
    public int hpCurrent;
    public override void Initialize()
    {
        Main.worldCamera = Camera.main;
        Popover.worldCamera = Camera.main;
        opens = new List<UIPopup>();
        popups = new Dictionary<string,UIPopup>();
        hpCurrent = DataManager.InGame.GetApply(UpgradeType.Health).ToCeil();
        hpMax = hpCurrent;
        UpdateHP();
    }

    public void UpdateHP()
    {
        var ratio = hpCurrent/(float)hpMax;
        hpBarImage.fillAmount = ratio;
        hpbarText.text = hpCurrent.ToString() + "/" + hpMax.ToString();
    }

}
