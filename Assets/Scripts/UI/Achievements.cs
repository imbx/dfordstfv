using UnityEngine;
using UnityEngine.UI;

public class Achievements : MonoBehaviour {
    public Image AchievementIcon;
    public Text UpperText;
    public Text LowerText;

    public float Duration = 5f;
    private bool willClose = false;


    private void OnEnable() {
        willClose = false;
        GetComponent<Animator>().Rebind();
        GetComponent<Animator>().Update(0f);
    }

    private void Update() {
        Duration  -= Time.deltaTime;

        if(!willClose && Duration <= 0)
        {
            willClose = true;
            Duration = 1f;
            GetComponent<Animator>().SetTrigger("Out");
        }
        else if(Duration <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void ShowAchievementn()
    {
        // CHANGE DATA

        gameObject.SetActive(true);
    }
}