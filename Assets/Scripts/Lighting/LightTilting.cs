using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTilting : MonoBehaviour
{
    public Vector2 IntensityValues = new Vector2(2.4f, 2.8f);
    public Vector2 RangeValues = new Vector2(0.60f, 0.64f);
    public bool isGoingUp = false;
    public float Speed = 2f;
    private float timer = 0f;
    private Light lampLight;

    void Start()
    {
        lampLight = GetComponent<Light>();
    }
    
    void Update()
    {
        timer += Time.deltaTime * Speed;

        if(isGoingUp)
        {
            lampLight.intensity = Mathf.Lerp(IntensityValues.x, IntensityValues.y, timer);
            lampLight.range = Mathf.Lerp(RangeValues.x, RangeValues.y, timer);
        }
        else
        {
            lampLight.intensity = Mathf.Lerp(IntensityValues.y, IntensityValues.x, timer);
            lampLight.range = Mathf.Lerp(RangeValues.y, RangeValues.x, timer);
        }

        if(timer >= 1f)
        {
            timer = 0f;
            isGoingUp = !isGoingUp;
        }
    }
}
