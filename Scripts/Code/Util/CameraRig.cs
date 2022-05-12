using UnityEngine;
public class CameraRig : MonoBehaviour
{
    void LateUpdate()
    {
        Move();
        if (CameraManager.Setting.IsFollowZoom == false)
        {
            Zoom();
        }
    }

    private static void Zoom()
    {
        float cameraZoom = GetZoomRatio();
        float zoom = Mathf.Lerp(
            Camera.main.orthographicSize,
            cameraZoom,
            CameraManager.Setting.SpeedZoom);
        Camera.main.orthographicSize = zoom;
    }

    private static float GetZoomRatio()
    {
        float cameraZoom = CameraManager.Setting.CameraZoom;
        int width = Screen.width;
        int height = Screen.height;
        if (CameraManager.Setting.IsFitTarget)
            cameraZoom *= height / (float)width;
        return cameraZoom;
    }

    private void Move()
    {
        var position = Vector3.Lerp(transform.position, CameraManager.Setting.Position, CameraManager.Setting.SpeedFollowMove);
        var rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(CameraManager.Setting.Rotation), CameraManager.Setting.SpeedFollowMove);
        transform.SetPositionAndRotation(position, rotation);
    }  
}