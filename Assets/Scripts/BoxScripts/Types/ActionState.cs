using System;

namespace BoxScripts{
    [Serializable]
    public class ActionState
    {
        [ReadOnly]
        public string Identifier;
        public bool isBusy;
        public int state = 0;
        public delegate void Load();
        public delegate bool Execute();
        public delegate void CustomUpdate();
        public delegate void Remove();
        public Load load;
        public Execute execute;
        public CustomUpdate customUpdate;
        public Remove remove;

        public ActionState(string id, bool active = false, int st = 0)
        {
            Identifier = id;
            isBusy = active;
            st = state;
        }
    }
}
