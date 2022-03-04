using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoSingleton<CameraManager>, IManager
{
    public CameraSetting setting = new CameraSetting();
    public static CameraSetting Setting => Instance.setting;
    public override void Initialize()
    {
    }
    /// <summary>
    /// LateUpdate is called every frame, if the Behaviour is enabled.
    /// It is called after all Update functions have been called.
    /// </summary>


    public static Vector3 GetPositionWorldToRect(Vector3 world)
    {

        return Camera.main.WorldToScreenPoint(world);
    }

    [System.Serializable]
    public class CameraSetting
    {
        public Vector3 TargetPosition;
        public float SpeedFollowMove = .5f;
        public float CameraZoom = 15f;
        public float SpeedZoom = 0.05f;
        public float SpeedFollowZoom = 0.05f;
        public bool IsFollowZoom = false;
    }
}