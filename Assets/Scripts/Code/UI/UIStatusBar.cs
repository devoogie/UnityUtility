using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class UIStatusBar : UI<UIStatusBar>
{
    public const float RepeatHP = 1f;
    public RectTransform Mana;
    public Material material;
    UnitStatus previous;
    public enum Texts
    {
        HealthText
    }
    public enum Bars
    {
        HealthBar,
        ManaBar,
    }
    public override void OnInitialize()
    {
        BindImage(typeof(Bars));
        BindText(typeof(Texts));
        SetMaterialHP();
    }

    private void SetMaterialHP()
    {
        var hpMaterial = Instantiate(material);
        hpMaterial.CopyPropertiesFromMaterial(material);
        material = hpMaterial;
        GetImage((int)Bars.HealthBar).material = material;
    }

    public override void OnSpawn()
    {
    }

    public override void OnDespawn()
    {

    }
    private float GetMaxHPBar(UnitStatus unitStatus)
    {
        var barMax = Mathf.Max(unitStatus.healthMax, unitStatus.currentHealth + unitStatus.currentShield);
        if (barMax == 0)
            barMax = 1;
        return barMax;
    }
    public void SetTeam(int team)
    {
        Color color;
        if(team == 1)
            color = "#00ED10".ToColor();
        else
            color = "#E13400".ToColor();
        material.SetColor(PropertyHPBar._ColorHP.ToString(), color);

    }
    public void Show(UnitStatus unitStatus)
    {
        ShowHealth(unitStatus);
        ShowMana(unitStatus);
        ShowDamage(unitStatus);
    }

    private void ShowDamage(UnitStatus unitStatus)
    {
        int damage = previous.currentHealth - unitStatus.currentHealth;
        if (damage > 0)
            UIText.Show(damage.ToString(), Color.white, transform.position);
        previous = unitStatus;
    }

    private void ShowHealth(UnitStatus unitStatus)
    {
        GetText((int)Texts.HealthText).text = unitStatus.currentHealth.ToString();
        float repeat = GetMaxHPBar(unitStatus) / RepeatHP;
        material.SetFloat(PropertyHPBar._Repeat.ToString(), repeat);
        material.SetFloat(PropertyHPBar._RatioHealth.ToString(), unitStatus.currentHealth / GetMaxHPBar(unitStatus));
        material.SetFloat(PropertyHPBar._RatioShield.ToString(), unitStatus.currentShield / GetMaxHPBar(unitStatus));
        material.SetFloat(PropertyHPBar._RatioPivot.ToString(), (unitStatus.currentHealth + unitStatus.currentShield) / GetMaxHPBar(unitStatus));
        material.SetFloat(PropertyHPBar._RatioCurrent.ToString(), (unitStatus.currentHealth + unitStatus.currentShield) / GetMaxHPBar(unitStatus));
    }
    private void ShowMana(UnitStatus unitStatus)
    {
        var manaObject = Mana.gameObject;
        var isShowMana = unitStatus.manaMax > 0;
        manaObject.SetActive(isShowMana);
        if (isShowMana)
        {
            var ratio = unitStatus.currentMana / (float)unitStatus.manaMax;
            var manaBar = GetImage((int)Bars.ManaBar);
            manaBar.fillAmount = ratio;
            manaBar.color = Mathf.Approximately(ratio,1f)?ColorSet.Grade_Legend:ColorSet.Grade_Rare;
        }   

    }

    enum PropertyHPBar
    {
        _ColorHP,
        _Repeat,
        _RatioHealth,
        _RatioShield,
        _RatioPivot,
        _RatioCurrent,
    }

}