namespace BoxScripts
{
    interface IAction
    {
        void Load();
        bool Execute();
        void CustomUpdate();
        void Remove();        
    }
}
