using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class TimerModel : MonoBehaviour
{
    public int year;
    public int month;
    public int day;

    public int hour;
    public int minute;

    public void TimeChange(int minute)
    {
        this.minute += minute;
        while (this.minute >= 60)
        {
            this.minute -= 60;
            hour += 1;
            if (hour > 23)
            {
                hour = 0;
                DayChange(1);
            }
        }
    }

    public void DayChange(int day)
    {
        this.day += day;

        while (this.day > GetLastDay(month))
        {
            this.day -= GetLastDay(month);
            month += 1;
        }

        while (month > 12)
        {
            month -= 12;
            year += 1;
        }
    }

    int GetLastDay(int month)
    {
        if (month == 2)
            return 28;
        if (month == 4 || month == 6 || month == 9 || month == 11)
            return 30;
        return 31;
    }

    public string GetFormatTime()
    {
        string formatTime = $"{hour:D2} : {minute:D2}";
        return formatTime;
    }

    public string GetFormatDay()
    {
        string formatDay = $"{year}년 {month}월 {day}일";
        return formatDay;
    }
}
