using UnityEngine;
using BoxScripts;
using System.Collections.Generic;
using System;

[CreateAssetMenu(fileName = "RoomData", menuName = "BoxScripts/RoomData", order = 0)]
public class RoomData : ScriptableObject {
    public List<SceneRoom> Data;

    public bool CheckIfRoomExists(string TargetName)
    {
        foreach(var x in Data)
        {
            if(x.RoomName == TargetName) return true;
        }
        return false;
    }
}