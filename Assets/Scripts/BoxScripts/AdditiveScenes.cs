using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using System;
using System.Linq;
using System.Collections.Generic;

namespace  BoxScripts
{
    public class AdditiveScenes : MonoBehaviour {

        public List<SceneRoom> Rooms;
        public LayerMask scLayer;
        [HideInInspector]
        public int[] currentRoomId;

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

        public void UnloadRoom(int id)
        {
            try
            {
                if(Rooms.Count < id && Rooms.Count > 0)
                SceneManager.UnloadSceneAsync(Rooms[id].RoomName);
            } catch(Exception e)
            {
                DBot.SendError("AdditiveScenes", e.ToString());
            }

        }
        public void UpdateRooms(int[] currentRooms)
        {
            if(Rooms == null) return;
            if(RoomsChecker(currentRooms) || currentRoomId.Equals(currentRooms)) return;

            currentRoomId = currentRooms;
            int[] currentActiveIds = {};

            foreach(int _i in currentRooms)
            {
                currentActiveIds = currentActiveIds.Union(Rooms[_i].GetNextRooms()).ToArray();
            }

            for(int i = 0; i < Rooms.Count; i++)
            {
                if(currentActiveIds.Contains(i) || currentRooms.Contains(i))
                {
                    if(!Rooms[i].isActive) LoadRoom(i);
                }
                else
                {
                    if(Rooms[i].isActive) UnloadRoom(i);
                }
            }
            DBot.SendLog("AdditiveScenes", "Finished updating scenes.");
        }
        private bool RoomIdChecker(int id)
        {
            if(id >= Rooms.Count) return false;
            return true;
        }

        private bool RoomsChecker(int[] ids)
        {
            foreach(int _id in ids) 
                if(!RoomIdChecker(_id))
                    return false;
            return true;
        }
    }

    [CustomEditor(typeof(AdditiveScenes))]
    public class AdditiveScenesEditor : Editor
    {
        private string pathToRooms = "/Scenes/Rooms/";
        public AdditiveScenes scriptTarget;
        public void Awake()
        {
            scriptTarget = (AdditiveScenes)target;
        }
        public override void OnInspectorGUI()
        {
            EditorGUILayout.TextField("Room Scenes Path: ", pathToRooms);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Load Rooms"))
            {
                LoadRoomsFromFolder();
            }
            GUILayout.EndHorizontal();

            EditorGUILayout.Space();
            EditorGUILayout.Space();

            base.DrawDefaultInspector();

            EditorGUILayout.Space();
            EditorGUILayout.Space();
            
            string currentRoomsStr = "";

            foreach(int i in  scriptTarget.currentRoomId)
                currentRoomsStr += i + " ";

            if(scriptTarget.currentRoomId.Length <= 0) currentRoomsStr  = "None";

            EditorGUILayout.HelpBox("\n  Current Active Rooms :\n  " + currentRoomsStr + "\n", MessageType.Info); 
        }

        public void LoadRoomsFromFolder()
        {
            
        }
    }

}
