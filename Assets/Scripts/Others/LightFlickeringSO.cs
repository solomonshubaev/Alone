using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LightFlickering_", menuName = "Scriptable Objects/Light/Light Flickering")]
public class LightFlickeringSO : ScriptableObject
{
    [Range(0, 1)] public float minLightIntensity = 0f;

    [Tooltip("[Optional] - Some scripts can take the original light intensity as max.")]
    [Range(0, 1)] public float maxLightIntensity = 1f;
    public int numberOfFlickersInRow = 1;

    public float minTimeBetweenFlickersProcess = 0.01f;
    public float maxTimeBetweenFlickersProcess = 10f;

    public float minTimeMinIntensity = 0.01f;
    public float maxTimeMinIntensity = 1f;

    public float minTimeMaximumIntensity = 0.01f;
    public float maxTimeMaximumIntensity = 1f;



    private void OnValidate()
    {
        if (this.minLightIntensity > this.maxLightIntensity)
            this.minLightIntensity = maxLightIntensity;

        if (this.minTimeBetweenFlickersProcess > this.maxTimeBetweenFlickersProcess)
        {
            Debug.LogWarning("minTimeBetweenFlickers is bigger than maxTimeBetweenFlickers." +
                " Setting minTimeBetweenFlickers to maxTimeBetweenFlickers.");
            this.minLightIntensity = this.maxLightIntensity;
        }

        if (this.minTimeMinIntensity > this.maxTimeMinIntensity)
        {
            Debug.LogWarning("minTimeBetweenIntensityChange is bigger than maxTimeBetweenIntensityChange." +
                " Setting minTimeBetweenIntensityChange to maxTimeBetweenIntensityChange.");
            this.minTimeMinIntensity = this.maxTimeMinIntensity;
        }

        if (this.minTimeMaximumIntensity > this.maxTimeMaximumIntensity)
        {
            Debug.LogWarning("minTimeBetweenIntensityChange is bigger than maxTimeBetweenIntensityChange." +
                " Setting minTimeBetweenIntensityChange to maxTimeBetweenIntensityChange.");
            this.minTimeMaximumIntensity = this.maxTimeMaximumIntensity;
        }

        if(this.numberOfFlickersInRow < 1)
        {
            Debug.LogWarning("numberOfFLickers in a row should be at least 1." +
                " Setting to 1.");
            this.numberOfFlickersInRow = 1;
        }
    }
}
