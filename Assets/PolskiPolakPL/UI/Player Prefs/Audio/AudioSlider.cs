using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class AudioSlider : MonoBehaviour
{
    public AudioMixer audioMixer;
    [SerializeField] string name;
    [SerializeField] TMP_Text label;
    [SerializeField] Slider slider;

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat(name, Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat(name, volume);
    }

    public void LoadVolume()
    {
        if (PlayerPrefs.HasKey(name))
        {
            slider.value = PlayerPrefs.GetFloat(name);
            SetVolume(slider.value);
        }
        else
            SetVolume(slider.value);
        label.text = name;
    }
}
