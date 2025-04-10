using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public static class Extensions
{
    public  static readonly KoreanLunisolarCalendar lunarCalendar = new KoreanLunisolarCalendar();

    public static LunarDateTime ToLunarConvertor(this DateTime dateTime)
    {
        int year = lunarCalendar.GetYear(dateTime);
        int month = lunarCalendar.GetMonth(dateTime);
        int day = lunarCalendar.GetDayOfMonth(dateTime);
        bool isLeapYear = lunarCalendar.IsLeapYear(year);
        bool isLeapMonth = lunarCalendar.IsLeapMonth(year, month, 1); // era = 1로 하면 되나?
        if (isLeapYear)
        {
            //윤년인 경우
            if(month >= lunarCalendar.GetLeapMonth(year))
            {
                month--;
            }
        }

        return new LunarDateTime(year, month, day, isLeapMonth);
    } 
}
