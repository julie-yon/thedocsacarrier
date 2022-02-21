using System.Collections;
using Docsa;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightND : NightDaySwitchEvent
{
    public bool nightLight = true; 
    public float lightOnIntensity;
    public float lightOffIntensity;
    //curve x,y는 0이상 1이하 값으로 정의되어야만 합니다.
    public AnimationCurve curve;
    public float duration;
           
    private Light2D _lt;
    // Start is called before the first frame update
    void Start()
    {
        _lt = GetComponent<Light2D>();
    }

    // Update is called once per frame
    public override void ChangeState(bool isNight)
    {
        if (nightLight)
        {
            if (isNight)
            {
                StartCoroutine(FadeFromTo(lightOffIntensity, lightOnIntensity));
            }
            else
            {
                StartCoroutine(FadeFromTo(lightOnIntensity, lightOffIntensity));
            }
        }
        else
        {
            if (isNight)
            {
                StartCoroutine(FadeFromTo(lightOnIntensity, lightOffIntensity));
            }
            else
            {
                StartCoroutine(FadeFromTo(lightOffIntensity, lightOnIntensity));
            }
        }
    }

    private IEnumerator FadeFromTo(float startSize, float targetSize)
    {
        float timePassed = 0f;
        float sizeDiff = targetSize - startSize;
        while (timePassed < duration)
        {
            timePassed += Time.deltaTime;
            _lt.intensity = startSize + curve.Evaluate(timePassed / duration) * sizeDiff;
            yield return null;
        }
    
        _lt.intensity = targetSize;
    }
}