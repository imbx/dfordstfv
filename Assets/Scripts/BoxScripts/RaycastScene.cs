using System;
using System.Collections.Generic;
using UnityEngine;
using BoxScripts;

public class RaycastScene : MonoBehaviour {
    public LayerMask TargetLayer;

    private void Awake() {
        TargetLayer = LayerMask.GetMask("Floor");
    }
    private void FixedUpdate() {
        List<int> objects = new List<int>();

        bool result = RaycastExtensions.Raycast(transform.position, -transform.up, TargetLayer,
            delegate (RaycastHit hit) {
                int tempRoom = Int32.Parse(hit.transform.name);
                if(!objects.Contains(tempRoom)) objects.Add(tempRoom);
            }
            );
        
        if(result)
        {
            string DebugStringTotalScenes = "";
            foreach( var x in objects) DebugStringTotalScenes += x.ToString() + " ";
            // DBot.SendLog("RaycastScene", "Scenes ID under player: " + DebugStringTotalScenes); 

            GameController.instance.additiveScenes.UpdateRooms(objects.ToArray());
        }

        objects.Clear(); 
    }
}