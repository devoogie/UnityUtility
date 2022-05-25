using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoSingleton<CameraManager>
{
    public static CameraSetting Setting = new CameraSetting();
    public CameraRig CameraRig;
    public static Vector3 GetPositionWorldToRect(Vector3 world)
    {
        return Camera.main.WorldToScreenPoint(world);
    }

    public override void Initialize()
    {

    }
    void LateUpdate()
    {
        CameraRig.Move();
        if (Setting.IsFollowZoom == false)
        {
            CameraRig.Zoom();
        }
    }
    

    [System.Serializable]
    public class CameraSetting
    {
        public Vector3 Position;
        public Vector3 Rotation;
        public float SpeedFollowMove = .5f;
        public float CameraZoom =  10f;
        public float SpeedZoom = 0.05f;
        public float SpeedFollowZoom = 0.05f;
        public bool IsFollowZoom = false;
        public bool IsFitTarget = false;
        public Color BackgroundColor = ColorSet.Sky;
        public float CameraWidth => Screen.width / (float)Screen.height * CameraZoom;
        public float CameraHeight => CameraZoom;
    }
}