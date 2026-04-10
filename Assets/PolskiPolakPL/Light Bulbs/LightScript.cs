using System;
using UnityEngine;
public class LightScript : MonoBehaviour
{
    [field: SerializeField] public bool isON { get; private set; }
    [SerializeField] Renderer lightbulbMatRenderer;
    [SerializeField] Light lightSource;
    float baseIntensity;
    Material ONMaterial;
    Material OFFMaterial;

    public event Action OnLightsON;
    public event Action OnLightsOFF;

    private void Awake()
    {
        baseIntensity = lightSource.intensity;

        if (!lightbulbMatRenderer)
        {
            Debug.LogWarning($"Did not attached <b>Material Renderer</b> to {this.name}!");
            return;
        }

        OFFMaterial = lightbulbMatRenderer.material;
        ONMaterial = CreateMaterial(OFFMaterial);
        if (isON)
            TurnON();
        else TurnOFF();
    }

    public void TurnON()
    {
        lightbulbMatRenderer.material = ONMaterial;
        lightSource.intensity = baseIntensity;
        lightSource.enabled = true;
        isON = true;
        OnLightsON?.Invoke();
    }

    public void TurnOFF()
    {
        lightbulbMatRenderer.material = OFFMaterial;
        lightSource.enabled = false;
        isON = false;
        OnLightsOFF?.Invoke();
    }

    public void Toggle(bool state)
    {
        if (state)
            TurnON();
        else TurnOFF();
    }

    public void Flicker()
    {
        if (!isON)
            return;
        lightSource.intensity = UnityEngine.Random.Range(0, baseIntensity);
        if (lightSource.intensity < baseIntensity / 2)
            lightbulbMatRenderer.material = OFFMaterial;
        else
            lightbulbMatRenderer.material = ONMaterial;
    }

    Material CreateMaterial(Material referenceMat)
    {
        Material newMat;
        newMat = new Material(referenceMat);
        newMat.color = lightSource.color;
        newMat.EnableKeyword("_EMISSION");
        newMat.SetColor("_EmissionColor", lightSource.color);

        return newMat;
    }
}
