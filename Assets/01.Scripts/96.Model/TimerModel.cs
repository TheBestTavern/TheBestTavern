using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class TimerModel : MonoBehaviour
{
    public LunarDateTime dateTime;

    private void Awake()
    {
        dateTime = new LunarDateTime(1234, 11, 26, false);
    }

    public void DayChange(int day)
    {
        dateTime = dateTime.AddDays(day);
    }

    public string GetFormatDay()
    {
        string formatDay = dateTime.ToString(true);
        return formatDay;
    }
}
