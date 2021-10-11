using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder : MonoBehaviour {
    public List<Light> thunderLights;

    public Vector2 SpawnInterval = new Vector2(3f, 5f);
    public bool ExecuteThunder = false;

    [Header("Thunder Parameters")]
    [SerializeField] private float MaxIntensity = 8f;
    [SerializeField] private float LowestMaxIntensity = 6f;
    [SerializeField] private float TimeToMaxIntensity = 0.25f;
    [SerializeField] private float TimeToHalf = 0.1f;
    [SerializeField] private float HoldAtMaxTimer = 0.5f;
    [SerializeField] private float SpeedToRemove = 1f;

    [Header("FMOD Sound")]

    // [FMODUnity.EventRef]
    public string thunderSound = "event:/Ambiente/trueno";


    private float SpawnTimer = -1f;
    

    private bool isAnimating = false;

    private void Update() {

        if(SpawnTimer <= 0 && SpawnTimer != -1)
        {
            SpawnTimer = Random.Range(SpawnInterval.x, SpawnInterval.y) * 60f;
            Debug.Log("[Thunder] Time for next Thunder : " + SpawnTimer);
            StartCoroutine(AnimThunder());
        }

        if(!isAnimating && SpawnTimer != -1)
        {
            SpawnTimer -= Time.deltaTime;
        }
    }

    IEnumerator AnimThunder()
    {
        if(thunderLights.Count > 0)
        {
            Light x = thunderLights[Random.Range(0, thunderLights.Count)];
            isAnimating = true;
            float timer = 0f;
            while(timer < TimeToMaxIntensity)
            {
                timer += Time.deltaTime;
                x.intensity = Mathf.Lerp(0, MaxIntensity, timer / TimeToMaxIntensity);
                yield return null;
            }
            // GameController.current.music.playMusic(thunderSound);

            timer = 0f;
            while(timer < TimeToHalf)
            {
                timer += Time.deltaTime;
                x.intensity = Mathf.Lerp(MaxIntensity, LowestMaxIntensity, timer / TimeToHalf);
                yield return null;
            }

            timer = 0f;
            while(timer < TimeToHalf)
            {
                timer += Time.deltaTime;

                x.intensity = Mathf.Lerp(LowestMaxIntensity, MaxIntensity, timer / TimeToHalf);
                yield return null;
            }

            yield return new WaitForSeconds(HoldAtMaxTimer);

            timer = 0f;
            while(timer < 1f)
            {
                timer += Time.deltaTime * SpeedToRemove;
                x.intensity = Mathf.Lerp(x.intensity, 0, timer);
                yield return null;
            }
            
            isAnimating = false;
        }
        yield return null;
    }

    public void TriggerFirstThunder()
    {
        SpawnTimer = 0;
    }
}