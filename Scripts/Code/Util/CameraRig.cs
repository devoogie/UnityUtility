using UnityEngine;
public class CameraRig : MonoBehaviour
{
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, CameraManager.Setting.Position, CameraManager.Setting.SpeedFollowMove);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(CameraManager.Setting.Rotation), CameraManager.Setting.SpeedFollowMove);



        if (CameraManager.Setting.IsFollowZoom == false)
        {
            float cameraZoom = CameraManager.Setting.CameraZoom;
            int width = Screen.width;
            int height = Screen.height;
            if (CameraManager.Setting.IsFitTarget)
                cameraZoom *= height / (float)width;
            float zoom = Mathf.Lerp(
                Camera.main.orthographicSize,
                cameraZoom,
                CameraManager.Setting.SpeedZoom);
            Camera.main.orthographicSize = zoom;
            return;
        }

    }
}