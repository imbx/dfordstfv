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
            DBot.SendLog("BObject", Identifier);
            DBot.SendLog("BObject", " RESULT IS: ");
            Debug.Log(ActionManager.Instance.GetASBool(Identifier));
            if(ActionManager.Instance.GetASBool(Identifier)) return;
            
            if(Reqs != null)
                if(Reqs.Count > 0)
                {
                    foreach(string uid in Reqs)
                    {
                        if(!(ActionManager.Instance.GetASInt(uid) == 5)) return;
                    }
                }

            ActionManager.Instance.SetASBool(Identifier, true);
            ActionManager.Instance.AddAction(Identifier);

            DBot.SendWarning("BObject", " ACTION FOR " + Identifier + " RUNNING.");
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
    [CustomEditor(typeof(BObject))]
    public class BObjectEditor : Editor 
    {
        public BObject scriptTarget;
        private OTypes lastOType;
        public void Awake()
        {
            scriptTarget = (BObject)target;
        }

        public override void OnInspectorGUI()
        {
            base.DrawDefaultInspector();
            
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Update Components"))
            {
                UpdateButton();
            }
            GUILayout.EndHorizontal();
        }

        private void UpdateButton()
        {
            if(lastOType == scriptTarget.ObjectType) return;
                
            if(lastOType != OTypes.Default)
            {
                if(lastOType == OTypes.Drawer)
                {
                    scriptTarget.execute -= Selection.activeGameObject.GetComponent<Drawer>().Execute;
                    Destroy(Selection.activeGameObject.GetComponent<Drawer>());
                }   
            }
            
            if(scriptTarget.ObjectType == OTypes.Drawer)
            {
                Drawer dw = Selection.activeGameObject.AddComponent<Drawer>();
                scriptTarget.execute += dw.Execute;
            }
        }
    }
}