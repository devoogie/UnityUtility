using System;
using UnityEngine;

[System.Serializable]
public struct UnitStatus
{
    public int cost;
    public int healthMax;
    public int manaMax;
    public int manaStarting;
    public int attackDamage;
    public int attackRange;
    public float attackSpeed;
    public float moveSpeed;

    public int currentHealth;
    public int currentShield;
    public int currentMana;

    public UnitStatus(int cost, int healthMax, int manaMax, int manaStarting, int attackDamage, float attackSpeed, int attackRange, float moveSpeed)
    {
        this.cost = cost;
        this.healthMax = healthMax;
        this.manaMax = manaMax;
        this.manaStarting = manaStarting;
        this.attackDamage = attackDamage;
        this.attackSpeed = attackSpeed;
        this.attackRange = attackRange;
        this.moveSpeed = moveSpeed;
        this.currentHealth = healthMax;
        this.currentShield = 0;
        this.currentMana = manaStarting;
    }

    public static UnitStatus ApplyLevel(UnitStatus a, int level)
    {
        a.healthMax = a.healthMax * level;
        a.manaMax = a.manaMax - level;
        a.manaStarting = a.manaStarting + level;
        a.attackDamage = a.attackDamage + Mathf.RoundToInt(level*0.5f);
        a.attackRange = a.attackRange + Mathf.RoundToInt(level*0.5f);
        a.attackSpeed = a.attackSpeed * (1 + level * 0.15f);
        a.moveSpeed = a.moveSpeed * (level * 0.15f);
        a.currentHealth = a.healthMax;
        a.currentShield = 0;
        a.currentMana = a.manaStarting;
        return a;
    }

}
