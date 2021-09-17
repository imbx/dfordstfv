
using System;

namespace BoxScripts
{
    [Serializable]
    public class CItem : BObject
    {
        public string[] Reqs;

        public bool CheckReqs()
        {
            if(Reqs != null){
                if ( Reqs.Length > 0)
                {
                    foreach(string _id in Reqs)
                        if(!ActionManager.Instance.GetASBool(_id)) return false;
                    ActionManager.Instance.SetASBool(Identifier, true);
                    return true;
                }
            }
            return false;
        }
    }
}