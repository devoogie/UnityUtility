using Define;
public class DataManager : Singleton<DataManager>,IManager
{
    public InGameData inGameData;
    public static InGameData InGame => Instance.inGameData;

    public DataManager()
    {
        Initialize();
        
    }

    public void Initialize()
    {
        inGameData = new InGameData();
        inGameData.health = 20;
    }
    public static int GetCost(UpgradeType upgrade)
    {
        var level = InGame.upgrade.GetValue(upgrade);

        return level;
    }
}

public class PlayerData
{
    public int level;
    public int exp;
    
}

[System.Serializable]
public class InGameData
{
    public int gold;
    public int health;
    public UpgradeData upgrade = new UpgradeData();

    public float GetApply(UpgradeType type)
    {
        var value = upgrade.GetValue(type);
        return type switch
        {
            UpgradeType.Health => Upgrade.Start.HealthMax + value * Upgrade.Increase.HealthMax,
            UpgradeType.GoldEarn => Upgrade.Start.GoldEarn + value * Upgrade.Increase.GoldEarn,
            UpgradeType.Damage => Upgrade.Start.Damage + value * Upgrade.Increase.Damage,
            // UpgradeType.Reload => Upgrade.Start.Reload - value * Upgrade.Increase.Reload,
            UpgradeType.MoveSpeed => Upgrade.Start.MoveSpeed + value * Upgrade.Increase.MoveSpeed,
            UpgradeType.BulletSpeed => Upgrade.Start.BulletSpeed + value * Upgrade.Increase.BulletSpeed,
            UpgradeType.ItemMagnet => Upgrade.Start.ItemMagnet + value * Upgrade.Increase.ItemMagnet,
            UpgradeType.TailCount => Upgrade.Start.TailCount + value * Upgrade.Increase.TailCount,
            UpgradeType.Boost => Upgrade.Start.Boost + value * Upgrade.Increase.Boost,
            _ => 0,
        };
    }
    public int GetCost(UpgradeType type)
    {
        var level = upgrade.GetValue(type);
        return type.ToCost(level);
    }
    public bool IsUpgradable(UpgradeType type)
    {
        var cost = GetCost(type);
        if(gold < cost)
            return false;
        return true;
    }
    public bool TryUpgrade(UpgradeType type)
    {
        var cost = GetCost(type);
        bool isUpgradable = IsUpgradable(type);
        if(isUpgradable == false)
            return isUpgradable;
        gold -= cost;
        if(type == UpgradeType.Health)
        {
            UIManager.Instance.hpCurrent += GetApply(type).ToCeil();
            UIManager.Instance.hpMax += GetApply(type).ToCeil();
        }

        upgrade.SetValue(type, upgrade.GetValue(type) + 1);
        return isUpgradable;
    }

}