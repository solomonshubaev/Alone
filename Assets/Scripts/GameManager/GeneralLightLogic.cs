using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Rendering.Universal;


[DisallowMultipleComponent]
[RequireComponent(typeof(Light2D))]
[RequireComponent(typeof(GeneralLightEvent))]
public class GeneralLightLogic : MonoBehaviour
{
    private Light2D generalLight;
    private GeneralLightEvent generalLightEvent;

    private float lastTimeADayStarted;
    private float timeForATimePeriod;
    private int currentPeriodNumber;
    private int midFullDayPeriodNumber;

    private readonly float maxGeneralLightIntensity = 1f;
    private readonly float aFullDayLength = 0.12f * 60;
    private readonly int aFullDayDifferentPeriods = 80;
    private readonly float lightIntensityChangeForPeriod = 0.025f;

    private void Awake()
    {
        this.generalLightEvent = GetComponent<GeneralLightEvent>();
        this.generalLight = GetComponent<Light2D>();
    }

    void Start()
    {
        this.midFullDayPeriodNumber = (int)this.aFullDayDifferentPeriods / 2;
        this.timeForATimePeriod = this.aFullDayLength / aFullDayDifferentPeriods;
        this.lastTimeADayStarted = Time.time;
        this.currentPeriodNumber = 1;
    }

    void Update()
    {
        if(Time.time >= this.lastTimeADayStarted + (this.currentPeriodNumber * this.timeForATimePeriod))
        {
            this.currentPeriodNumber++;
            Debug.Log("Changing period time to: " + this.currentPeriodNumber);
            if(this.currentPeriodNumber <= this.midFullDayPeriodNumber)
            {
                this.generalLight.intensity -= this.lightIntensityChangeForPeriod;
            }
            else
            {
                this.generalLight.intensity += this.lightIntensityChangeForPeriod;
            }
            this.generalLightEvent.UpdateDayTimeEvent(this.generalLight.intensity);
            this.ValidateLightIntensityRealTime(this.generalLight.intensity);
            if(this.currentPeriodNumber == this.aFullDayDifferentPeriods)
            {
                this.SetNewDay();
            }
        }
    }

    private void SetNewDay()
    {
        this.currentPeriodNumber = 1;
        this.lastTimeADayStarted = Time.time;
    }

    private void ValidateLightIntensityRealTime(float generalLightIntensity)
    {
        if (this.generalLight.intensity < 0f)
        {
            Debug.LogWarning("General light intensity is negative (less than 0)!" +
                " Setting light to 0");
            this.generalLight.intensity = 0f;
        }

        if (this.generalLight.intensity > this.maxGeneralLightIntensity)
        {
            Debug.LogWarning("General light intensity is bigger than maxGeneralLightIntensity. " +
                "Setting light to maxGeneralLightIntensity");
            this.generalLight.intensity = this.maxGeneralLightIntensity;
        }
    }

    private void OnValidate()
    {
        Light2D generalLight = GetComponent<Light2D>();
        if (generalLight.intensity > this.maxGeneralLightIntensity)
        {
            Debug.LogWarning(string.Format("Light intensity is {0} higher then max playerLightIntensity."
                , generalLight.intensity));
        }

        if(this.aFullDayDifferentPeriods % 2 != 0)
        {
            Debug.LogWarning("aFullDayDifferentPeriods should be an even because we divide it by 2");
        }
    }
}
