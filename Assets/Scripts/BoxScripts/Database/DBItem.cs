using UnityEngine;
using System;

namespace BoxScripts.DB
{
    public class DBItem<T>
    {
        public T value;
        public DBItemSettings settings;

        public DBItem (T val) => value = val;

        public void Settings(
            SerializableVector2 size,
            SerializableVector2 anchoredPos,
            bool isTop = false,
            bool isCentered = false,
            float lifetime = 4f)
        {
            settings.size = size;
            settings.anchoredPosition = anchoredPos;
            settings.isAtTop = isTop;
            settings.isCentered = isCentered;
            settings.lifeTime = lifetime;
        }

    }
}