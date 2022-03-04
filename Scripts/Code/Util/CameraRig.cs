using UnityEngine;
public class CameraRig : MonoBehaviour
{
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, CameraManager.Setting.TargetPosition, CameraManager.Setting.SpeedFollowMove);
        if (CameraManager.Setting.IsFollowZoom == false)
        {
            float zoom = Mathf.Lerp(
                Camera.main.orthographicSize,
                CameraManager.Setting.CameraZoom,
                CameraManager.Setting.SpeedZoom);
            Camera.main.orthographicSize = zoom;
            return;
        }

    }
}