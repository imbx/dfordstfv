using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BoxScripts
{
    public class ActionManager : MonoBehaviour {

        public static ActionManager Instance;
        public List<string> CurrentActions;

        [SerializeField]
        public ActionDB actions;

        //[SerializeField]
        //public List<ActionState> actionState;

        private void Awake() {
            Instance = this;
        }

        private void Reset() {
            if (!actions)
            {
                DBot.SendError("ActionManager", "No action ScriptableObject was added.");
                return;
            }

            actions.Data.Clear();

            foreach(BObject bobj in Resources.FindObjectsOfTypeAll(typeof(BObject)) as BObject[])
            {
                string cId = bobj.Identifier;
                if(cId == "" || cId == null) bobj.Identifier = AssignNewGUID(null);
                else if (SearchActionState(cId) == null)
                {
                    actions.Add(cId);
                }
            }
        }

        private string GenerateGUID(string idstr = null)
        {
            return Guid.NewGuid().ToString();
        }

        public string AssignNewGUID(string oldId)
        {
            if(oldId != "")
            {
                DBot.SendWarning("ActionManager", "Removing ID: " + oldId);
                RemoveActionState(oldId);
            }

            string nGUID = GenerateGUID();
            actions.Add(nGUID);
            return nGUID;
        }

        public bool RemoveActionState(string Id)
        {
            if(Id == null) return false;
            actions.Data.Remove(SearchActionState(Id));
            return true;
        }

        public void AddAction(string guid)
        {
            if(!CurrentActions.Contains(guid))
                CurrentActions.Add(guid);
        }

        public void RemAction(string guid)
        {
            if(CurrentActions.Contains(guid))
                CurrentActions.Remove(guid);
        }

        public bool isActionInUse(string guid)
        {
            return CurrentActions.Contains(guid);
        }

        public ActionState SearchActionState(string guid)
        {
            foreach(ActionState aState in actions.Data)
            {
                if(aState.Identifier == guid) return aState;
            }
            return default;
        }

        public bool GetASBool(string guid)
        {
            ActionState action = SearchActionState(guid);
            return (action != default) ? action.isActive : false;
        }

        public void SetASBool(string guid, bool active = false)
        {
            ActionState action = SearchActionState(guid);
            if(action != default) action.isActive = active;
        }

        public int GetASInt(string guid)
        {
            ActionState action = SearchActionState(guid);
            return (action != default) ? action.state : -1;
        }

        public void SetASInt(string guid, int _state = -1)
        {
            ActionState action = SearchActionState(guid.ToString());
            if(action != default) action.state = _state;
        }

        public void Rst()
        {
            Reset();
        }
    }


    [CustomEditor(typeof(ActionManager))]
    public class ActionManagerEditor : Editor 
    {
        public ActionManager scriptTarget;
         public void Awake()
        {
            scriptTarget = (ActionManager)target;
        }

        public override void OnInspectorGUI()
        {
            base.DrawDefaultInspector();
            
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Load Interactions"))
            {
                Reset();
            }
            GUILayout.EndHorizontal();
        }

        private void Reset() {
            if(scriptTarget) scriptTarget.Rst();
        }
    }
}
