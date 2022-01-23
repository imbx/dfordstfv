using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(RaycastPlayer))]
[RequireComponent(typeof(RaycastScene))]
public class CController : MonoBehaviour
{
    [Header("Camera Movement")]
    public CameraSettings m_CConfig;
    public Transform m_PitchController;

    private float m_Yaw = 0f;
    private float m_Pitch = 0f;

    [Header("Movement")]
    private CharacterController m_characterController;
    public CharacterVariables m_CVars;
    public ControllerData m_PlayerMovement;

    [Header("Stats")]
    private float timer = 0;
    private float defaultYPos = 0;

    private RaycastPlayer rcPlayer;

    private bool isMoving = false;
    float lastTranslate = 0;
    int bobbingDirection = 1;

    void OnEnable()
    {
        rcPlayer = GetComponent<RaycastPlayer>();
        m_characterController = GetComponent<CharacterController>();
        defaultYPos = m_PitchController.localPosition.y;

        if(m_CVars.isLoadingData) 
        {
            transform.position = m_CVars.PlayerPosition;
            transform.rotation = Quaternion.Euler(0, m_Yaw, 0);
            m_PitchController.localRotation = Quaternion.Euler(m_Pitch, 0, 0);
        }
    }

    void FixedUpdate()
    {
        if(m_CVars.CanLook) CameraMovement();
        else isMoving = false;
        if(m_CVars.CanMove) Movement();
        else isMoving = false;

        Debug.DrawRay(m_PitchController.position, m_PitchController.forward * m_CVars.VisionRange, Color.green);
        rcPlayer.ExecuteRaycast(m_PitchController.position, m_PitchController.forward, m_CVars.VisionRange, m_PlayerMovement.isInputDown);

        // isOnCarpets = CheckOnCarpet();
        HeadBobbing();
    }
    #region UpdateFunctions
    private void CameraMovement()
    {
        Vector2 mouseAxis = m_PlayerMovement.CameraAxis;
        m_Yaw = m_Yaw + mouseAxis .x * m_CConfig.YawSpeed * Time.deltaTime * (m_CConfig.InvertX ? -1 : 1);
        m_Pitch = m_Pitch + mouseAxis.y * m_CConfig.PitchSpeed * Time.deltaTime * (m_CConfig.InvertY ? -1 : 1);
        m_Pitch = Mathf.Clamp(m_Pitch, m_CConfig.MinPitch, m_CConfig.MaxPitch);

        transform.rotation = Quaternion.Euler(0, m_Yaw, 0);
        m_PitchController.localRotation = Quaternion.Euler(m_Pitch, 0, 0);

        m_CVars.CameraRotations = new Vector3(m_Pitch, 0, m_Yaw);
    }

    private void Movement()
    {
        Vector3 l_Forward = transform.forward;
        Vector3 l_Right = transform.right;
        Vector3 l_Movement = Vector3.zero;
        Vector2 l_Axis = m_PlayerMovement.Axis;

        l_Movement = l_Forward * l_Axis.x;
        l_Movement += l_Right * l_Axis.y;
        l_Movement += transform.up * -1 * m_CVars.Gravity * Time.deltaTime;

        isMoving = Mathf.Abs(l_Movement.x) > 0.1f || Mathf.Abs(l_Movement.z) > 0.1f;

        l_Movement.Normalize();

        l_Movement = l_Movement * m_CVars.Speed * Time.deltaTime;

        m_characterController.Move(l_Movement);
        m_CVars.PlayerPosition = transform.position;
    }

    private void HeadBobbing()
    {
        if(isMoving)
        {
            float waveslice = Mathf.Sin(timer);
            timer += Time.deltaTime * m_CVars.BobbingSpeed;

            Vector2 l_Axis = m_PlayerMovement.Axis;
            float translateChange = waveslice * m_CVars.BobbingAmount;
            float totalAxes = Mathf.Abs(l_Axis.x) + Mathf.Abs(l_Axis.y);
            totalAxes = Mathf.Clamp (totalAxes, 0.0f, 1.0f);
            translateChange = totalAxes * translateChange;


            if (bobbingDirection == 1 && translateChange < lastTranslate)
            {
                bobbingDirection = -1;
            }
            if (bobbingDirection == -1 && translateChange > lastTranslate)
            {
                bobbingDirection = 1;
            }

            lastTranslate = translateChange;
            m_PitchController.localPosition =
                new Vector3(
                    m_PitchController.localPosition.x,
                    defaultYPos + translateChange,
                    m_PitchController.localPosition.z
                    );
            return;
                    
        }
        timer = 0;
        m_PitchController.localPosition =
            new Vector3(
                m_PitchController.localPosition.x,
                Mathf.Lerp(m_PitchController.localPosition.y, defaultYPos, Time.deltaTime * m_CVars.BobbingSpeed),
                m_PitchController.localPosition.z
                );
        
    }
    #endregion

    #region Reset
    public void Reset()
    {
        //m_startPosition.ApplyTo(transform);
        // m_CVars.OnAfterDeserialize();
    }
    #endregion
}
