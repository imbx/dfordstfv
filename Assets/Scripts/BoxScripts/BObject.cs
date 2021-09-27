using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

namespace BoxScripts
{
    public class BObject : MonoBehaviour, IBaseObject
    {
        [SerializeField]
        [ReadOnly]
        public string Identifier;
        // [ReadOnly]
        //public string IdentifierStr;
        public OTypes ObjectType;
        public bool isUnique;
        public List<Guid> GuidsToListen;

        private void Reset() {
            DBot.SendWarning("BObject - " + transform.name, " Changed its GUID");
            GenerateGUID();
        }

        public void Load()
        {
            
        }

        public void CustomUpdate()
        {

        }


        public void Execute()
        {

        }

        public void Remove()
        {
            
        }


        public void GenerateGUID(string idstr = null)
        {
            Identifier = Guid.NewGuid().ToString();
            // IdentifierStr = Identifier.ToString();
        }

    }
}