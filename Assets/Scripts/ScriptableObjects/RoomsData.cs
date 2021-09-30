using UnityEngine;
using BoxScripts;
using System.Collections.Generic;
using System;

[CreateAssetMenu(fileName = "RoomsData", menuName = "BoxScripts/RoomsData", order = 0)]
[Serializable]
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