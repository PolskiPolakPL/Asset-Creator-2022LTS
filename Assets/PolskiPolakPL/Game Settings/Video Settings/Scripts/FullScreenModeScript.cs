using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FullScreenModeScript : MonoBehaviour
{
    [SerializeField] TMP_Dropdown displayDropdown;
    [HideInInspector] public int CurrentModeIndex = 0;

    private void Awake()
    {
        SetUpDisplayModeDropdown();
    }

    void SetUpDisplayModeDropdown()
    {
        displayDropdown.ClearOptions();
        List<string> options = new List<string>();
        options.Add("Full Screen");
        options.Add("Borderless");
        options.Add("Windowed");
        displayDropdown.AddOptions(options);
        GetCurentDisplayModeIndex();
        displayDropdown.SetValueWithoutNotify(CurrentModeIndex);
    }

    public void SetDisplayMode(int displayModeIndex)
    {
        switch (displayModeIndex)
        {
            case 0:
                {
                    Screen.fullScreenMode = UnityEngine.FullScreenMode.ExclusiveFullScreen;
                }
                break;
            case 1:
                {
                    Screen.fullScreenMode = UnityEngine.FullScreenMode.FullScreenWindow;
                }
                break;
            case 2:
                {
                    Screen.fullScreenMode = UnityEngine.FullScreenMode.Windowed;
                }
                break;
        }
        CurrentModeIndex = displayModeIndex;
    }

    void GetCurentDisplayModeIndex()
    {
        switch (Screen.fullScreenMode)
        {
            case UnityEngine.FullScreenMode.ExclusiveFullScreen:
                {
                    CurrentModeIndex = 0;
                }
                break;

            case UnityEngine.FullScreenMode.FullScreenWindow:
                {
                    CurrentModeIndex = 1;
                }
                break;

            case UnityEngine.FullScreenMode.Windowed:
                {
                    CurrentModeIndex = 2;
                }
                break;

        }
    }
}