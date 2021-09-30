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
        
        RaycastHit[] hits;

        hits = Physics.RaycastAll(transform.position, -transform.up, 20f,TargetLayer);

        if(hits.Length > 0)
        {
            List<int> objects = new List<int>();

            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit hit = hits[i];
                int tempRoom = Int32.Parse(hit.transform.name);

                if(!objects.Contains(tempRoom)) objects.Add(tempRoom);
            }

            string DebugStringTotalScenes = "";
            foreach( var x in objects) {
                DebugStringTotalScenes += x.ToString() + " ";
            }

            DBot.SendLog("RaycastScene", "Scenes ID under player: " + DebugStringTotalScenes);

            

            GameController.instance.additiveScenes.UpdateRooms(objects.ToArray());
        }
        
    }
}