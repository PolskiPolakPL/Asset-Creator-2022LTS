using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    [SerializeField] float delay;
    [SerializeField] float flickerTime = 0.1f;
    [SerializeField] List<LightScript> lights = new List<LightScript>();
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(FlickerLights), delay, flickerTime);
    }


    void FlickerLights()
    {
        foreach (LightScript light in lights)
        {
            light.Flicker();
        }
    }
}
