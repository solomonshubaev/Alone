using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class GameManagerEvent : MonoBehaviour
{
    public event Action<GameManagerEvent, GameManagerArgs> updateDayTime;

    public void UpdateDayTimeEvent(int currentPeriodNumber, int midFullDayPeriodNumber,
        float lightIntensityChangeForPeriod, float maxGeneralLightIntensity)
    {
        updateDayTime?.Invoke(this, new GameManagerArgs()
        {
            currentPeriodNumber = currentPeriodNumber,
            midFullDayPeriodNumber = midFullDayPeriodNumber,
            lightIntensityChangeForPeriod = lightIntensityChangeForPeriod,
            maxGeneralLightIntensity = maxGeneralLightIntensity
        });
    }

}

public class GameManagerArgs : EventArgs
{
    public int currentPeriodNumber;
    public int midFullDayPeriodNumber;
    public float lightIntensityChangeForPeriod;
    public float maxGeneralLightIntensity;
}