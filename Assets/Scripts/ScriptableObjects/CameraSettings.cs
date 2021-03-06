using UnityEngine;

[CreateAssetMenu(fileName = "SceneRooms", menuName = "BoxScripts/CameraSettings", order = 0)]
public class CameraSettings : ScriptableObject {
    public float MinPitch = -55f;
    public float MaxPitch = 50f;
    public float YawSpeed = 11f;
    public float PitchSpeed = 5.5f;
    public bool InvertY = true;
    public bool InvertX = false;
}