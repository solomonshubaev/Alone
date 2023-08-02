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
    [SerializeField] private GameManagerEvent gameManagerEvent;


    private void Awake()
    {
        this.generalLight = GetComponent<Light2D>();
        this.generalLightEvent = GetComponent<GeneralLightEvent>();
    }

    private void OnEnable()
    {
        this.gameManagerEvent.updateDayTime += GameManagerEvent_ConfigGeneralLight;
    }

    private void OnDisable()
    {
        this.gameManagerEvent.updateDayTime -= GameManagerEvent_ConfigGeneralLight;
    }
    void Start()
    {
        
    }

    private void GameManagerEvent_ConfigGeneralLight(GameManagerEvent gameManagerEvent,
        GameManagerArgs gameManagerArgs)
    {
        if (gameManagerArgs.currentPeriodNumber <= gameManagerArgs.midFullDayPeriodNumber)
        {
            this.generalLight.intensity -= gameManagerArgs.lightIntensityChangeForPeriod;
        }
        else
        {
            this.generalLight.intensity += gameManagerArgs.lightIntensityChangeForPeriod;
        }
        this.ValidateLightIntensityRealTime(gameManagerArgs.maxGeneralLightIntensity);
        this.generalLightEvent.UpdateLightIntensityEvent(this.generalLight.intensity);
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
