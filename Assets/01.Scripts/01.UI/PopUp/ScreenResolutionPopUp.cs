using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ScreenResolutionPopUp : BasePopUp
{
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Toggle fullScreenToggle;

    private Resolution[] resolutions;
    private List<Resolution> res43List = new List<Resolution>();
    private int currentResolutionIndex = 0;
    private bool isFullScreen;

    [SerializeField] private Slider qualitySlider;
    [SerializeField] private TextMeshProUGUI qualityText;
    private int currentQualityIndex = -1;
    private string[] qualityTexts = { "매우 낮음", "낮음", "보통", "높음", "매우 높음", "울트라" };


    private void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        fullScreenToggle.isOn = Screen.fullScreen;
        isFullScreen = Screen.fullScreen;
        fullScreenToggle.onValueChanged.AddListener(SetScreenMode);

        foreach (Resolution res in resolutions)
        {
            float aspect = (float)res.width / res.height;
            if (Mathf.Abs(aspect - 4f / 3f) < 0.01f)
            {
                res43List.Add(res);
            }
        }

        List<string> options = new List<string>();

        for (int i = 0; i < res43List.Count; i++)
        {
            string option = res43List[i].width + " x " + res43List[i].height + " " + res43List[i].refreshRateRatio + "Hz";
            options.Add(option);

            if (res43List[i].width == Screen.currentResolution.width &&
                res43List[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        resolutionDropdown.onValueChanged.AddListener(SetResolution);

        qualitySlider.wholeNumbers = true;
        qualitySlider.minValue = 0;
        qualitySlider.maxValue = QualitySettings.names.Length - 1;

        qualitySlider.onValueChanged.AddListener(QualitySetting);

        currentQualityIndex = QualitySettings.GetQualityLevel();
        qualitySlider.value = currentQualityIndex;
        qualityText.text = qualityTexts[currentQualityIndex];
    }

    public void SetResolution(int index)
    {
        Resolution res = res43List[index];
        Screen.SetResolution(res.width, res.height, isFullScreen);
    }

    public void SetScreenMode(bool isFullScreen)
    {
        this.isFullScreen = isFullScreen;
        Screen.fullScreen = isFullScreen;
    }

    public void QualitySetting(float value)
    {
        int index = Mathf.RoundToInt(value);

        if (index != currentQualityIndex)
        {
            currentQualityIndex = index;
            QualitySettings.SetQualityLevel(index);
            qualityText.text = qualityTexts[index];
        }
    }
}
