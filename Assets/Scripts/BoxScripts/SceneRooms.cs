using UnityEngine;
using UnityEngine.SceneManagement;
using System;

namespace BoxScripts
{
    //[CreateAssetMenu(fileName = "SceneRooms", menuName = "/ScriptableObjects/SceneRoom", order = 0)]
    [Serializable]
    public class SceneRoom {
        [SerializeField]
        public string RoomName;
        [SerializeField]
        public bool isActive
        {
            get
            {
                return _isActive;
            }
        }
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
    }
}