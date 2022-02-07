using UnityEngine;
using BoxScripts.DB;

namespace  BoxScripts
{
    public class GameController : MonoBehaviour {
        public static GameController instance;
        public AdditiveScenes additiveScenes;
        public GameControllerObject gco; 
        public DBManager Database;

        private void Awake() {
            instance = this;
            Cursor.lockState = CursorLockMode.Locked;
            //  isVisible ? : CursorLockMode.Confined;
        }

        private void OnEnable() {
            Database.Build();
        }


    }
}