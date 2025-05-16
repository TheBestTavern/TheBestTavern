using BamaoUIPack.Scripts;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CookingMixingBowlUI : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI valueText;

    public void UpdateUI(float value)
    {
        slider.minValue = 0f;
        slider.maxValue = 100f;
        float curValue = value;

        float sliderValue = Mathf.Clamp01(curValue / 5f);
        slider.value = sliderValue * 100;
        valueText.text = $"{slider.value.ToString("N2")} % ";
    }
}
