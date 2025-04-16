using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TensionGauge : MonoBehaviour
{
    [Header("게이지 설정")]
    public float currentValue = 0f;
    public float maxValue = 1f;
    public float gaugeSpeed = 0.5f; // 게이지 속도
    public float overloadTimeThreshold = 1.5f;
    public Image gaugeBar;

    private float overloadTimer = 0f;

    void Update()
    {
        UpdateGaugeUI(); // UI 반영
    }

    public void IncreaseGauge()
    {
        currentValue = Mathf.Clamp(currentValue + gaugeSpeed * Time.deltaTime, 0, maxValue);

        if (currentValue >= maxValue)
        {
            overloadTimer += Time.deltaTime;
        }
        else
        {
            overloadTimer = 0f;
        }
    }

    public void DecreaseGauge()
    {
        currentValue = Mathf.Clamp(currentValue - gaugeSpeed * Time.deltaTime, 0, maxValue);
        overloadTimer = 0f;
    }

    public bool IsOverloaded()
    {
        return overloadTimer >= overloadTimeThreshold;
    }

    public void ResetGauge()
    {
        currentValue = 0f;
        overloadTimer = 0f;
    }

    private void UpdateGaugeUI()
    {
        if (gaugeBar != null)
        {
            gaugeBar.fillAmount = currentValue / maxValue;
        }
    }
}
