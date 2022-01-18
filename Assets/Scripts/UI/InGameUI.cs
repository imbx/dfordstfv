using UnityEngine;
using BoxScripts;
public class InGameUI : MonoBehaviour {
    public static InGameUI instance;

    [SerializeField]
    private Achievements achievements;
    [SerializeField]
    private Archive archive;

    public bool checker = false;
    public Sprite test;

    private void Awake() {
        instance = this;
    }
    private void Update() {
        if(checker)
        {
            checker = false;
            SpawnAchievement(AchievementType.Key);
        }
    }

    public void SpawnAchievement(AchievementType achievementType)
    {
        achievements.ShowAchievement(test,  "", "");
    }

}