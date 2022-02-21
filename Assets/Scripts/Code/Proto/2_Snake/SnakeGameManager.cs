// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using Rule.Snake;
// using MEC;

// namespace Rule.Snake
// {
//     public enum InGameState
//     {
//         None,
//         Begin,
//         Prepare,
//         Battle,
//         LevelUp,
//         Result,
//         End
//     }
//     public abstract class BaseInGameState : IState<InGameState>
//     {
//         public abstract InGameState LeaveTo { get; }

//         public abstract void Initialize();
//         public void LeaveState()
//         {

//         }
//         public abstract void OnEnter();
//         public abstract void OnLeave();
//         public abstract void OnProgress();
//     }
//     public class BeginState : IState<InGameState>
//     {
//         public InGameState LeaveTo => throw new System.NotImplementedException();

//         public void Initialize()
//         {
//         }

//         public void LeaveState()
//         {
//         }

//         public void OnEnter()
//         {
//         }

//         public void OnLeave()
//         {
//         }

//         public void OnProgress()
//         {
//         }
//     }
//     public class PrepareState : IState<InGameState>
//     {
//         public InGameState LeaveTo => throw new System.NotImplementedException();

//         public void Initialize()
//         {
//         }

//         public void LeaveState()
//         {
//         }

//         public void OnEnter()
//         {
//         }

//         public void OnLeave()
//         {
//         }

//         public void OnProgress()
//         {
//         }
//     }
//     public class BattleState : IState<InGameState>
//     {
//         public InGameState LeaveTo => throw new System.NotImplementedException();

//         public void Initialize()
//         {
//         }

//         public void LeaveState()
//         {
//         }

//         public void OnEnter()
//         {
//         }

//         public void OnLeave()
//         {
//         }

//         public void OnProgress()
//         {
//         }
//     }
//     public class LevelUpState : IState<InGameState>
//     {
//         public InGameState LeaveTo => throw new System.NotImplementedException();

//         public void Initialize()
//         {
//         }

//         public void LeaveState()
//         {
//         }

//         public void OnEnter()
//         {
//         }

//         public void OnLeave()
//         {
//         }

//         public void OnProgress()
//         {
//         }
//     }
//     public class ResultState : IState<InGameState>
//     {
//         public InGameState LeaveTo => throw new System.NotImplementedException();

//         public void Initialize()
//         {
//         }

//         public void LeaveState()
//         {
//         }

//         public void OnEnter()
//         {
//         }

//         public void OnLeave()
//         {
//         }

//         public void OnProgress()
//         {
//         }
//     }
//     public class EndState : IState<InGameState>
//     {
//         public InGameState LeaveTo => throw new System.NotImplementedException();

//         public void Initialize()
//         {
//         }

//         public void LeaveState()
//         {
//         }

//         public void OnEnter()
//         {
//         }

//         public void OnLeave()
//         {
//         }

//         public void OnProgress()
//         {
//         }
//     }

// }
// public class SnakeGameManager : GameManager
// {
//     SnakePlayer player;

//     public Team PlayerTeam => player.Team;

//     public override bool IsGameOver => player.IsDead;

//     public override void Initialize()
//     {
//         base.Initialize();
//         StartGame();
//     }

//     private void StartGame()
//     {
//         UIPopup.FindOrAdd<UIJoystick>();


//         CreatePlayer();
//         Timing.RunCoroutine(spawnEnemy());
//     }

//     private void CreatePlayer()
//     {
//         player = PoolManager.Spawn<SnakePlayer>();
//         Debug.Log("TailCount: " + DataManager.InGame.GetApply(UpgradeType.TailCount));
//         for (int i = 0; i < DataManager.InGame.GetApply(UpgradeType.TailCount); i++)
//         {
//             SnakeBody body = PoolManager.Spawn<SnakeBody>();
//             player.AddBody(body);
//         }
//     }

//     IEnumerator<float> spawnEnemy()
//     {
//         int wave = 0;
        
