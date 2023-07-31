using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;

[DisallowMultipleComponent]
public class CollideWithPlayer : MonoBehaviour
{
    [Tooltip("Will the sprite become transparent when collides with Player.")]
    [SerializeField] private bool isTransparent = false;

    [Range(0,255)]
    [Tooltip("When collides with Player what will be sprite's alpha.")]
    [SerializeField] private int spriteAlpha = 150;

    [SerializeField] private SpriteRenderer[] spriteRenderers;

    [SerializeField] private SortingGroup sortingGroup;

    private Transform parentTransform;
    private readonly int infrontOfPlayer = 1;
    private readonly int behindOfPlayer = -1;

    private void Awake()
    {
        this.parentTransform = this.transform.parent;
        HelperValidations.ValidateNotNull(this.parentTransform, nameof(this.parentTransform));
        this.sortingGroup = this.parentTransform.GetComponent<SortingGroup>();
        HelperValidations.ValidateNotNull(this.sortingGroup, nameof(this.sortingGroup));
        this.spriteRenderers = this.parentTransform.GetComponentsInChildren<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.transform.tag;
        if (tag == TagsEnum.Player.ToString())
        {
            Debug.Log("Trigger with Player");
            this.sortingGroup.sortingOrder = this.infrontOfPlayer;
            if (this.isTransparent)
            {
                foreach(SpriteRenderer spriteRenderer in this.spriteRenderers)
                {
                    spriteRenderer.color = new Color(1f, 1f, 1f, this.spriteAlpha / 255f);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        string tag = collision.transform.tag;
        Debug.Log("Exit Trigger with Player");
        if (tag == TagsEnum.Player.ToString())
        {
            Debug.Log("Exit Trigger with Player");
            this.sortingGroup.sortingOrder = this.behindOfPlayer;
            if (this.isTransparent)
            {
                foreach (SpriteRenderer spriteRenderer in this.spriteRenderers)
                {
                    spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
                }
            }
        }
    }

    private void OnValidate()
    {
        if (this.transform.parent == null)
            Debug.LogWarning("This script looking for a parent gameobject and it is missing!");
    }
}
