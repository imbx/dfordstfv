using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using BoxScripts;
public class Door : ActionBase
{
    public float Speed = 2f;
    public Vector3 Angle;
    [SerializeField]
    [ReadOnly]
    private string MovId;

    public override bool Execute()
    {
        DBot.SendError("Door", " Trying to execute.");
        if (MovId == null || MovId == "")
        {
            DBot.SendError("Door", " Trying to create a movement.");
            MovId = MovementManager.Instance.CreateMov(transform, new DataPackage(transform.position, transform.eulerAngles + Angle), Speed, false, true);
        }
        else if(!MovementManager.Instance.CheckExecution(MovId)) MovementManager.Instance.Flip(MovId);
        else DBot.SendError("Door", " Trying to execute, but is already being executed.");
        
        return true;
    }
    private void OnDrawGizmos() {
        #if UNITY_EDITOR
            Handles.color = Utils.GetSolidBlueGUI;
            Handles.DrawSolidArc(
                transform.position,
                Angle.normalized,
                transform.forward.normalized,
                Angle.magnitude, 1f);
            Handles.color = Color.white;      
        #endif
    }
}
