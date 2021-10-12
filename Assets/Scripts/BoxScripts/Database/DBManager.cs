using System;
using System.Collections.Generic;

using BoxScripts;
using BoxScripts.Localization;

using UnityEngine;

namespace  BoxScripts.DB
{
    public class DBManager : MonoBehaviour
    {
        private Dictionary<MultiKeyArr<string, string>, DBItem> DB;
        public Dictionary<string, string> Lang;
        public string PathToDB = "Database/";

        public void Build()
        {
            Lang.Add("ES", "Español");
            Lang.Add("EN", "English");


            foreach(KeyValuePair<string, string> entry in Lang)
            {
                ParseData<Interactions>(
                    "Database/"+ entry.Key + "/interactions",
                    delegate(Interactions interact){
                        DB.Add(MultiKeyArr.Create<string, string>(entry.Key, interact.tag), interact.Convert());
                });

                ParseData<Dialogue>(
                    "Database/"+ entry.Key + "/dialogues",
                    delegate(Dialogue dialogue){
                        DB.Add(MultiKeyArr.Create<string, string>(entry.Key, dialogue.id.ToString()), dialogue.Convert());
                });
                
            }
        }

        public void ParseInteractions()
        {

        }


        public T[] ParseData<T>(string fileName, Action<T> action)
        {
            var textAsset = Resources.Load<TextAsset>(fileName);

            T[] d = JsonHelper.FromJson<T>(textAsset.text);

            foreach(T tempObj in d)
            {
                action(tempObj);

                DBot.SendLog("Database", "Data extracted added " + tempObj);
            }

            return d;
        }


    }
}