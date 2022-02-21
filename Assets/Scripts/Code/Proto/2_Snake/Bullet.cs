using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;
using DG.Tweening;
public class Bullet : PoolableMono
{
    public Team team;

    [SerializeField] 
    private SpriteRenderer sprite;
    [SerializeField] 
    private Rigidbody2D rigid;
    public void Initialize(Team team)
    {
        this.team = team;
        sprite.color = team.ToColor();
    }
    public override void OnDespawn()
    {
        transform.DOKill();
        sprite.color = Color.white;
        rigid.velocity = Vector2.zero;
        transform.Identity();
    }

    public override void OnInitialize()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }
    
    public override void OnSpawn()
    {
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        var unit = other.GetComponent<Enemy>();
        if(unit == null)
            return;
        if(unit.team == Team.Gray)
            return;
        if(unit.team == team)
            return;
        unit.TakeDamage(DataManager.InGame.GetApply(UpgradeType.Damage).ToCeil());
        // unit.AddForce(transform.up * 5);
        CreateParticle();
        
    }

    private void DespawnAnimation()
    {
        CreateParticle();
        Despawn();
    }

    private void CreateParticle()
    {
        var particle = PoolManager.Spawn<Particle>("ParticleDeadBullet");
        particle.transform.position = transform.position;
    }
}
