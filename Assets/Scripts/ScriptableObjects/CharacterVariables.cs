using UnityEngine;

[CreateAssetMenu]
public class CharacterVariables : ScriptableObject, ISerializationCallbackReceiver {
    [Header("Basic Vars")]
    public float Speed = 4.5f;
    public float Gravity = 200f;
    public float VisionRange = 50f;

    [Header("Head bobbing")]
    public float BobbingSpeed = 13;
    public float BobbingAmount = 0.025f;

    [Header("Others")]

    public Vector3 CameraRotations = Vector3.zero;
    public bool CanMove = true;
    public bool CanLook = true;

    public bool IsBlockedByEvent = false;
    public Vector3 PlayerPosition = Vector3.zero;
    public bool isLoadingData = false;


    public void OnAfterDeserialize() { }
    public void OnBeforeSerialize() { }

    public void ControlMovement(bool isTrue) {
        CanMove = isTrue;
        CanLook = isTrue;
    }
}