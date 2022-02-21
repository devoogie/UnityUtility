using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;

public class OrbPlayer : Orb
{
    public bool IsDead => UIManager.Instance.hpCurrent <= 0;

    public Vector2 Position => rigid.position;

    public Team Team => Team.Blue;
    public Rigidbody2D rigid;
    public Transform pivotSatellite;
    public List<Satellite> satellites;
    public int Count => satellites.Count;

    SpriteRenderer spriteRenderer;

    public override void OnDespawn()
    {
        CreateParticle();
    }
    private void CreateParticle()
    {
        var particle = PoolManager.Spawn<Particle>("ParticleDeadSnake");
        particle.transform.position = transform.position;
        particle.SetColor(Team.ToColor());
    }

    public override void OnInitialize()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        
    }
    Vector2 currentMove;
    float height = Define.Orbit.Height_Activate;
    float rotation = Define.Orbit.Rotation_Speed_Normal;

    public void Move(Vector2 moveDirection)
    {
        rigid.velocity = moveDirection * DataManager.InGame.GetApply(UpgradeType.MoveSpeed); 
    }
    public void MoveOrbitFar()
    {
        height = Define.Orbit.Height_Normal;
        rotation = Define.Orbit.Rotation_Speed_Activate;
    }
    public void MoveOrbitClose()
    {
        height = Define.Orbit.Height_Activate;
        rotation = Define.Orbit.Rotation_Speed_Normal;
    }

    public override void OnSpawn()
    {
        spriteRenderer.color = Team.ToColor();

    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        CameraManager.TargetPosition = Position;
        TryGetItem();
        RotateSatellite();
    }

    private void TryGetItem()
    {
        if (Team != Team.Blue)
            return;
        GetItem();
    }

    private void GetItem()
    {
        var targets = Physics2D.OverlapCircleAll(transform.position, DataManager.InGame.GetApply(UpgradeType.ItemMagnet), LayerMask.GetMask("Item"));
        var items = (from target in targets
                     let item = target.GetComponent<Item>()
                     where item != null && item.isCollected == false
                     select item);
        if (items.Count() == 0)
            return;
        foreach (var item in items)
        {
            if (item is ItemExp)
            {
                var exp = (item as ItemExp).exp;
                item.isCollected = true;
                item.transform.SetParent(transform);
                item.transform.DOLocalMove(Vector3.zero, Define.Time.Merge_Effect)
                    .OnComplete(() =>
                    {
                        DataManager.InGame.gold += exp;
                        item.Despawn();
                    });
            }
            else
            {
                item.isCollected = true;
                item.transform.SetParent(transform);
                item.transform.DOLocalMove(Vector3.zero, Define.Time.Merge_Effect)
                    .OnComplete(() =>
                    {
                        AddSatellite();
                        item.Despawn();
                    });
            }
        }
    }


    private void RotateSatellite()
    {
        
        for (int i = 0; i < satellites.Count; i++)
        {
            Satellite satellite = satellites[i];
            satellite.SetRotate(i, satellites.Count);
            satellite.MoveOrbit(height * (1 + 0.01f* satellites.Count));
        }
        pivotSatellite.Rotate(0, 0, rotation* DataManager.InGame.GetApply(UpgradeType.BulletSpeed) * Time.deltaTime);
    }

    public void AddSatellite()
    {
        var addSatelite = PoolManager.Spawn<Satellite>();
        addSatelite.Initialize(Team);
        addSatelite.transform.Identity(pivotSatellite);
        satellites.Add(addSatelite);
    }

}
public interface IPlayer
{
    bool IsDead {get;}
    Vector2 Position {get;}
    Team Team {get;}
    void OnMove(Vector2 moveDirection);

}