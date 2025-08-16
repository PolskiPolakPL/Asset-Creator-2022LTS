using UnityEngine;
using UnityEngine.UI;

public class VideoSettings : MonoBehaviour
{
    [SerializeField] Toggle vSynchToggle;
    [SerializeField] Toggle antiAliasingToggle;
    [SerializeField] Slider fpsSlider;

    private void Start()
    {
        InitializeSettings();
    }

    public void SetFPSLimiter(float value)
    {
        int _fps = (int)value;
        Application.targetFrameRate = _fps;
        PlayerPrefs.SetInt("FPSLimit", _fps);
    }

    public void SetVSync(bool isOn)
    {
        if (isOn)
            QualitySettings.vSyncCount = 1;
        else
            QualitySettings.vSyncCount = 0;
        PlayerPrefs.SetInt("Vsync", QualitySettings.vSyncCount);
    }

    public void SetAntiAliasing(bool isOn)
    {
        if (isOn)
            QualitySettings.antiAliasing = 8;
        else
            QualitySettings.antiAliasing = 0;
        PlayerPrefs.SetInt("AntiAliasing", QualitySettings.antiAliasing);
    }

    

    private bool IntToBool(int value)
    {
        return value > 0;
    }

    private void InitializeSettings()
    {
        //FPS limiter
        if (PlayerPrefs.HasKey("FPSLimit"))
        {
            fpsSlider.value = (float)PlayerPrefs.GetInt("FPSLimit");
        }
        SetFPSLimiter(fpsSlider.value);

        //V-Sync
        if (PlayerPrefs.HasKey("Vsync"))
        {
            vSynchToggle.isOn = IntToBool(PlayerPrefs.GetInt("Vsync"));
        }
        SetVSync(vSynchToggle.isOn);

        //Anti-Aliasing
        if (PlayerPrefs.HasKey("AntiAliasing"))
        {
            antiAliasingToggle.isOn = IntToBool(PlayerPrefs.GetInt("AntiAliasing"));
        }
        SetAntiAliasing(antiAliasingToggle.isOn);
    }
}
