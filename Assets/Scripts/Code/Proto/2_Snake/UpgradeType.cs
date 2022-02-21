using UnityEngine;

public enum UpgradeType
{
    Health,
    GoldEarn,
    Damage,
    // Reload,
    MoveSpeed,
    BulletSpeed,
    ItemMagnet,
    TailCount,
    Boost,
    Max,
}

public static class UpgradeTypeExtension
{
    public static string ToTitle(this UpgradeType upgradeType)
    {
        switch(upgradeType)
        {
            case UpgradeType.Health:
                return "HealthMax";
            case UpgradeType.GoldEarn:
                return "GoldEarn";
            case UpgradeType.Damage:
                return "Damage";
            // case UpgradeType.Reload:
            //     return "Reload";
            case UpgradeType.MoveSpeed:
                return "MoveSpeed";
            case UpgradeType.BulletSpeed:
                return "BulletSpeed";
            case UpgradeType.ItemMagnet:
                return "ItemMagnet";
            case UpgradeType.TailCount:
                return "TailCount";
            case UpgradeType.Boost:
                return "Boost";
        }
        return "Default";
    }
    public static Sprite ToSprite(this UpgradeType upgradeType)
    {
        string spriteName =upgradeType  switch
        {
            UpgradeType.Health => "plus",
            UpgradeType.Damage => "Attack",
            _ => upgradeType.ToString(),
        };
        return ResourceManager.GetSprite(spriteName);
    }
    
    public static int ToCost(this UpgradeType upgradeType,int currentLevel)
    {
        int baseCost = upgradeType  switch
        {
            UpgradeType.Health => Define.Upgrade.Cost.Cost_HealthMax,
            UpgradeType.GoldEarn => Define.Upgrade.Cost.Cost_GoldEarn,
            UpgradeType.Damage => Define.Upgrade.Cost.Cost_Damage,
            // UpgradeType.Reload => Define.Upgrade.Cost.Cost_Reload,
            UpgradeType.MoveSpeed => Define.Upgrade.Cost.Cost_MoveSpeed,
            UpgradeType.BulletSpeed => Define.Upgrade.Cost.Cost_BulletSpeed,
            UpgradeType.ItemMagnet => Define.Upgrade.Cost.Cost_ItemMagnet,
            UpgradeType.TailCount => Define.Upgrade.Cost.Cost_TailCount,
            UpgradeType.Boost => Define.Upgrade.Cost.Cost_Boost,
            _ => 0,
        };
        return baseCost + (baseCost * (1+ 0.5f * currentLevel)).ToFloor();
    }
    
}