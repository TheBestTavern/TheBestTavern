using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class TimerModel : MonoBehaviour
{
    public DateTime dateTime;

    private void Awake()
    {
        dateTime = new DateTime(1234, 11, 28);
    }

    public void DayChange(float day)
    {
        dateTime = dateTime.AddDays(day);
    }

    public string GetFormatDay()
    {
        string formatDay = dateTime.ToString("yyyy년 MM월 dd일");
        return formatDay;
    }
}
