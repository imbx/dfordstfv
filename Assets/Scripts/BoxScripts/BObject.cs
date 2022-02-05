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

        private ActionState _action;


        private void Awake() {
            try
            {
                _action = ActionManager.Instance.SearchActionState(Identifier);
                _action.load += Load;
            }
            catch (Exception e)
            {
                DBot.SendError("BObject - " + transform.name, " Something went wrong searching for ActionState");
                DBot.SendError("BObject - " + transform.name, e.ToString());
            }
        }

        private void Reset() {
            if(Identifier == null || Identifier == "") {
                Identifier = ActionManager.Instance.AssignNewGUID(Identifier);
                DBot.SendWarning("BObject - " + transform.name, " Changed its GUID");
                
            } // GenerateGUID();
        }

        public void Load()
        {
            if(ActionManager.Instance.GetASInt(Identifier) == 5) gameObject.SetActive(false);
            if(ActionManager.Instance.GetASInt(Identifier) == 1) _action?.execute.Invoke();
            if(ActionManager.Instance.IsBusy(Identifier)) ActionManager.Instance.SetBusy(Identifier, false);
        }

        public void CustomUpdate()
        {

        }


        public void Execute()
        {
            DBot.SendLog("BObject", Identifier + " is active?  " + ActionManager.Instance.IsBusy(Identifier));
            if(ActionManager.Instance.IsBusy(Identifier)) return;
            
            if(Reqs != null)
                if(Reqs.Count > 0)
                {
                    foreach(string uid in Reqs)
                    {
                        if(!(ActionManager.Instance.GetASInt(uid) == 5)) return;
                    }
                }

            ActionManager.Instance.AddAction(Identifier);

            DBot.SendWarning("BObject", " ACTION FOR " + Identifier + " RUNNING.");
            bool res =_action.execute?.Invoke() ?? false;

            if(res) Remove();
        }

        public void Remove()
        {
            ActionManager.Instance.RemAction(Identifier);
            _action.remove?.Invoke();

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
                    // scriptTarget.execute -= Selection.activeGameObject.GetComponent<Drawer>().Execute;
                    Destroy(Selection.activeGameObject.GetComponent<Drawer>());
                }
                else if(lastOType == OTypes.Door)
                {
                    // scriptTarget.execute -= Selection.activeGameObject.GetComponent<Drawer>().Execute;
                    Destroy(Selection.activeGameObject.GetComponent<Door>());
                }   
            }

            DBot.SendLog("BObject", " Adding " + scriptTarget.ObjectType.ToString());
            
            if(scriptTarget.ObjectType == OTypes.Drawer) Selection.activeGameObject.AddComponent<Drawer>();
            else if(scriptTarget.ObjectType == OTypes.Door) Selection.activeGameObject.AddComponent<Door>();
        }
    }
}