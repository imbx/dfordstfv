using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System;
using System.Collections.Generic;

namespace BoxScripts
{
    public class Comparer : MonoBehaviour {

        [SerializeField]
        public List<CItem> comparerList;
    
    }

    [CustomEditor(typeof(Comparer))]
    public class ComparerEditor : Editor 
    {
        public Comparer scriptTarget;
        public void Awake()
        {
            scriptTarget = (Comparer)target;
        }

        public override void OnInspectorGUI()
        {
            base.DrawDefaultInspector();
            
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Add Compare"))
            {
                AddComp();
            }
            GUILayout.EndHorizontal();
        }

        private void AddComp() {
            if(scriptTarget)
            {
                BObject bobj = Selection.activeGameObject.AddComponent<BObject>();
                bobj.ObjectType = OTypes.Compare;
                InternalEditorUtility.SetIsInspectorExpanded(bobj, false);
                scriptTarget.comparerList.Add (new CItem(bobj)) ;
            }
        }
    }
}
