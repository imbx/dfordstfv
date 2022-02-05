
using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

namespace BoxScripts
{
    [Serializable]
    public class CItem
    {
        private BObject Obj;
        [ReadOnly]
        public string Identifier  = "";
        [Tooltip("Paste the identifiers to be compared")]
        public string[] Reqs;

        public CItem(BObject  bobj)
        {
            Obj = bobj;
            Identifier = Obj.Identifier;
            Reqs = new string[2];
        }

        public bool CheckReqs()
        {
            if(Reqs != null){
                if ( Reqs.Length > 0)
                {
                    foreach(string _id in Reqs)
                        if(!ActionManager.Instance.IsBusy(_id)) return false;
                    ActionManager.Instance.SetBusy(Obj.Identifier, true);
                    return true;
                }
            }
            return false;
        }
    }
}