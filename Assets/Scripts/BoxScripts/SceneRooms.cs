using UnityEngine;
using UnityEngine.SceneManagement;
using System;

namespace BoxScripts
{
    // [CreateAssetMenu(fileName = "SceneRooms", menuName = "BoxScripts/SceneRoom", order = 0)]
    [Serializable]
    public class SceneRoom{
        [ReadOnly]
        public string RoomName;
        
        public bool isActive
        {
            get
            {
                return _isActive;
            }
        }
        [SerializeField]
        private bool _isActive = false;
        [SerializeField]
        private int[] Contiguous;

        public void SetActiveBool(bool active)
        {
            _isActive = active;
        }

        public int[] GetNextRooms()
        {
            return Contiguous;
        }

        public SceneRoom(string RName){
            RoomName = RName;
        }
    }
}