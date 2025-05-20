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
    private bool isFilp= false;

    public void SetDirection(int direction)
    {
        curDir = direction;

        if (curDir == -1 && !isFilp)
        {
            Vector3 scale = arrowImage.gameObject.transform.localScale;
            scale.x = -Mathf.Abs(scale.x);
            arrowImage.gameObject.transform.localScale = scale;
            isFilp = true;
        }
        else if (curDir == 1 && isFilp)
        {
            Vector3 scale = arrowImage.transform.localScale;
            scale.x = Mathf.Abs(scale.x); // 시계 방향
            arrowImage.transform.localScale = scale;
            isFilp = false;
        }
    }
    public void UpdateUI(float value, bool dir)
    {
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
