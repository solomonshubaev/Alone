using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[DisallowMultipleComponent]
[RequireComponent(typeof(SortingGroup))]
public class FruitBushLogic : InteractableAbstract
{
    [SerializeField] private FruitBushSO fruitBushSO;
    [SerializeField] protected bool hasFruits;
    private SortingGroup sortingGroup;
    private GameObject[] fruitGameObjects;
    private float lastTimeHadFruits;

    protected virtual void Awake()
    {
        this.InitFruitArray();
        this.sortingGroup = GetComponent<SortingGroup>();
    }

    void Start()
    {
        if (!this.hasFruits)
        {
            this.lastTimeHadFruits = Time.time;
        }
        this.sortingGroup.sortingOrder = HelperUtilities.GetSortingOrderByPosition(this.transform.position);
    }

    protected virtual void Update()
    {
        this.GrowFruits();
    }

    public override void InteractWithPlayer()
    {
        if (this.hasFruits)
        {
            this.lastTimeHadFruits = Time.time;
            this.SetFruitsActive(false);
            Player.Instance.IncreaseVitalityBy((float)this.fruitBushSO.vitalityGainPerFruit * this.fruitGameObjects.Length);
        }
    }

    protected void GrowFruits()
    {
        if (!this.hasFruits && Time.time >= this.lastTimeHadFruits + this.fruitBushSO.fruitGrowTime)
        {
            this.SetFruitsActive(true);
        }
    }

    private void InitFruitArray()
    {
        List<GameObject> fruitGameObjects = new List<GameObject>();
        foreach (Transform child in this.transform)
        {
            if (child.tag == TagsEnum.Fruit.ToString())
            {
                fruitGameObjects.Add(child.gameObject);
            }
        }
        int numberOfFruits = Random.Range(1, fruitGameObjects.Count);
        this.fruitGameObjects = new GameObject[numberOfFruits];
        for (int i = 0; i < this.fruitGameObjects.Length; i++)
        {
            fruitGameObjects[i].SetActive(true); // activate at beginning
            this.fruitGameObjects[i] = fruitGameObjects[i];
        }
    }

    protected void SetFruitsActive(bool active)
    {
        foreach (GameObject fruit in this.fruitGameObjects)
        {
            fruit.SetActive(active);
        }
        this.hasFruits = active;
    }

    private void OnValidate()
    {
        if (this.transform.tag != TagsEnum.Interactable.ToString())
        {
            Debug.LogWarning(string.Format("GameObject should be with tag: '{0}'",
                TagsEnum.Interactable.ToString()));
        }
    }
}
