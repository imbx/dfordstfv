using UnityEngine;
using UnityEditor;
using UnityEngine.Events;

namespace BoxScripts.Localization 
{
    public class TxtLocalization : MonoBehaviour {
        public ScriptableObject Table;
        public string Identifier;

        public UnityEvent<string> TargetProperty;

        private void OnEnable() 
        {
            TargetProperty.Invoke(Identifier);
        }
    }
}
