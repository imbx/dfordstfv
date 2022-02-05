using BoxScripts;
using UnityEngine;
using UnityEditor;

public class Letter : ActionBase
{
    public string LocaleIdentifier;
    public override void Load()
    {
        base.Load();
    }

    public override bool Execute()
    {
        return base.Execute();
    }

    public override void Remove()
    {
        base.Remove();
    }
    void OnDrawGizmos(){
        #if UNITY_EDITOR
        Handles.color = Utils.GetDarkBlueGUI;
        // Handles.DrawLine(transform.position, transform.position + Dir);
        Handles.color = Color.white;
        #endif
    }
}