using UnityEngine;

namespace  BoxScripts
{
    public class GameController : MonoBehaviour {
        public static GameController instance;
        public AdditiveScenes additiveScenes;

        private void Awake() {
            instance = this;
        }


    }
}