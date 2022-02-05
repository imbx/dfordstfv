using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using BoxScripts;

public class Drawer : ActionBase
{
    public float Speed = 2f;
    public Vector3 Dir;
    [SerializeField]
    [ReadOnly]
    private string MovId;

    public override bool Execute()
    {
        DBot.SendError("Drawer", " Trying to execute.");
        if (MovId == null || MovId == "")
        {
            DBot.SendError("Drawer", " Trying to create a movement.");
            MovId = MovementManager.Instance.CreateMov(transform, new DataPackage(transform.position + Dir), Speed);
        }
        else if(!MovementManager.Instance.CheckExecution(MovId)) MovementManager.Instance.Flip(MovId);
        else DBot.SendError("Drawer", " Trying to execute, but is already being executed.");
        
        return true;
    }

    void OnDrawGizmos(){
        #if UNITY_EDITOR
        Handles.color = Utils.GetDarkBlueGUI;
        Handles.DrawLine(transform.position, transform.position + Dir);
        Handles.color = Color.white;
        #endif
    }
}
