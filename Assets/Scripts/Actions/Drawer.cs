using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BoxScripts;

[RequireComponent(typeof(BObject))]
public class Drawer : MonoBehaviour, IAction
{
    public float Speed = 2f;
    public Vector3 Dir;
    [SerializeField]
    [ReadOnly]
    private string MovId;

    private void Awake() {
        ActionState actionState  = ActionManager.Instance.SearchActionState(GetComponent<BObject>().Identifier);
        actionState.execute += Execute;
        actionState.load += Load;
    }

    public void Load()
    {

    }
    public void Execute()
    {
        DBot.SendError("Drawer", " Trying to execute.");
        if (MovId == null)
        {
            MovId = MovementManager.Instance.CreateMov(transform, new DataPackage(transform.position + Dir), Speed);
        }
        else if(!MovementManager.Instance.CheckExecution(MovId)) MovementManager.Instance.Flip(MovId);
        else DBot.SendError("Drawer", " Trying to execute, but is already being executed.");
    }

    public void CustomUpdate()
    {

    }

    public void Remove()
    {

    }

    void OnDrawGizmos(){
        #if UNITY_EDITOR
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(
                transform.position,
                transform.position + Dir
            );
            Gizmos.color = Color.white;
        #endif
    }
}
