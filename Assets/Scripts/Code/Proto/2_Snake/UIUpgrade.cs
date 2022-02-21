using UnityEngine;

public class UIUpgrade : UI<UIUpgrade>
{
    enum Image
    {
        BG,
        Upgrade,
    }
    enum Text
    {
        Title,
        Description,
    }
    [SerializeField] RectTransform inner;
    public override void OnDespawn()
    {
        onClick = null;
        
    }

    public override void OnInitialize()
    {
        BindImage(typeof(Image));
        BindText(typeof(Text));
    }
    UpgradeType upgradeType;
    public event System.Action onClick;
    public void Initialize(UpgradeType upgradeType)
    {
        this.upgradeType = upgradeType;
        UpdateInfo();
    }
    public override void UpdateInfo()
    {
        SetTitle(upgradeType.ToTitle());
        SetDescription(DataManager.InGame.GetCost(upgradeType).ToString());
        SetIcon(upgradeType);
        SetBGColor(DataManager.InGame.IsUpgradable(upgradeType)? Grade.Legend.ToColor(): Color.gray);
    }
    public override void OnSpawn()
    {
        
        
    }
    public void SetTitle(string title)
    {
        GetText((int)Text.Title).text = title;
    
    }
    public void SetDescription(string description)
    {
        GetText((int)Text.Description).text = description;        
    }
    public void SetBGColor(Color color)
    {
        var image = GetImage((int)Image.BG);
        image.color = color;
    }
    
    public void SetIcon(UpgradeType upgrade)
    {
        var image = GetImage((int)Image.Upgrade);
        image.sprite = upgrade.ToSprite();
    }

    public void OnClick()
    {
        inner.PopDrop(0);
        var result = DataManager.InGame.TryUpgrade(upgradeType);
        UpdateInfo();
        onClick?.Invoke();
        if(result == false)
        {
            var popup = UIPopup.FindOrAdd<UIToast>();
            popup.SetText("Not enough gold");
            return;
        }
        var particle = PoolManager.Spawn<Particle>("ParticleCoin");
        particle.transform.position = transform.position;
    }
}