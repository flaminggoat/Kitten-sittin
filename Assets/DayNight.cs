using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayNight : MonoBehaviour
{
    
    [Range(-0.5f, 1.5f)]
    [Tooltip("0 to 1 is day, outside that is night")]
    public float dayCycle = 0;
    public float dayDurationSeconds = 10; 
    public Light2D[] nightLights;
    private float[] _nightLightsMaxIntensities;
    private Light2D sunLight;


    [Tooltip("0 to 1 is day, outside that is night")]
    public AnimationCurve dayIntensity;
    public Gradient dayLightColor;

    // Start is called before the first frame update
    void Start()
    {
        sunLight = GetComponent<Light2D>();

        Array.Resize(ref _nightLightsMaxIntensities, nightLights.Length);

        for (int i = 0 ; i < nightLights.Length ; i++) {
            _nightLightsMaxIntensities[i] = nightLights[i].intensity;
        }
    }

    // Update is called once per frame
    void Update()
    {
        dayCycle += Time.deltaTime / dayDurationSeconds;
        if (dayCycle > 1.5) {
            dayCycle -= 2;
        }

        ConfigureLights(dayCycle);
    }

    void ConfigureLights(float dayCycle) {
        sunLight.intensity = dayIntensity.Evaluate(dayCycle);
        var clamped = Mathf.Clamp(sunLight.intensity, 0, 1); 
        sunLight.color = dayLightColor.Evaluate(clamped);

        for (int i = 0 ; i < nightLights.Length ; i++) {
            var light = nightLights[i];
            var maxIntensity = _nightLightsMaxIntensities[i];

            light.intensity = (1-clamped) * maxIntensity;
        }
    }
}
