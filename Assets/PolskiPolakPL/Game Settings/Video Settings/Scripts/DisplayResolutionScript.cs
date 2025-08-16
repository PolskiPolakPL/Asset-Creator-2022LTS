using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class DisplayResolutionScript : MonoBehaviour
{

    [SerializeField] TMP_Dropdown resolutionDropdown;
    [HideInInspector] public int CurrentResIndex = 0;
    Resolution[] resolutions;

    private void Awake()
    {
        SetUpResolutionDropdown();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        CurrentResIndex = resolutionIndex;
    }

    private void SetUpResolutionDropdown()
    {
        resolutions = Screen.resolutions;

        List<Resolution> resolutionsList = GetResolutionList(resolutions);
        resolutionsList = InvertResolutionList(resolutionsList);

        List<string> optionsList = GetResolutionOptions(resolutionsList);
        CurrentResIndex = GetCurrentResolutionIndex(resolutionsList);

        //Setting up resolution dropdown
        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(optionsList);
        resolutionDropdown.SetValueWithoutNotify(CurrentResIndex);
        resolutionDropdown.RefreshShownValue();
        resolutions = resolutionsList.ToArray();
    }


    List<Resolution> GetResolutionList(Resolution[] resArray)
    {
        List<Resolution> resolutionsList = new List<Resolution>();
        foreach(Resolution resolution in resArray)
        {
            resolutionsList.Add(resolution);
        }
        return resolutionsList;
    }

    List<Resolution> InvertResolutionList(List<Resolution> resList)
    {
        List<Resolution> newResList = new List<Resolution>();
        int lastResListIndex = resList.Count - 1;
        for(int i = lastResListIndex; i >= 0; i--)
        {
            newResList.Add(resList[i]);
        }
        return newResList;
    }

    List<string> GetResolutionOptions(List<Resolution> resList)
    {
        List<string> options = new List<string>();
        foreach(Resolution res in resList)
        {
            options.Add($"{res.width}x{res.height} ({res.refreshRateRatio}Hz)");
        }
        return options;
    }

    int GetCurrentResolutionIndex(List<Resolution> resList)
    {
        int resIndex = 0;
        int currentScreenWidth = Screen.currentResolution.width;
        int currentScreenHeight = Screen.currentResolution.height;

        foreach (Resolution resolution in resList)
        {
            if (resolution.width == currentScreenWidth && resolution.height == currentScreenHeight)
                break;
            resIndex++;
        }
        return resIndex;
    }
}