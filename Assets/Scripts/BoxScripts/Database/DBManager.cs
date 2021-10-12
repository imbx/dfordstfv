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
                DB.Add(("ES", "ASD"), new DBItem("asd"));
            }
        }

        public void ParseInteractions()
        {

        }


        public T[] ParseData<T>(string fileName)
        {
            var textAsset = Resources.Load<TextAsset>(fileName);

            T[] d = JsonHelper.FromJson<T>(textAsset.text);

            foreach(T tempObj in d)
            {
                DBot.SendLog("Database", "Data extracted added " + tempObj);
            }

            return d;
        }


    }
}