using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

namespace BoxScripts
{
    public class BObject : MonoBehaviour, IBaseObject
    {
        [SerializeField]
        [ReadOnly]
        public string Identifier;
        // [ReadOnly]
        //public string IdentifierStr;
        public OTypes ObjectType;
        public bool Obtainable;
        public List<string> Reqs;

        public delegate void ExtLoad();
        public delegate void ExtExecute();
        public ExtLoad load;
        public ExtExecute execute;

        private void Reset() {
            if(Identifier == null || Identifier == "") {
                Identifier = ActionManager.Instance.AssignNewGUID(Identifier);
                DBot.SendWarning("BObject - " + transform.name, " Changed its GUID");

                try
                {
                    ActionState action = ActionManager.Instance.SearchActionState(Identifier);
                    action.load += Load;
                    action.execute += Execute;
                }
                catch (Exception e)
                {
                    DBot.SendError("BObject - " + transform.name, " Something went wrong searching for ActionState");
                    DBot.SendError("BObject - " + transform.name, e.ToString());
                }
                
            } // GenerateGUID();
        }

        public void Load()
        {
            if(ActionManager.Instance.GetASInt(Identifier) == 5) gameObject.SetActive(false);
            load?.Invoke();
        }

        public void CustomUpdate()
        {

        }


        public void Execute()
        {
            if(ActionManager.Instance.GetASBool(Identifier)) return;
            if(Reqs == null) return;
            if(Reqs.Count > 0)
            {
                foreach(string uid in Reqs)
                {
                    if(!(ActionManager.Instance.GetASInt(uid) == 5)) return;
                }
            }

            ActionManager.Instance.SetASBool(Identifier, true);
            ActionManager.Instance.AddAction(Identifier);
            execute?.Invoke();
        }

        public void Remove()
        {
            ActionManager.Instance.RemAction(Identifier);
            ActionManager.Instance.SetASBool(Identifier, false);

            if(Obtainable)
            {
                ActionManager.Instance.SetASInt(Identifier,  5); // 5 AS COMPARER TRUE;
            }
        }  
    }
}