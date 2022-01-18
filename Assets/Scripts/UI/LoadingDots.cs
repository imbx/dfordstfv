using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingDots : MonoBehaviour
{

    public Text text;
    public float speedToWrite = 0.3f;
    public float timeToWait = 2f;

    void OnEnable()
    {
        StartCoroutine(AnimDots());
    }
    private void OnDisable() {
        StopCoroutine(AnimDots());
    }

    IEnumerator AnimDots()
    {
        while(true)
        {
            if(text.text.Length == 3)
            {
                yield return new WaitForSeconds(timeToWait);
                text.text = "";
            }
            yield return new WaitForSeconds(speedToWrite);
            text.text += ".";

            yield return null;
        }
    }
}
