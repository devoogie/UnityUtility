using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class Unit : PoolableMono
{
    public Team team;
    protected UnitData Data;
    public bool IsDead => Data.healthCurrent <= 0;
    protected TextMeshPro text;
    protected Rigidbody2D rigid;
    protected SpriteRenderer spriteRenderer;
    public bool TakeDamage(int damage)
    {
        Data.healthCurrent -= damage;
        OnDamage();
        if (Data.healthCurrent <= 0)
        {
            Data.healthCurrent = 0;
            OnDead();
            return true;
        }
        return false;
    }
    public void AddForce(Vector2 dir)
    {
        rigid.AddForce(dir, ForceMode2D.Impulse);
    }
    private void OnDamage()
    {
        UpdateHealth();
        OnTakeDamage();
    }

    protected void UpdateHealth()
    {
        text.text = Data.healthCurrent.ToString();
    }

    protected abstract void OnTakeDamage();
    public abstract void OnDead();

    public override void OnInitialize()
    {
        text = GetComponentInChildren<TextMeshPro>();
        rigid = GetComponentInChildren<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

    }

}

public class Enemy : Unit
{
    Team playerTeam;
    public void Initialize(Team playerTeam, int wave)
    {
        this.playerTeam = playerTeam;
        SetTeam(playerTeam, wave);
        spriteRenderer.color = team.ToColor();
        UpdateHealth();
    }

    private void SetTeam(Team playerTeam, int wave)
    {
        var random = (Team)Random.Range(0, (int)Team.Max);
        var health = (wave * 2f).ToCeil();
        switch (random)
        {
            case Team.Red:
                this.Data = new UnitData(health * 2, wave * 2, 2);
                team = Team.Red;
                transform.localScale = Vector3.one * 3f;
                break;
            case Team.Orange:
                this.Data = new UnitData((health * 0.5f).ToCeil(), (wave * 0.5f).ToCeil(), 5);
                team = Team.Orange;
                transform.localScale = Vector3.one * 2f;
                break;
            default:
            case Team.Pink:
                this.Data = new UnitData(health, wave, 3);
                transform.localScale = Vector3.one * 2f;
                team = Team.Pink;
                break;
        }
    }


    public override void OnSpawn()
    {

    }
    public override void OnDespawn()
    {
        var particle = PoolManager.Spawn<Particle>("ParticleDeadTriangle");
        particle.transform.position = transform.position;
        particle.SetColor(team.ToColor());

        team = Team.Gray;
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (IsDead)
            return;
        if (team == Team.Gray)
            return;
        if (other.IsTouchingLayers(LayerMask.GetMask("Block")) == false)
            return;
        var check = other.GetComponent<OrbPlayer>();
        if (check == null)
            return;
        UIManager.Instance.hpCurrent -= 1;
        UIManager.Instance.UpdateHP();
        // check.snake.OnTake();
        // check.TakeDamage(Data.healthCurrent);
        OnDead();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (rigid == null)
            return;
        Vector2 movingVector = (CameraManager.TargetPosition.ToVector2() - rigid.position).normalized * Mathf.Clamp(Data.speed, 1, 10);
        rigid.velocity = Vector2.Lerp(rigid.velocity, movingVector, 0.05f);
    }

    public override void OnDead()
    {
        DropExp();
        TryAddTail();

        Despawn();
    }

    private void DropExp()
    {
        GameManager.Instance.itemSystem.CreateExp(transform.position);
    }

    private void TryAddTail()
    {
        var tailCount = 3;

        var pro = Random.Range(0, 100);
        if (pro > tailCount)
            return;
        var body = PoolManager.Spawn<ItemSatellite>();
        body.transform.position = transform.position.Add(Random.insideUnitCircle);
    }

    protected override void OnTakeDamage()
    {

    }
}

public struct UnitData
{
    public int healthCurrent;
    public int healthMax;
    public int damage;
    public float speed;
    public UnitData(int health, int damage, int speed)
    {
        healthCurrent = health;
        healthMax = health;
        this.damage = damage;
        this.speed = speed * 0.85f;
    }
}
