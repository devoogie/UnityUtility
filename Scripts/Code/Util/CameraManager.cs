using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoSingleton<CameraManager>,IManager
{
    public static Vector3 TargetPosition;
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
    public static class Setting
    {
        public static float SpeedFollowMove { get; set; } = .5f;
        public static float CameraZoom { get; set; } = 15f;
        public static float SpeedZoom { get; set; } = 0.05f;
        public static float SpeedFollowZoom { get; set; } = 0.05f;
        public static bool IsFollowZoom { get; set; } = false;
    }
}