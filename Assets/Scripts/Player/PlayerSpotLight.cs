using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[DisallowMultipleComponent]
[RequireComponent(typeof(Light2D))]
public class PlayerSpotLight : LightManagerAbstract
{
    private Light2D playerLight;
    [SerializeField] private GeneralLightEvent generalLightEvent;

    private readonly float maxPlayerLightIntensity = 0.8f;
    private readonly float lightActivateThreshold = 0.4f;

    private void Awake()
    {
        this.playerLight = GetComponent<Light2D>();
    }

    protected override void Start()
    {
        base.Start();
    }

    private void OnEnable()
    {
        this.generalLightEvent.updateLightIntensity += GeneralLightEvent_ConfigPlayerLight;
    }

    private void OnDisable()
    {
        this.generalLightEvent.updateLightIntensity -= GeneralLightEvent_ConfigPlayerLight;
    }

    private void GeneralLightEvent_ConfigPlayerLight(GeneralLightEvent generalLightEvent,
        GeneralLightArgs generalLightArgs)
    {
        if(generalLightArgs.lightIntensity <= this.lightActivateThreshold)
        {
            this.playerLight.enabled = true;
            if (this.canAccessLight)
            {
                this.playerLight.intensity = this.CalculatePlayerLightIntensity(generalLightArgs.lightIntensity);
                Debug.Log("Light component is not locked");
            }
            else
                Debug.Log("Light component is locked");
        }
        else
        {
            this.playerLight.enabled = false;
        }
    }

    private float CalculatePlayerLightIntensity(float generalLightIntensity)
    {
        float newPlayerLightIntensity = this.maxPlayerLightIntensity - generalLightIntensity;
        if (newPlayerLightIntensity < 0)
        {
            Debug.LogWarning("newPlayerLightIntensity is negative value, check this please");
        }
        return Mathf.Clamp(newPlayerLightIntensity, 0, this.maxPlayerLightIntensity);
    }

    private void OnValidate()
    {
        this.playerLight = GetComponent<Light2D>();
        HelperValidations.ValidateNotNull(this.playerLight, nameof(this.playerLight));
        if (this.playerLight.intensity > this.maxPlayerLightIntensity)
        {
            Debug.LogWarning(string.Format("Light intensity is {0} higher then max playerLightIntensity."
                , this.playerLight.intensity));
        }
    }
}
