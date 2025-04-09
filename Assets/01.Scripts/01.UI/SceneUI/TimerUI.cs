using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dayText;
    [SerializeField] private TextMeshProUGUI timeText;

    public void SetTime(string time)
    {
        timeText.text = time;
    }

    public void SetDay(string day)
    {
        dayText.text = day;
    }
}
