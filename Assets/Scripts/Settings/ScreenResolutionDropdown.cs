using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ScreenResolutionDropdown : TMP_Dropdown
{
    public event Action<Resolution> OnNewResolutionSelected;

    private Resolution[] _resolutions;
    private List<Resolution> _filteredResolutions = new List<Resolution>();

    private float _currentRefreshRate;
    private int _currentResolutionIndex;

    public void Initialize()
    {
        AddOptions();
        onValueChanged.AddListener(SelectResolution);
    }

    private void AddOptions()
    {
        _resolutions = Screen.resolutions;
        _currentRefreshRate = Screen.currentResolution.refreshRate;

        ClearOptions();

        for(int i = 0; i < _resolutions.Length; i++)
        {
            if(_resolutions[i].refreshRate == _currentRefreshRate)
            {
                _filteredResolutions.Add(_resolutions[i]);
            }
        }

        List<string> options = new List<string>();

        for(int i = 0; i < _filteredResolutions.Count; i++)
        {
            string resolutionOptions = $"{_filteredResolutions[i].width} x {_filteredResolutions[i].height} {_filteredResolutions[i].refreshRate}Hz";
            options.Add(resolutionOptions);

            if(_filteredResolutions[i].width == Screen.width && _filteredResolutions[i].height == Screen.height)
            {
                _currentResolutionIndex = i;
            }
        }

        AddOptions(options);
        value = options.Count;
        SelectResolution(value);
        RefreshShownValue();
    }

    public void SelectResolution(int resolutionIndex)
    {
        Resolution resolution = _filteredResolutions[resolutionIndex];
        OnNewResolutionSelected?.Invoke(resolution);
    }
}
