using System;

namespace BoxScripts{
    [Serializable]
    public class ActionState
    {
        [ReadOnly]
        public string Identifier;
        public bool isActive;
        public int state = 0;


        public ActionState(string id, bool active = false, int st = 0)
        {
            Identifier = id;
            isActive = active;
            st = state;
        }
    }
}
