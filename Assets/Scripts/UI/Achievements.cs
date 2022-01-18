using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class Achievements : MonoBehaviour {
    public Image AchievementIcon;
    public Text UpperText;
    public Text LowerText;

    public float Duration = 5f;
    private float _innerDuration = 5f;
    private bool willClose = false;

    IEnumerator SwAchievement()
    {
        GetComponent<Animator>().Rebind();
        GetComponent<Animator>().Update(0f);

        yield return null;
        willClose = false;
        _innerDuration = Duration;

        while(!(willClose && _innerDuration <= 0))
        {
            _innerDuration  -= Time.deltaTime;

            if(!willClose && _innerDuration <= 0)
            {
                willClose = true;
                _innerDuration = 0.5f;
                GetComponent<Animator>().SetTrigger("Out");
            }
            yield return null;
        }
        gameObject.SetActive(false);
    }

    public void ShowAchievement(Sprite img, string uText, string lText)
    {
        AchievementIcon.sprite = img;
        UpperText.text = uText;
        LowerText.text = lText;
        
        gameObject.SetActive(true);
        StartCoroutine(SwAchievement());
    }
}