
public interface ITarget
{
    void TakeDamage(int damage);
    bool IsDead { get; }
}
public interface IAttacker
{
    float AttackTimer { get; set; }
    bool CanAttack { get;  }
    float Attack();
}