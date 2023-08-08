using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class UiManagerEvent : MonoBehaviour
{
    public event Action<UiManagerEvent, InformationUiArgs> onInformation;

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