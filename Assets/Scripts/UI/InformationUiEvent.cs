using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class InformationUiEvent : MonoBehaviour
{
    public event Action<InformationUiEvent, InformationUiArgs> onInformation;

    public void CallUpdatePlayer_InformationUiEvent(float hungerPercent, float staminaPercent)
    {
        onInformation?.Invoke(this, new InformationUiArgs() { hungerPercent = hungerPercent, staminaPercent = staminaPercent });
    }

}

public class InformationUiArgs : EventArgs
{
    public float hungerPercent;
    public float staminaPercent;
}