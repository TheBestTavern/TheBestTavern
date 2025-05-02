using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

/// <summary>
/// 날짜 모델
/// </summary>
public class TimerModel
{
    // 음력 시간
    public LunarDateTime dateTime;

    //private void Awake()
    //{
    //    // 날짜 초기화
    //    dateTime = new LunarDateTime(1234, 11, 26, false);
    //}

    public TimerModel(int year, int month, int day, bool isLeapMonth)
    {
        dateTime = new LunarDateTime(year, month, day, isLeapMonth);
    }

    // 날짜 바꾸기
    public void DayChange(int day)
    {
        dateTime = dateTime.AddDays(day);
    }

    // UI용 포멧 변경 
    public string GetFormatDay()
    {
        string formatDay = dateTime.ToString(true);
        return formatDay;
    }
}
