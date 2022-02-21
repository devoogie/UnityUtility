using System.Collections;
using System.Collections.Generic;
using MEC;
using UnityEngine;

public class OrbitGameManager : GameManager
{
    OrbPlayer player;

    public Team PlayerTeam => player.Team;

    public override bool IsGameOver => player.IsDead;

    public override void Initialize()
    {
        base.Initialize();
        StartGame();

    }
    private void StartGame()
    {
        UIPopup.FindOrAdd<UIJoystick>();


        CreatePlayer();
        Timing.RunCoroutine(spawnEnemy());
    }

    private void CreatePlayer()
    {
        player = PoolManager.Spawn<OrbPlayer>();
        Input.onMove += player.Move;
        Input.onFireBegin += player.MoveOrbitFar;
        Input.onFireEnd += player.MoveOrbitClose;

        for(int i= 0 ; i < DataManager.InGame.GetApply(UpgradeType.TailCount).ToCeil(); i++)
            player.AddSatellite();

    }

    IEnumerator<float> spawnEnemy()
    {
        int wave = 0;

        while (player.IsDead == false)
        {
            wave++;
            // UIManager.Instance.hpCurrent = UIManager.Instance.hpMax;
            UIManager.Instance.UpdateHP();
            int repeat = (10 + 10 * wave);
            for (int i = 0; i < repeat; i++)
            {
                var screenRatio = Screen.width > Screen.height ?
                      Screen.width / Screen.height :
                      Screen.height / Screen.width;
                var screenSize = CameraManager.Setting.CameraZoom * 1.2f * screenRatio;
                var enemy = PoolManager.Spawn<Enemy>();
                enemy.Initialize(Team.Blue, wave);
                enemy.transform.position = player.Position.Add(Random.insideUnitCircle.normalized * screenSize);
                
                if (IsGameOver)
                {
                    break;
                }
                yield return Timing.WaitForSeconds(Define.Game.Interval_Spawn_Enemy);
                yield return Timing.WaitUntilTrue(() => PoolManager.GetCount<Enemy>() <= 100);
            }
            

            yield return Timing.WaitUntilTrue(() => PoolManager.GetCount<Enemy>() == 0 || IsGameOver);

            if (IsGameOver)
            {
                break;
            }
            else
            {
                var toast = UIPopup.FindOrAdd<UIToast>();
                toast.SetText($"Stage {wave}\nClear");

                yield return Timing.WaitForSeconds(Define.Time.Game_Wait);
                UIUpgradePopup popup = ShowUpgrade();
                yield return Timing.WaitUntilTrue(() => UIPopup.Exist<UIUpgradePopup>() == false);
                int diff = DataManager.InGame.GetApply(UpgradeType.TailCount).ToCeil() - player.Count;
                if(diff > 0)
                {
                    for (int i = 0; i < diff; i++)
                    {
                        player.AddSatellite();
                    }
                }
                

            }

        }
        OnGameOver();
    }

    private static UIUpgradePopup ShowUpgrade()
    {
        var popup = UIPopup.FindOrAdd<UIUpgradePopup>();
        for (int i = 0; i < (int)UpgradeType.Max; i++)
        {
            popup.Create((UpgradeType)i);
        }

        return popup;
    }

    private void OnGameOver()
    {
        player.Despawn();

        var popup = UIPopup.FindOrAdd<UIToast>();
        popup.SetText("Game Over");
    }
}
namespace Define
{
    public class Orbit
    {
        public const float Height_Normal = 2;
        public const float Height_Activate = 4;
        public const float Rotation_Speed_Normal = 120;
        public const float Rotation_Speed_Activate = 240;
    }
}