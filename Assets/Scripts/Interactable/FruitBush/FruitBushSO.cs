using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FruitBush_", menuName = "Scriptable Objects/Plants/Fruit Bush")]
public class FruitBushSO : ScriptableObject
{
    public int vitalityGain = 0;

    public float fruitGrowTime = 10f;

    private void OnValidate()
    {
        if (this.fruitGrowTime < 0f)
        {
            Debug.LogWarning("fruitGrowTime should be positive");
        }
    }
}
