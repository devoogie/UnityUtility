using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CameraManager
{
    public static CameraSetting Setting;
    
    public static Vector3 GetPositionWorldToRect(Vector3 world)
    {
        return Camera.main.WorldToScreenPoint(world);
    }

    [System.Serializable]
    public class CameraSetting
    {
        public Vector3 Position;
        public Vector3 Rotation;
        public float SpeedFollowMove = .5f;
        public float CameraZoom = 15f;
        public float SpeedZoom = 0.05f;
        public float SpeedFollowZoom = 0.05f;
        public bool IsFollowZoom = false;
        public bool IsFitTarget = false;
    }
}