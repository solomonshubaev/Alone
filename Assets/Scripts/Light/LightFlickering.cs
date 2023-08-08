using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

[DisallowMultipleComponent]
[RequireComponent(typeof(Light2D))]
public class LightFlickering : MonoBehaviour
{
    [SerializeField] private LightFlickeringSO lightFlickeringSO;
    private LightManagerAbstract lightManagerAbstract;
    private Light2D lightToFlicker;
    private float lastTimeFlickered; // last time flicker process started
    private float timeToNextFlickerProcess; // time to next flicker process

    private float timeBetweenIntensityChange; // time between light intensity change

    private float originalIntensity;

    private bool isFlickering = false;


    private void Awake()
    {
        this.lightToFlicker = GetComponent<Light2D>();
    }

    private void Start()
    {
        this.lightManagerAbstract = GetComponent<LightManagerAbstract>();
        this.originalIntensity = this.lightToFlicker.intensity;
        this.lastTimeFlickered = Time.time;
        this.timeToNextFlickerProcess = this.GetNextTimeToFlickerProcess();
        this.timeBetweenIntensityChange = this.GetNextMinIntensity();
        this.isFlickering = false;
    }

    private void Update()
    {
        if (this.lightToFlicker.enabled)
        {
            if (Time.time > this.lastTimeFlickered + this.timeToNextFlickerProcess && !isFlickering)
            {
                this.isFlickering = true;
                this.FlickerInRowAsync();
            }
        }
        else
        {
            this.lastTimeFlickered = Time.time;
        }
    }

    private async void FlickerInRowAsync()
    {
        this.lightManagerAbstract.SetCanAccessLightComponent(false);
        for (int i = 0; i < this.lightFlickeringSO.numberOfFlickersInRow; i++)
        {
            await this.MakeLightFlickerAsync();
            await Task.Delay(TimeSpan.FromSeconds(this.GetNextTimeMaxIntensity()));
        }
        this.lastTimeFlickered = Time.time;
        this.isFlickering = false;
        this.timeToNextFlickerProcess = this.GetNextTimeToFlickerProcess();
        this.lightManagerAbstract.SetCanAccessLightComponent(true);
    }

    private async Task MakeLightFlickerAsync()
    {
        // maybe add animation - no instance change - need to test
        this.originalIntensity = this.lightToFlicker.intensity;
        this.lightToFlicker.intensity = this.lightFlickeringSO.minLightIntensity;
        this.timeBetweenIntensityChange = this.GetNextMinIntensity();
        await Task.Delay(TimeSpan.FromSeconds(this.timeBetweenIntensityChange));
        this.lightToFlicker.intensity = this.originalIntensity;
    }

    private float GetNextTimeToFlickerProcess()
    {
        return Random.Range(this.lightFlickeringSO.minTimeBetweenFlickersProcess,
            this.lightFlickeringSO.maxTimeBetweenFlickersProcess);
    }

    private float GetNextMinIntensity()
    {
        return Random.Range(this.lightFlickeringSO.minTimeMinIntensity,
            this.lightFlickeringSO.maxTimeMinIntensity);
    }

    private float GetNextTimeMaxIntensity()
    {
        return Random.Range(this.lightFlickeringSO.minTimeMaximumIntensity,
            this.lightFlickeringSO.maxTimeMaximumIntensity);
    }

    private void OnValidate()
    {
        LightManagerAbstract lightManagerAbstract = GetComponent<LightManagerAbstract>();
        HelperValidations.ValidateNotNull(this.lightFlickeringSO, nameof(this.lightFlickeringSO));
        HelperValidations.ValidateNotNull(lightManagerAbstract, nameof(lightManagerAbstract), 
            "Please add a light manager object which manage light component");
    }
}
