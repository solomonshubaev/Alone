using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManagerAbstract : MonoBehaviour
{
    protected bool canAccessLight = true;

    protected virtual void Start()
    {
        this.canAccessLight = true;
    }

    public void SetCanAccessLightComponent(bool canAccess)
    {
        this.canAccessLight = canAccess;
    }
}
