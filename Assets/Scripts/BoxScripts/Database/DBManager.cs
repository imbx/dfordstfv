using System;
using System.Collections.Generic;

using BoxScripts;
using BoxScripts.Localization;

using UnityEngine;

namespace  BoxScripts.DB
{
    public class DBManager : MonoBehaviour
    {
        private Dictionary<string, string, object> DB;
        private Dictionary<string, Dictionary<string, object>> DB2;
        public Dictionary<string, string, object> Contents
        {
            get { return DB; }
            
        }
        public Dictionary<string, string> Lang;
        public string PathToDB = "Database/";

        public void Build()
        {
            DB = new Dictionary<string, string, object>();
            DB2 = new Dictionary<string, Dictionary<string, object>>();
            Lang = new Dictionary<string, string>();

            Lang.Add("ES", "Español");
            Lang.Add("EN", "English");

            foreach(KeyValuePair<string, string> entry in Lang)
            {
                DBot.SendLog("DBManager", "Parsing " + entry.Value + " into DB.");
                DB2.Add(entry.Key, new Dictionary<string, object>());

                ParseData<Interactions>(
                    PathToDB + entry.Key + "/interactions",
                    delegate(Interactions interact){
                        DB.Add(entry.Key, interact.tag, interact.Convert());
                        DB2[entry.Key].Add(interact.tag, interact.Convert());
                });

                ParseData<Dialogue>(
                    PathToDB + entry.Key + "/dialogues",
                    delegate(Dialogue dialogue){
                        DB.Add(entry.Key, dialogue.id.ToString(), dialogue.Convert());
                        DB2[entry.Key].Add(dialogue.id.ToString(), dialogue.Convert());
                });

                DBot.SendLog("DBManager", entry.Value + " parsed into DB.");
            }
            DBot.SendLog("DB", "Test value for DB2: " + (DB2["ES"]["BasicInteraction"] as DBItem<string>).value);
            /*DBot.SendLog("DB" , "Test value : " + (DB["ES", "BasicInteraction"] as DBItem<string>).value);
            
            foreach(KeyValuePair<Tuple<string,string>, object> entry in DB)
            {
                DBot.SendLog("DB", ((DBItem<string>)entry.Value).value + " " + entry.Key);
            }*/
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