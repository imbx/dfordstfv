using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BoxScripts;

namespace BoxScripts
{
    
    [RequireComponent(typeof(BObject))]
    public class ActionBase : MonoBehaviour, IAction
    {

        private void Awake() {
            ActionState actionState  = ActionManager.Instance.SearchActionState(GetComponent<BObject>().Identifier);
            actionState.execute += Execute;
            actionState.customUpdate += CustomUpdate;
            actionState.load += Load;
            actionState.remove += Remove;
            actionState.load?.Invoke();
            DBot.SendError("ActionBase", "Action Base Loaded");
        }

        private void OnEnable()
        {
        }

        public virtual void Load()
        {
        }
        
        public virtual bool Execute()
        {
            return false;
        }

        public virtual void CustomUpdate()
        {
        }

        public virtual void Remove()
        {
        }
    }

}