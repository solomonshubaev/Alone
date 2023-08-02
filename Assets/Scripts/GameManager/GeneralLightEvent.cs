using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class GeneralLightEvent : MonoBehaviour
{
    public event Action<GeneralLightEvent, GeneralLightArgs> updateLightIntensity;

    public void UpdateLightIntensityEvent(float lightIntensity)
    {
        updateLightIntensity?.Invoke(this, new GeneralLightArgs()
        {
            lightIntensity = lightIntensity
        });
    }

}

public class GeneralLightArgs : EventArgs
{
    public float lightIntensity;
}