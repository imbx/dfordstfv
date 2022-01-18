using System;
using System.Collections.Generic;
using UnityEngine;

namespace BoxScripts
{
    [CreateAssetMenu(fileName = "ActionDB", menuName = "BoxScripts/ActionDB", order = 0)]
    public class ActionDB : ScriptableObject {
        public List<ActionState> Data;
        public bool Add(string id)
        {
            foreach(ActionState a in Data)
                if(a.Identifier == id)
                    return false;

            Data.Add(new ActionState(id));
            return true;
        }
        
    }
}
