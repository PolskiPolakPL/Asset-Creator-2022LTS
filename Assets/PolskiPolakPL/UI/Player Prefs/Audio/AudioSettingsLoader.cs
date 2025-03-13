using UnityEngine;
using UnityEngine.Audio;

public class AudioSettingsLoader : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [Header("----- Audio Settings -----")]
    [SerializeField] AudioSlider[] audioSliders;

    private void Start()
    {
            LoadAudioSettings();
    }

    private void LoadAudioSettings()
    {
       foreach(AudioSlider audioSlider in audioSliders)
        {
            audioSlider.audioMixer = audioMixer;
            audioSlider.LoadVolume();
        }
    }
}
