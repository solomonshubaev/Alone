using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class NightFruitBush : FruitBushLogic
{
    protected override void Awake()
    {
        base.Awake();
        this.SetFruitsActive(false);
    }

    protected override void Update()
    {
        if (GameManager.Instance.IsDark())
        {
            base.Update();
        }
        else
        {
            if(this.hasFruits)
                this.SetFruitsActive(false);
        }
    }
}
