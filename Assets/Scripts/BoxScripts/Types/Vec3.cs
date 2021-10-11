using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BoxScripts
{
    [Serializable]
    public class SerializableVector3
    {
        public float x;
        public float y;
        public float z;
        public Vector3 Vector3
        {
            get
            {
                return new Vector3(x, y, z);
            }
            set
            {
                x = value.x;
                y = value.y;
                z = value.z;
            }
        }

        public SerializableVector3() { x = y = z = 0; }
        public SerializableVector3(Vector3 vector3) : this(vector3.x, vector3.y, vector3.z) {}
        public SerializableVector3 (float _x, float _y, float _z)
        {
            x = _x;
            y = _y;
            z = _z;
        }
    }
}