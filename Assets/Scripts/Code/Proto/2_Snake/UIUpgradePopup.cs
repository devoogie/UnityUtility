using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIUpgradePopup : UIPopup
{
    [SerializeField] SpawnerUIUpgrade upgradeSpawner;
    enum Text
    {
        Gold
    }
    public override void OnDespawn()
    {
        upgradeSpawner.Clear();
    }

    public override void OnInitialize()
    {
        upgradeSpawner = GetComponentInChildren<SpawnerUIUpgrade>();
        BindText(typeof(Text));
    }

    public override void OnSpawn()
    {
        UpdateGold();   
    }
    public void Create(UpgradeType upgradeType)
    {
        var upgradeUI = upgradeSpawner.CreateUI();
        upgradeUI.Initialize(upgradeType);
        upgradeUI.onClick += UpdateGold;
        // upgradeUI.SetBGColor(grade);
    }

    private void UpdateGold()
    {
        var text = GetText((int)(Text.Gold));
        text.text = DataManager.InGame.gold.ToString();
        upgradeSpawner.UpdateInfos();
    }
    
}
