using UnityEngine;

public class Satellite : Orb
{
    public Team Team;
    public Transform pivot;
    public Bullet bullet;
    public void Initialize(Team team)
    {
        Team = team;
        bullet.Initialize(team);
    }
    
    public override void OnDespawn()
    {

    }

    public override void OnInitialize()
    {

    }

    public override void OnSpawn()
    {

    }
    const int maxOrbit = 10;
    public void SetRotate(int index , int satelliteCount)
    {
        float angle = 360f / satelliteCount;
        pivot.localRotation = Quaternion.Slerp(pivot.localRotation, Quaternion.Euler(0, 0, index * angle),0.01f) ;
    }
    public void MoveOrbit(float height)
    {
        bullet.transform.localPosition = Vector3.Lerp(bullet.transform.localPosition ,Vector2.up * height,0.05f);
    }
    
}
