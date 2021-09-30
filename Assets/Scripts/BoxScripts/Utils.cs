using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace BoxScripts
{
    public class Utils
    {
        public static void LoadRooms(string roomsPath, Action<string> action)
        {
            List<string> rooms = new List<string>();
            try {
                EditorUtility.DisplayProgressBar("BoxScripts - Load Rooms",
                    "Collecting scenes. Please wait.", 0);
            
                string[] files = Directory.GetFiles(roomsPath);
                for (int i = 0; i < files.Length; ++i) {
                    if (files[i].EndsWith(".unity")) {
                        rooms.Add(files[i]);
                    }
                }
                int count = rooms.Count;
                for (int i = 0; i < count; ++i) {
                    string scenePath = rooms[i];
                    
                    EditorUtility.DisplayProgressBar("BoxScripts - Load Rooms",
                        $"Processing {scenePath}", ((i + 1) / (float)count));
                    
                    // Invoke the action
                    action(scenePath);
                    
                    Debug.Log($"Processed {scenePath}");
                }
            } finally {
                EditorUtility.ClearProgressBar();
            }
        }

        public static RoomData CreateRoomDataAsset()
        {
            string assetPath = "Assets/SOb/RoomData.asset";
            bool assetExists = AssetDatabase.GetMainAssetTypeAtPath( assetPath ) != null;
            RoomData asset;
            if(!assetExists)
            {
                DBot.SendLog("Utils", "Creating scene room object.");
                asset = ScriptableObject.CreateInstance<RoomData>();
                asset.Data = new List<SceneRoom>();
                AssetDatabase.CreateAsset(asset, assetPath);
                AssetDatabase.SaveAssets();
            }
            else
            {
                 DBot.SendLog("Utils", "Found scene room object.");
                asset = (RoomData) AssetDatabase.LoadAssetAtPath(assetPath, typeof(RoomData));
            }

            // EditorUtility.FocusProjectWindow();

            // Selection.activeObject = asset;

            return asset;
        }
    }
}