//         while (player.IsDead == false)
//         {
//             wave++;
//             Debug.Log("Wave: " + wave);
//             int repeat = (10 + 10 * wave);
//             for (int i = 0; i < repeat; i++)
//             {
//                 var screenRatio = Screen.width > Screen.height ?
//                       Screen.width / Screen.height :
//                       Screen.height / Screen.width;
//                 var screenSize = CameraManager.Setting.CameraZoom * 1.2f * screenRatio;
//                 var enemy = PoolManager.Spawn<Enemy>();
//                 enemy.Initialize(Team.Blue, wave);
//                 enemy.transform.position = player.Position.Add(Random.insideUnitCircle.normalized * screenSize);
//                 Debug.Log($"Spawn Enemy {wave}:{i}");
//                 if(IsGameOver)
//                 {
//                     break;
//                 }
//                 yield return Timing.WaitForSeconds(Define.Game.Interval_Spawn_Enemy);
//                 yield return Timing.WaitUntilTrue(() => PoolManager.GetCount<Enemy>() <= 100);
//             }
//             Debug.Log("Spawn Wait");

//             yield return Timing.WaitUntilTrue(() => PoolManager.GetCount<Enemy>() == 0 || IsGameOver);

//             Debug.Log("Spawn End");
//             if (IsGameOver)
//             {
//                 break;
//             }
//             else
//             {
//                 var toast = UIPopup.FindOrAdd<UIToast>();
//                 toast.SetText($"Stage {wave}\nClear");

//                 yield return Timing.WaitForSeconds(Define.Time.Game_Wait);
//                 UIUpgradePopup popup = ShowUpgrade();
//                 yield return Timing.WaitUntilTrue(() => UIPopup.Exist<UIUpgradePopup>() == false);
                
//                 Debug.Log("Spawn sdfdsf");

//             }

//         }
//         OnGameOver();
//     }

//     private static UIUpgradePopup ShowUpgrade()
//     {
//         var popup = UIPopup.FindOrAdd<UIUpgradePopup>();
//         for (int i = 0; i < (int)UpgradeType.Max; i++)
//         {
//             popup.Create((UpgradeType)i);
//         }

//         return popup;
//     }

//     private void OnGameOver()
//     {
//         var popup = UIPopup.FindOrAdd<UIToast>();
//         popup.SetText("Game Over");
//     }
// }


namespace Define
{
    public partial class Game
    {
        public static float Interval_Spawn_Enemy = 0.2f;

        public static float Tail_Distance = 1f;
        public static float Boost_Tail_Destroy = 5f;
        public static int Amount_Tail_Start = 3;
        public static int Amount_Health_Start = 3;
        public static int MapSize =18;

    }
 
    public class Upgrade
    {
        public class Cost
        {
            public static int Cost_HealthMax = 5;
            public static int Cost_GoldEarn = 200;
            public static int Cost_Damage = 5;
            public static int Cost_Reload = 5;
            public static int Cost_MoveSpeed = 5;
            public static int Cost_BulletSpeed = 10;
            public static int Cost_ItemMagnet = 5;
            public static int Cost_TailCount = 50;
            public static int Cost_Boost = 5;
        }
        public class Increase
        {
            public static int HealthMax = 5;
            public static int GoldEarn = 2;
            public static int Damage = 1;
            public static float Reload = .05f;
            public static int MoveSpeed = 2;
            public static float BulletSpeed = .2f;
            public static float ItemMagnet = .5f;
            public static int TailCount = 1;
            public static float Boost = .5f;
        }
        public class Start
        {
            public static int HealthMax = 10;
            public static int GoldEarn = 1;
            public static int Damage = 1;
            public static float Reload = 1;
            public static int MoveSpeed = 4;
            public static int BulletSpeed = 1;
            public static float ItemMagnet = .25f;
            public static int TailCount = 3;
            public static float Boost = 5f;
        }
        
    }
}