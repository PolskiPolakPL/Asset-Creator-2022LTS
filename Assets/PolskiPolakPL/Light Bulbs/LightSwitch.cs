using UnityEngine;
[RequireComponent(typeof(Interactable))]
public class LightSwitch : MonoBehaviour
{
    Interactable interactable;
    [SerializeField] Transform lightsParent;

    LightScript[] lights;

    private void OnValidate()
    {
        if(!interactable)
            interactable = GetComponent<Interactable>();
        if(lightsParent)
            lights = lightsParent.GetComponentsInChildren<LightScript>();
    }
    private void Awake()
    {
        interactable.OnInteraction += Toggle;
    }

    public void Toggle()
    {
        if (lights.Length <= 0)
            return;
        foreach(LightScript light in lights)
        {
            if (light.isON)
                light.TurnOFF();
            else
                light.TurnON();
        }
    }
}
