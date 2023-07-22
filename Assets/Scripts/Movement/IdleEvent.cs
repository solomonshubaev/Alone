using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class IdleEvent : MonoBehaviour
{
    public event Action<IdleEvent> onIdle;

    public void CallIdleEvent()
    {
        onIdle?.Invoke(this);
    }
}
