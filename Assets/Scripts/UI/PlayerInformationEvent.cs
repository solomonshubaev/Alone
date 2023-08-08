using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class PlayerInformationEvent : MonoBehaviour
{
    public event Action<PlayerInformationEvent, PlayerInformationArgs> updatePlayerInformation;

    public void UpdatePlayerInformation(float vitalityPercent, float staminaPercent)
    {
        updatePlayerInformation?.Invoke(this, new PlayerInformationArgs() { vitalityPercent = vitalityPercent, staminaPercent = staminaPercent });
    }

}

public class PlayerInformationArgs : EventArgs
{
    public float vitalityPercent;
    public float staminaPercent;
}