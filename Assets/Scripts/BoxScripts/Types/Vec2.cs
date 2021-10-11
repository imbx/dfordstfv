using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BoxScripts
{
    [Serializable]
    public class SerializableVector2
    {
        public float x;
        public float y;
        public Vector2 Vector2
        {
            get
            {
                return new Vector2(x, y);
            }
            set
            {
                x = value.x;
                y = value.y;
            }
        }

        public SerializableVector2() { x = y = 0; }
        public SerializableVector2(Vector2 vector2) : this(vector2.x, vector2.y) {}
        public SerializableVector2 (float _x, float _y)
        {
            x = _x;
            y = _y;
        }
    }
}