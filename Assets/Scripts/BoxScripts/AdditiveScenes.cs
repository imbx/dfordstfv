using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using System.IO;

namespace  BoxScripts
{
    public class AdditiveScenes : MonoBehaviour {
        [SerializeField]
        public RoomData Rooms;
        public LayerMask scLayer;
        [HideInInspector]
        public int[] currentRoomId;

        public void LoadRoom(int id)
        {
            DBot.SendError("AdditiveScenes", "Loading room: " + Rooms.Data[id].RoomName);
            try
            {
                if(Rooms.Data.Count < id && Rooms.Data.Count > 0)
                SceneManager.LoadSceneAsync(Rooms.Data[id].RoomName, LoadSceneMode.Additive);
                Rooms.Data[id].SetActiveBool(true);
            } catch(Exception e)
            {
                DBot.SendError("AdditiveScenes", e.ToString());
            }

        }

        public void UnloadRoom(int id)
        {
            DBot.SendError("AdditiveScenes", "Unloading room: " + Rooms.Data[id].RoomName);
            try
            {
                if(Rooms.Data.Count < id && Rooms.Data.Count > 0)
                SceneManager.UnloadSceneAsync(Rooms.Data[id].RoomName);
                Rooms.Data[id].SetActiveBool(false);
            } catch(Exception e)
            {
                DBot.SendError("AdditiveScenes", e.ToString());
            }

        }
        public void UpdateRooms(int[] currentRooms)
        {
            if(Rooms == null || Rooms.Data.Count <= 0) 
            {
                DBot.SendError("AdditiveScenes", "No Scene Rooms loaded");
                return;
            }
            if(RoomsChecker(currentRooms) || currentRoomId.Equals(currentRooms)) return;

            currentRoomId = currentRooms;
            int[] currentActiveIds = {};

            foreach(int _i in currentRooms)
            {
                currentActiveIds = currentActiveIds.Union(Rooms.Data[_i].GetNextRooms()).ToArray();
            }

            for(int i = 0; i < Rooms.Data.Count; i++)
            {
                if(currentActiveIds.Contains(i) || currentRooms.Contains(i))
                {
                    if(!Rooms.Data[i].isActive) LoadRoom(i);
                }
                else
                {
                    if(Rooms.Data[i].isActive) UnloadRoom(i);
                }
            }
            DBot.SendLog("AdditiveScenes", "Finished updating scenes.");
        }
        private bool RoomIdChecker(int id)
        {
            if(id >= Rooms.Data.Count) return false;
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
                LoadRoomsFromFolder(pathToRooms);
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

        public void LoadRoomsFromFolder(string rootPath)
        {
            scriptTarget.Rooms = Utils.CreateRoomDataAsset();
            // scriptTarget.Rooms.Data.Clear();
            DBot.SendLog("AdditiveScenes", "Getting this path: " + Application.dataPath + rootPath);
            string rootDir = Application.dataPath + rootPath;
            Utils.LoadRooms(rootDir, delegate(string dir)
            {
                Scene openedScene = EditorSceneManager.OpenScene(dir, OpenSceneMode.Additive);
                string RoomName = Path.GetFileNameWithoutExtension(dir);

                if(!scriptTarget.Rooms.CheckIfRoomExists(RoomName))
                    scriptTarget.Rooms.Data.Add(new SceneRoom(RoomName));

                EditorSceneManager.MarkSceneDirty(openedScene);
                EditorSceneManager.SaveOpenScenes();
            });
        }
    }

}
