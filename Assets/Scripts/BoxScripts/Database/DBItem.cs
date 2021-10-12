using UnityEngine;
using System;

namespace BoxScripts.DB
{
    public class DBItem
    {
        public Material material;
        public string text;
        public Sprite sprite;

        public DBItemSettings settings;

        public DBItem (string txt) => text = txt;
        public DBItem (Material mat) => material = mat;
        public DBItem (Sprite spr) => sprite = spr;

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