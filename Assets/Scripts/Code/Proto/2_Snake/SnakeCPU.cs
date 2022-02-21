// using UnityEngine;
// public class SnakeCPU : Snake
// {
//     public override float RotateSpeed => throw new System.NotImplementedException();

//     public override float ItemMagnet => throw new System.NotImplementedException();

//     public override int BulletDamage => throw new System.NotImplementedException();

//     public override int BulletHP => throw new System.NotImplementedException();

//     public override float MoveSpeed => throw new System.NotImplementedException();

//     public override void OnCollectExp(int exp)
//     {

//     }
//     public override void OnDespawn()
//     {

//     }

//     public override void OnInitialize()
//     {

//     }

//     public override void OnSpawn()
//     {

//     }

//     protected override void OnMove()
//     {
//         var target = Random.insideUnitSphere.normalized * MoveSpeed;
//         currentMove = Vector2.Lerp(currentMove, target, 0.1f);
        
//     }
// }