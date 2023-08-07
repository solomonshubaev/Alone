using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class InformationUiEvent : MonoBehaviour
{
    public event Action<InformationUiEvent, InformationUiArgs> onInformation;

    public void CallUpdatePlayer_InformationUiEvent(float vitalityPercent, float staminaPercent)
    {
        onInformation?.Invoke(this, new InformationUiArgs() { vitalityPercent = vitalityPercent, staminaPercent = staminaPercent });
    }

}

public class InformationUiArgs : EventArgs
{
    public float vitalityPercent;
    public float staminaPercent;
}