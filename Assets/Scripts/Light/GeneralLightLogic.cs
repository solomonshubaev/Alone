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
    private GameManagerEvent gameManagerEvent;


    private void Awake()
    {
        this.generalLight = GetComponent<Light2D>();
        this.generalLightEvent = GetComponent<GeneralLightEvent>();
        this.gameManagerEvent = this.transform.parent.GetComponent<GameManagerEvent>();
    }

    private void OnEnable()
    {
        this.gameManagerEvent.updateDayTime += GameManagerEvent_ConfigGeneralLight;
    }

    private void OnDisable()
    {
        this.gameManagerEvent.updateDayTime -= GameManagerEvent_ConfigGeneralLight;
    }

    private void GameManagerEvent_ConfigGeneralLight(GameManagerEvent gameManagerEvent,
        GameManagerArgs_UpdateDayTime gameManagerArgs)
    {

        this.generalLight.intensity = this.CalculateNewLightIntensity(gameManagerArgs.currentPeriodNumber,
            gameManagerArgs.lightIntensityChangeForPeriod, gameManagerArgs.midFullDayPeriodNumber);
        this.ValidateLightIntensityRealTime(gameManagerArgs.maxGeneralLightIntensity);
        this.generalLightEvent.UpdateLightIntensityEvent(this.generalLight.intensity);
    }

    private float CalculateNewLightIntensity(int period, float lightIntensityChangeForPeriod,
        int midFullDayPeriodNumber)
    {
        if (period <= midFullDayPeriodNumber)
        {
            // if maximum light intensity is 1 !!!
            return 1 - ((period - 1) * lightIntensityChangeForPeriod);
        }
        else
        {
            // if maximum light intensity is 1 !!!
            return (period * lightIntensityChangeForPeriod) - 1;
        }
    }

    private void ValidateLightIntensityRealTime(float maxGeneralLightIntensity)
    {
        if (this.generalLight.intensity < 0f)
        {
            Debug.LogWarning("General light intensity is negative (less than 0)!" +
                " Setting light to 0");
            this.generalLight.intensity = 0f;
        }

        if (this.generalLight.intensity > maxGeneralLightIntensity)
        {
            Debug.LogWarning("General light intensity is bigger than maxGeneralLightIntensity. " +
                "Setting light to maxGeneralLightIntensity");
            this.generalLight.intensity = maxGeneralLightIntensity;
        }
    }

    private void OnValidate()
    {

    }
}
