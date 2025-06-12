using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class ScreenResolutionPopUp : BasePopUp
{
    [Header("화면 설정")]
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Toggle fullScreenToggle;

    private Resolution[] resolutions;
    private List<Resolution> res43List = new List<Resolution>();
    private int currentResolutionIndex = 0;
    private bool isFullScreen;

    [Header("품질 설정")]
    [SerializeField] private Slider qualitySlider;
    [SerializeField] private TextMeshProUGUI qualityText;
    private int currentQualityIndex = -1;
    private string[] qualityTexts = { "매우 낮음", "낮음", "보통", "높음", "매우 높음", "울트라" };

    [Header("프레임 설정")]
    [SerializeField] private Slider frameSlider;
    [SerializeField] private TextMeshProUGUI frameText;
    private int currentframeIndex = -1;
    private int[] frames = { 30, 60, 90, 120, -1 };
    [SerializeField] private Toggle vSyncCountToggle;

    private void Start()
    {
        InitScreenSet();
        InitQualitySet();
        InitFrameSet();

        resolutionDropdown.onValueChanged.AddListener(SetResolution);
        fullScreenToggle.onValueChanged.AddListener(SetScreenMode);

        qualitySlider.onValueChanged.AddListener(SetQuality);

        frameSlider.onValueChanged.AddListener(SetFrame);
        vSyncCountToggle.onValueChanged.AddListener(SetvSync);
    }

    private void InitScreenSet()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        fullScreenToggle.isOn = Screen.fullScreen;
        isFullScreen = Screen.fullScreen;

        HashSet<string> resolutionKey = new HashSet<string>();
        foreach (Resolution res in resolutions)
        {
            float aspect = (float)res.width / res.height;
            string key = res.width + " " + res.height;
            if (Mathf.Abs(aspect - 4f / 3f) < 0.01f && !resolutionKey.Contains(key))
            {
                res43List.Add(res);
                resolutionKey.Add(key);
            }
        }

        List<string> options = new List<string>();

        for (int i = 0; i < res43List.Count; i++)
        {
            string option = res43List[i].width + " x " + res43List[i].height;
            options.Add(option);
            if (res43List[i].width == Screen.width &&
                res43List[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    private void InitQualitySet()
    {
        qualitySlider.wholeNumbers = true;
        qualitySlider.minValue = 0;
        qualitySlider.maxValue = QualitySettings.names.Length - 1;

        currentQualityIndex = QualitySettings.GetQualityLevel();
        qualitySlider.value = currentQualityIndex;
        qualityText.text = qualityTexts[currentQualityIndex];
    }

    private void InitFrameSet()
    {
        frameSlider.wholeNumbers = true;
        frameSlider.minValue = 0;
        frameSlider.maxValue = 4;


        QualitySettings.vSyncCount = 0;
        vSyncCountToggle.isOn = false;
        currentframeIndex = 1;

        SetFrame(currentframeIndex);
        frameSlider.value = currentframeIndex;
    }

    private void SetResolution(int index)
    {
        Resolution res = res43List[index];
        Screen.SetResolution(res.width, res.height, isFullScreen);
    }

    private void SetScreenMode(bool isFullScreen)
    {
        this.isFullScreen = isFullScreen;
        Screen.fullScreen = isFullScreen;
    }

    private void SetQuality(float value)
    {
        int index = Mathf.RoundToInt(value);

        if (index != currentQualityIndex)
        {
            currentQualityIndex = index;
            QualitySettings.SetQualityLevel(index);
            qualityText.text = qualityTexts[index];
        }
    }

    private void SetFrame(float value)
    {
        currentframeIndex = Mathf.RoundToInt(value);
        int frame = frames[currentframeIndex];
        Application.targetFrameRate = frame;

        frameText.text = frame == -1 ? "무제한" : $"{frame} FPS";
    }

    private void SetvSync(bool isvSync)
    {
        QualitySettings.vSyncCount = isvSync ? 1 : 0;
        frameSlider.interactable = !isvSync;
    }
}
