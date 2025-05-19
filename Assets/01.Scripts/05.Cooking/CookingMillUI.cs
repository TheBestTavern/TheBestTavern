using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookingMillUI : MonoBehaviour
{
    [SerializeField] private Slider gaugeBar;
    [SerializeField] private Image fillImage;
    [SerializeField] private Image arrowImage;

    private int curDir;

    public void SetDirection(int direction)
    {
        curDir = direction;
    }
    public void UpdateUI(float value, bool dir)
    {
        if (curDir == -1) 
        {
            Vector3 scale = arrowImage.gameObject.transform.localScale;
            scale.x = - scale.x;
            arrowImage.gameObject.transform.localScale = scale;
        }
        fillImage.color = Color.red;
        arrowImage.color = Color.white;

        gaugeBar.value = value;
        gaugeBar.maxValue = 600f;
        gaugeBar.minValue = 0f;

        if (value >= 200f && value <= 400f)
        {
            fillImage.color = Color.green;
            arrowImage.color = Color.green;
        }

        if (!dir)
        {
            arrowImage.color = Color.red;
        }
        else
        {
            arrowImage.color = Color.green;
        }

        if (value <= 20)
        {
            arrowImage.color = Color.white;
        }
    }
}
