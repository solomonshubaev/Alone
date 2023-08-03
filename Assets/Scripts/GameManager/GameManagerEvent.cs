using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class GameManagerEvent : MonoBehaviour
{
    public event Action<GameManagerEvent, GameManagerArgs_UpdateDayTime> updateDayTime;
    public event Action<GameManagerEvent, GameManagerArgs_PassTime> passTime;

    public void UpdateDayTimeEvent(int currentPeriodNumber, int midFullDayPeriodNumber,
        float lightIntensityChangeForPeriod, float maxGeneralLightIntensity)
    {
        updateDayTime?.Invoke(this, new GameManagerArgs_UpdateDayTime()
        {
            currentPeriodNumber = currentPeriodNumber,
            midFullDayPeriodNumber = midFullDayPeriodNumber,
            lightIntensityChangeForPeriod = lightIntensityChangeForPeriod,
            maxGeneralLightIntensity = maxGeneralLightIntensity
        });
    }

    public void PassTimeEvent(int newPeriodNumber, int midFullDayPeriodNumber, float lightIntensityChangeForPeriod, float maxGeneralLightIntensity)
    {
        passTime?.Invoke(this, new GameManagerArgs_PassTime()
        {
            newPeriodNumber = newPeriodNumber,
            midFullDayPeriodNumber = midFullDayPeriodNumber,
            lightIntensityChangeForPeriod = lightIntensityChangeForPeriod,
            maxGeneralLightIntensity = maxGeneralLightIntensity
        });
    }

}

public class GameManagerArgs_UpdateDayTime : EventArgs
{
    public int currentPeriodNumber;
    public int midFullDayPeriodNumber;
    public float lightIntensityChangeForPeriod;
    public float maxGeneralLightIntensity;
}

public class GameManagerArgs_PassTime : EventArgs
{
    public int newPeriodNumber;
    public int midFullDayPeriodNumber;
    public float lightIntensityChangeForPeriod;
    public float maxGeneralLightIntensity;
}