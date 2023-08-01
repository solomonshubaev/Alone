using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class FruitBushLogic : InteractableAbstract
{
    [SerializeField] private FruitBushSO fruitBushSO;
    [SerializeField] private bool hasFruits;
    [SerializeField] private GameObject fruitGameObject;
    private float lastTimeHadFruits;

    private void Awake()
    {
        
    }

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

    public override void InteractWithPlayer()
    {
        if (this.hasFruits)
        {
            this.hasFruits = false;
            this.lastTimeHadFruits = Time.time;
            this.fruitGameObject.SetActive(false);
            Player.Instance.IncreaseHungerBy((float)this.fruitBushSO.hungerGain);
        }
    }

    private void OnValidate()
    {
        HelperValidations.ValidateNotNull(this.fruitGameObject, nameof(this.fruitGameObject));
        if (this.transform.tag != TagsEnum.Interactable.ToString())
        {
            Debug.LogWarning(string.Format("GameObject should be with tag: '{0}'",
                TagsEnum.Interactable.ToString()));
        }
    }
}
