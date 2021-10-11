using BoxScripts;
using  BoxScripts.Localization;
using UnityEngine;

namespace  BoxScripts.DB
{
    public class DBManager : MonoBheaviour
    {
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