using UnityEngine;
using BoxScripts.DB;

namespace  BoxScripts
{
    public class GameController : MonoBehaviour {
        public static GameController instance;
        public AdditiveScenes additiveScenes;
        public DBManager Database;

        private void Awake() {
            instance = this;
        }

        private void OnEnable() {
            Database.Build();
        }


    }
}