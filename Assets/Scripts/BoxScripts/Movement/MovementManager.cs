using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace BoxScripts
{
    public class MovementManager : MonoBehaviour {
        public static MovementManager Instance;
        private Dictionary<string, MovGroup> storage;

        private void Awake()
        {
            Instance = this;
            storage = new Dictionary<string, MovGroup>();
        }

        public string CreateMov(Transform tr, DataPackage data, float speed = 1f, bool rotate = false, bool scalate = false)
        {
            string Identifier = Guid.NewGuid().ToString();
            MovGroup group = new MovGroup(
                tr,
                new DataPackage(tr),
                data,
                new Mov(speed, rotate, scalate)
            );
            storage.Add(Identifier, group);
            StartCoroutine(group.mov.Execute(group.t, group.data1, group.data2));
            return Identifier;
        }

        public bool CheckExecution(string id)
        {
            if(storage.ContainsKey(id))
            {
                return storage[id].mov.isExecuting;
            }
            return false;
        }

        public void Flip(string id)
        {
            if(!storage.ContainsKey(id)) return;
            MovGroup group = storage[id];
            StartCoroutine(group.mov.Execute(group.t, group.data2, group.data1));
        }

        public void Remove(string id)
        {
            if(!storage.ContainsKey(id)) return;

            storage.Remove(id);
        }
    }
}
