using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class FruitBushLogic : MonoBehaviour
{
    [SerializeField] private FruitBushSO fruitBushSO;
    [SerializeField] private bool hasFruits;
    [SerializeField] private GameObject fruitGameObject;

    private float lastTimeHadFruits;

    void Start()
    {
        if (!this.hasFruits)
        {
            this.lastTimeHadFruits = Time.time;
        }
    }

    void Update()
    {
        if (!this.hasFruits && Time.time >= this.lastTimeHadFruits + this.fruitBushSO.fruitGrowTime)
        {
            this.hasFruits = true;
            this.fruitGameObject.SetActive(true);
        }
    }

    private void OnValidate()
    {
        HelperValidations.ValidateNotNull(this.fruitGameObject, nameof(this.fruitGameObject));
        if (this.transform.tag != TagsEnum.FruitBush.ToString())
        {
            Debug.LogWarning(string.Format("GameObject should be with tag: '{0}'",
                TagsEnum.FruitBush.ToString()));
        }
    }
}
