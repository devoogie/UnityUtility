using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class UpgradeData
{
    public int healthMax;
    public int goldEarn;
    public int damage;
    public int reload;
    public int moveSpeed;
    public int BulletSpeed;
    public int itemMagnet;
    public int tailCount;
    public int boost;

    public static UpgradeData operator +(UpgradeData a, UpgradeData b)
    {
        var result = new UpgradeData();
        result.healthMax = a.healthMax + b.healthMax;
        result.goldEarn = a.goldEarn + b.goldEarn;
        result.damage = a.damage + b.damage;
        result.reload = a.reload + b.reload;
        result.moveSpeed = a.moveSpeed + b.moveSpeed;
        result.BulletSpeed = a.BulletSpeed + b.BulletSpeed;
        result.itemMagnet = a.itemMagnet + b.itemMagnet;
        result.tailCount = a.tailCount + b.tailCount;
        result.boost = a.boost + b.boost;
        return result;
    }
    
}
public static class UpgradeExtension
{
    public static int GetValue(this UpgradeData data , UpgradeType upgradeType)
    {
        switch(upgradeType)
        {
            case UpgradeType.Health:
                return data.healthMax;
            case UpgradeType.GoldEarn:
                return data.goldEarn;
            case UpgradeType.Damage:
                return data.damage;
            // case UpgradeType.Reload:
            //     return data.reload;
            case UpgradeType.MoveSpeed:
                return data.moveSpeed;
            case UpgradeType.BulletSpeed:
                return data.BulletSpeed;
            case UpgradeType.ItemMagnet:
                return data.itemMagnet;
            case UpgradeType.TailCount:
                return data.tailCount;
            case UpgradeType.Boost:
                return data.boost;
            default:
                return 0;
        }
    }
    public static void SetValue(this UpgradeData data , UpgradeType upgradeType, int value)
    {
        switch(upgradeType)
        {
            case UpgradeType.Health: data.healthMax = value; break;
            case UpgradeType.GoldEarn: data.goldEarn = value; break;
            case UpgradeType.Damage: data.damage = value; break;
            // case UpgradeType.Reload: data.reload = value; break;
            case UpgradeType.MoveSpeed: data.moveSpeed = value; break;
            case UpgradeType.BulletSpeed: data.BulletSpeed = value; break;
            case UpgradeType.ItemMagnet: data.itemMagnet = value; break;
            case UpgradeType.TailCount: data.tailCount = value; break;
            case UpgradeType.Boost: data.boost = value; break;
            default: break;
        }
    }
    
}

// [System.Serializable]
// public class PlayerData
// {
//     public int jewel;
//     public UpgradeData upgrade;
// }