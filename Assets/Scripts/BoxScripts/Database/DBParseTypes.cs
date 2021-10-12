using System;

namespace BoxScripts.DB
{
    [Serializable]
    public class Dialogue
    {
        public int id;
        public SerializableVector2 anchoredPosition;
        public SerializableVector2 size;
        public string dialogueText;
        public bool isAnchoredAtTop;
        public bool isAnchoredAtCenter;
        public float lifeTime;

        public DBItem Convert()
        {
            DBItem item = new DBItem(dialogueText);
            item.Settings(
                size,
                anchoredPosition,
                isAnchoredAtTop,
                isAnchoredAtCenter,
                lifeTime);
            return item;
        }
    }

    [Serializable]
    public class Interactions
    {
        public string tag;
        public string interactionText;

        public DBItem Convert()
        {
            return new DBItem(interactionText);
        }
    }
}