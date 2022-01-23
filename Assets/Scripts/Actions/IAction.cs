namespace BoxScripts
{
    interface IAction
    {
        void Load();
        void Execute();
        void CustomUpdate();
        void Remove();        
    }
}
