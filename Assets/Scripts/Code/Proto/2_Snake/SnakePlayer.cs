// using System.ComponentModel;
// using UnityEngine;
// public class SnakePlayer : Snake
// {
//     public override float MoveSpeed => Mathf.Clamp(DataManager.InGame.GetApply(UpgradeType.MoveSpeed) + boost, 1, float.MaxValue);
//     public override float RotateSpeed => MoveSpeed * 40;

//     public override float ItemMagnet => DataManager.InGame.GetApply(UpgradeType.MoveSpeed);
//     public override int BulletDamage => DataManager.InGame.GetApply(UpgradeType.Damage).ToCeil();
//     public override int BulletHP => DataManager.InGame.GetApply(UpgradeType.BulletSpeed).ToCeil();

//     // public override int Reload => DataManager.InGame.GetApply(UpgradeType.Reload).ToCeil();

//     public override void OnCollectExp(int exp)
//     {
//         DataManager.InGame.gold += exp;
//     }

//     public override void OnDespawn()
//     {

//     }

//     public override void OnInitialize()
//     {   
//         GameManager.Input.onMove += OnInputMove;
//         data.team = Team.Blue;
       
//     }

//     public override void OnSpawn()
//     {

//     }

//     protected override void OnMove()
//     {
//         if(bodies.IsNullOfEmpty())
//             return;
//         CameraManager.TargetPosition = bodies.First().transform.position;
//     }
// }
