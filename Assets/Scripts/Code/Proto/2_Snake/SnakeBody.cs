using System.Net.Mime;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class SnakeBody : Unit, System.IComparable<SnakeBody>
{
    [SerializeField] Transform model;
    public SnakeBodyData node;
    public Snake snake;
    public int order;

    public void SetOrder(int sortingOrder)
    {
        order = sortingOrder;
        spriteRenderer.sortingOrder = -sortingOrder;
        text.sortingOrder = -sortingOrder;
        float wait = 1;//DataManager.InGame.GetApply(UpgradeType.Reload);
        var sequence = 1 - order/(float)snake.Count;
        intervalAction = Clock.Time - sequence; 
        
    }
    public int CompareTo(SnakeBody other)
    {
        return other.node.Level.CompareTo(node.Level);
    }

    public void Initialize()
    {
        SetTeamColor(snake.Team);
    }
    public void SetSnake(Snake snake,UnitData snakeNode)
    {
        this.snake = snake;
        this.Data = snakeNode;
        team = snake.Team;
        transform.SetParent(snake.transform);
        SetTeamColor(snake.Team);
        UpdateHealth();
    }
    


    

    public void SetTeamColor(Team team)
    {
        spriteRenderer.color = team.ToColor();
    }
    public override void OnDespawn()
    {
        snake = null;
        team = Team.Gray;
        spriteRenderer.color = Color.white;
        text.text = "";
    }

    public override void OnInitialize()
    {
        base.OnInitialize();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        CheckAdd(other);
    }

    private void CheckAdd(Collider2D other)
    {
        if (team == Team.Gray)
            return;
        if (other.IsTouchingLayers(LayerMask.GetMask("Block")) == false)
            return;
        var check = other.GetComponent<SnakeBody>();
        if (check == null)
            return;
        if (check.team == team)
            return;
        if (check.team != Team.Gray)
            return;
        snake.AddBody(check);
    }
    public override void OnSpawn()
    {
        snake = null;
    }

    public void Move(Vector3 moveDirection)
    {
        ApplyMapBound(moveDirection);
    }

    private void ApplyMapBound(Vector3 moveDirection)
    {
        var bound = moveDirection * Time.deltaTime * snake.MoveSpeed;
        var check = transform.position + bound;
        bool isOutX = Mathf.Abs(check.x) > Define.Game.MapSize;
        bool isOutY = Mathf.Abs(check.y) > Define.Game.MapSize;
        if (isOutX)
        {
            bound.x = 0;
            bound = bound.normalized * Time.deltaTime * snake.MoveSpeed;
        }
        if(isOutY)
        {
            bound.y = 0;
            bound = bound.normalized * Time.deltaTime * snake.MoveSpeed;
        }
        
        transform.Translate(bound);
    }


    private void LookModel(Vector3 moveDirection)
    {
        // float rotateSpeed = snake.RotateSpeed * Time.deltaTime;
        // var angle = Mathf.Clamp(Vector2.SignedAngle(model.up, moveDirection), -rotateSpeed, rotateSpeed);
        var angle = Vector2.SignedAngle(model.up, moveDirection);
        angle += Random.Range(-15f,15f);
        model.Rotate(0, 0, angle);
    }

    float intervalAction = 0;
    void Update()
    {
        if (spriteRenderer.sortingOrder != 0)
            TryAim();
        TryAction();
        TryGetItem();
    }
    private void TryAim()
    {
        var targets = Physics2D.OverlapCircleAll(transform.position, Camera.main.orthographicSize, LayerMask.GetMask("Block"));
        var enemies = (from target in targets
                       where target.GetComponent<Enemy>() != null
                       orderby Vector2.Distance(transform.position, target.transform.position)
                       select target);
        if (enemies.Count() == 0)
            return;
        var closest = enemies.First();
        LookModel(closest.transform.position - transform.position);
    }

    private void TryGetItem()
    {
        if (team != Team.Blue)
            return;
        if (snake == null)
            return;
        GetItem();
    }

    private void GetItem()
    {
        var targets = Physics2D.OverlapCircleAll(transform.position, DataManager.InGame.GetApply(UpgradeType.ItemMagnet), LayerMask.GetMask("Item"));
        var items = (from target in targets
                     let item = target.GetComponent<ItemExp>()
                     where item != null && item.isCollected == false
                     select item);
        if (items.Count() == 0)
            return;
        foreach (var item in items)
        {
            var exp = item.exp;
            item.isCollected = true;
            item.transform.SetParent(transform);
            item.transform.DOLocalMove(Vector3.zero, Define.Time.Merge_Effect)
                 .OnComplete(() =>
                 {
                     snake?.OnCollectExp(exp);
                     item.Despawn();
                 });
        }
    }

    private void TryAction()
    {
        if (snake == null)
            return;
        float wait =11;// DataManager.InGame.GetApply(UpgradeType.Reload);
        if (intervalAction.IsPassTime(wait) == false)
            return;
        Action();
        intervalAction = Clock.Time;
    }

    public void Action()
    {
        var bullet = PoolManager.Spawn<Bullet>();
        bullet.transform.position = transform.position;
        bullet.Initialize(snake.Team);
    }

    protected override void OnTakeDamage()
    {
        snake.OnTake();
    }

    public override void OnDead()
    {
        CreateParticle();
        snake.RemoveBody(this);
        Despawn();
    }

    private void CreateParticle()
    {
        var particle = PoolManager.Spawn<Particle>("ParticleDeadSnake");
        particle.transform.position = transform.position;
        particle.SetColor(team.ToColor());
    }
}
