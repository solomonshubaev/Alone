using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Camera))]
public class PlayerCamera : MonoBehaviour
{
    [HideInInspector] private Camera camera;
    [SerializeField] private int screenWidth = 1600;
    [SerializeField] private int screenHeight = 900;


    public void Awake()
    {
        this.camera = this.GetComponent<Camera>();
        this.PerformSizing();
    }
 
    public void Start()
    {
        this.PerformSizing();
    }

    private void PerformSizing()
    {
        // calc based on aspect ratio
        float targetRatio = this.screenWidth / this.screenHeight;

        // determine the game window's current aspect ratio
        float windowaspect = (float)Screen.width / (float)Screen.height;

        // current viewport height should be scaled by this amount
        float scaleheight = windowaspect / targetRatio;

        // if scaled height is less than current height, add letterbox
        if (scaleheight > 1.0f)
        {
            Rect rect = this.camera.rect;

            rect.width = 1.0f;
            rect.height = scaleheight;
            rect.x = 0;
            rect.y = (1.0f - scaleheight) / 2.0f;

            this.camera.rect = rect;
        }
        else // add pillarbox
        {
            float scalewidth = 1.0f / scaleheight;

            Rect rect = this.camera.rect;

            rect.width = scalewidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scalewidth) / 2.0f;
            rect.y = 0;

            this.camera.rect = rect;
        }
    }
}
