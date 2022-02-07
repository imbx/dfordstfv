using BoxScripts;
using UnityEngine;
using UnityEditor;

public class Letter : ActionBase
{
    public string LocaleIdentifier;
    public override void Load()
    {
        base.Load();

        // UPDATE LOCALE INTO SPRITES
    }

    public override bool Execute()
    {
        // EXECUTE MOV, INTERACTION AFTER
        
        return base.Execute();
    }
    public override void CustomUpdate()
    {

        // If Mov finished = GameController.instance.gco.lookItem.SetItem(transform);
        base.CustomUpdate();
    }

    public override void Remove()
    {
        base.Remove();
        
        // CALLED FROM INTERACTION, MOVE FLIP IF DO NOT OBTAINED, REMOVED IF DO
    }
    void OnDrawGizmos(){
        #if UNITY_EDITOR
        Handles.color = Utils.GetDarkBlueGUI;
        // Handles.DrawLine(transform.position, transform.position + Dir);
        Handles.color = Color.white;
        #endif
    }
}