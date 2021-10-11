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


    [Serializable]
    public class Dialogue
    {
        public int id;
        public SerializableVector2 anchoredPosition;
        public SerializableVector2 size;
        public string dialogueText;
        public bool isAnchoredAtTop;
        public bool isAnchoredAtCenter;
        public float lifeTime;
    }    

    // https://stackoverflow.com/questions/36239705/serialize-and-deserialize-json-and-json-array-in-unity

    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.Items;
        }

        public static string ToJson<T>(T[] array)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper);
        }

        public static string ToJson<T>(T[] array, bool prettyPrint)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper, prettyPrint);
        }

        [Serializable]
        private class Wrapper<T>
        {
            public T[] Items;
        }
    }

    // https://forum.unity.com/threads/change-gameobject-layer-at-run-time-wont-apply-to-child.10091/
    
     
    public static class IListExtensions {
        /// <summary>
        /// Shuffles the element order of the specified list.
        /// </summary>
        public static void Shuffle<T>(this IList<T> ts) {
            var count = ts.Count;
            var last = count - 1;
            for (var i = 0; i < last; ++i) {
                var r = UnityEngine.Random.Range(i, count);
                var tmp = ts[i];
                ts[i] = ts[r];
                ts[r] = tmp;
            }
        }
    }
}