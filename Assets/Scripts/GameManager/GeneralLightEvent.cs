using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class GeneralLightEvent : MonoBehaviour
{
    public event Action<GeneralLightEvent, GeneralLightArgs> updateDayTime;

    public void UpdateDayTimeEvent(float lightIntensity)
    {
        updateDayTime?.Invoke(this, new GeneralLightArgs()
        {
            lightIntensity = lightIntensity
        }); ;
    }

}

public class GeneralLightArgs : EventArgs
{
    public float lightIntensity;
}