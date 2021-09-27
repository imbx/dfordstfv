using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BoxScripts
{
    public class ActionManager : MonoBehaviour {

        public static ActionManager Instance;
        public List<Guid> CurrentActions;

        [SerializeField]
        public List<ActionState> actionState;

        private void Awake() {
            Instance = this;
        }

        private void Reset() {
            foreach(BObject bobj in Resources.FindObjectsOfTypeAll(typeof(BObject)) as BObject[])
            {
                if(bobj.Identifier == "") bobj.GenerateGUID();
                actionState.Add ( new ActionState(bobj.Identifier) );
            }
        }

        public void AddAction(Guid guid)
        {
            if(!CurrentActions.Contains(guid))
                CurrentActions.Add(guid);
        }

        public void RemAction(Guid guid)
        {
            if(CurrentActions.Contains(guid))
                CurrentActions.Remove(guid);
        }

        public bool isActionInUse(Guid guid)
        {
            return CurrentActions.Contains(guid);
        }

        public ActionState SearchActionState(string guid)
        {
            foreach(ActionState aState in actionState)
            {
                if(aState.Identifier == guid) return aState;
            }
            return default;
        }

        public bool GetASBool(Guid guid)
        {
            ActionState action = SearchActionState(guid.ToString());
            return (action != default) ? action.isActive : false;
        }

        public bool GetASBool(string guid)
        {
            ActionState action = SearchActionState(guid);
            return (action != default) ? action.isActive : false;
        }

        public void SetASBool(Guid guid, bool active = false)
        {
            ActionState action = SearchActionState(guid.ToString());
            if(action != default) action.isActive = active;
        }

        public void SetASBool(string guid, bool active = false)
        {
            ActionState action = SearchActionState(guid);
            if(action != default) action.isActive = active;
        }

        public void GetASInt(Guid guid, int _state = -1)
        {
            ActionState action = SearchActionState(guid.ToString());
            if(action != default) action.state = _state;
        }

        public void GetASInt(string guid, int _state = -1)
        {
            ActionState action = SearchActionState(guid);
            if(action != default) action.state = _state;
        }

        public void SetASInt(Guid guid, int _state = -1)
        {
            ActionState action = SearchActionState(guid.ToString());
            if(action != default) action.state = _state;
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
            if(scriptTarget)
            {
                scriptTarget.actionState = new List<ActionState>();
                foreach(BObject bobj in Resources.FindObjectsOfTypeAll(typeof(BObject)) as BObject[])
                {
                    if(bobj.Identifier == "") bobj.GenerateGUID();
                    scriptTarget.actionState.Add ( new ActionState(bobj.Identifier) );
                }
            }
        }
    }
}
