using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MEC;
using UnityEngine;

public abstract class Snake : PoolableMono
{
    public abstract float MoveSpeed {get;}
    public abstract float RotateSpeed {get;}
    public abstract float ItemMagnet {get;}
    public abstract int BulletDamage {get;}
    public abstract int BulletHP {get;}
    public abstract int Reload {get;}
    public Team Team => data.team;
    public bool IsDead => bodies.IsNullOfEmpty();

    protected Vector2 currentMove;
    protected SnakeData data;
    protected List<SnakeBody> bodies;
    protected List<Vector2> positions;
    protected float boost {get;private set;}
    public float Count => bodies.Count;
    public Vector2 Position => positions.IsNullOfEmpty() ? CameraManager.TargetPosition : positions[0];
    public Snake()
    {
        data = new SnakeData();
        bodies = new List<SnakeBody>();
        positions = new List<Vector2>();
        currentMove = Vector2.up;
    }

    public void OnInputMove(Vector2 move)
    {
        if(move == Vector2.zero)
            return;
        currentMove = move.normalized;
    }
    void Update()
    {
        if(UIPopup.Exist<UIUpgradePopup>())
            return;
        Move();
    }
    protected abstract void OnMove();
    float lastMoveTime;
    void Move()
    {
        if (bodies.Count == 0)
        {
            return;
        }
        boost = Mathf.Clamp(boost - Time.deltaTime * 10, 0, Define.Game.Boost_Tail_Destroy);
        var body = bodies[0];
        body.Move(currentMove);
        var distance = (body.transform.position.ToVector2() - positions[0]).magnitude;
        if (lastMoveTime.IsPassTime())
        {
            var replace = body.transform.position.ToVector2(); ;
            for (int i = 0; i < positions.Count; i++)
            {
                if (positions.IsValid(i) == false)
                    break;
                var temp = positions[i];
                positions[i] = replace;
                replace = temp;
            }
            lastMoveTime = Clock.Time + 1/MoveSpeed;
        }
        float ratio = 1 - (lastMoveTime - Clock.Time) * MoveSpeed;
        for (int i = 1; i < bodies.Count; i++)
        {
            if (bodies.IsValid(i) == false)
                break;
            if (bodies.IsValid(i - 1) == false)
                break;
            bodies[i].transform.position = Vector2.Lerp(positions[i], positions[i - 1], ratio);
        }

        OnMove();
    }

    public abstract void OnCollectExp(int exp);

    public void AddBody(SnakeBody add)
    {
        add.SetSnake(this, new UnitData(DataManager.InGame.GetApply(UpgradeType.Health).ToCeil(),0,0));
        add.Initialize();
        bodies.Add(add);
        positions.Add(add.transform.position);
        OnChangeTailCount();
    }

    private void OnChangeTailCount()
    {
        bodies.Sort();
        for (int i = 0; i < bodies.Count; i++)
            bodies[i].SetOrder(i);
    }

    public void RemoveBody(SnakeBody remove)
    {
        bodies.Remove(remove);
        if(bodies.Count < positions.Count)
        {
            positions.RemoveAt(positions.Count - 1);
        }
        OnChangeTailCount();
        boost += Define.Game.Boost_Tail_Destroy;
    }
    public void OnTake()
    {
        foreach (var add in bodies)
        {
            add.SetTeamColor(Team.Red);
            Wait.Second(() =>
            {
                add.SetTeamColor(Team.Blue);
            }, 0.12f);
    
        }
        if(UIManager.Instance.hpCurrent < 0)
        {
            for (int i = bodies.Count - 1; i >= 0; i--)
                bodies[i].OnDead();
        }
    }
}