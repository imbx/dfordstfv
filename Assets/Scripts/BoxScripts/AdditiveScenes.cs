using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;

namespace  BoxScripts
{
    public class AdditiveScenes : MonoBehaviour {
    
        public List<SceneRoom> Rooms;

        public void LoadRoom(int id)
        {
            try
            {
                if(Rooms.Count < id && Rooms.Count > 0)
                SceneManager.LoadSceneAsync(Rooms[id].RoomName, LoadSceneMode.Additive);
            } catch(Exception e)
            {
                DBot.SendError("AdditiveScenes", e.ToString());
            }

        }

    }
}
