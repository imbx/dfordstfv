using System;
using System.Collections.Generic;

using BoxScripts;
using BoxScripts.Localization;

using UnityEngine;

namespace  BoxScripts.DB
{
    public class DBManager : MonoBehaviour
    {
        private Dictionary<MultiKeyArr<string, string>, object> DB;
        public Dictionary<string, string> Lang;
        public string PathToDB = "Database/";

        public void Build()
        {
            Lang.Add("ES", "Español");
            Lang.Add("EN", "English");

            foreach(KeyValuePair<string, string> entry in Lang)
            {
                DBot.SendLog("DBManager", "Parsing " + entry.Value + " into DB.");
                
                ParseData<Interactions>(
                    PathToDB + entry.Key + "/interactions",
                    delegate(Interactions interact){
                        DB.Add(MultiKeyArr.Create<string, string>(entry.Key, interact.tag), interact.Convert());
                });

                ParseData<Dialogue>(
                    PathToDB + entry.Key + "/dialogues",
                    delegate(Dialogue dialogue){
                        DB.Add(MultiKeyArr.Create<string, string>(entry.Key, dialogue.id.ToString()), dialogue.Convert());
                });

                DBot.SendLog("DBManager", entry.Value + " parsed into DB.");
            }
        }

        public void ParseData<T>(string fileName, Action<T> action)
        {
            try
            {
                var textAsset = Resources.Load<TextAsset>(fileName);
                T[] d = JsonHelper.FromJson<T>(textAsset.text);

                foreach(T tempObj in d)
                {
                    action(tempObj);
                    DBot.SendLog("DBManager", "Data extracted added " + tempObj);
                }
            }
            catch (Exception e)
            {
                DBot.SendError("DBManager", e.ToString());
            }
        }
    }
}