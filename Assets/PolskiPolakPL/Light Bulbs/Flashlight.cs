using UnityEngine;

public class Flashlight : MonoBehaviour
{
    Light lightSrc;
    private void Awake()
    {
        lightSrc = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            ToggleFlashlight();
    }

    private void ToggleFlashlight()
    {
        lightSrc.enabled = !lightSrc.enabled;
    }
}
