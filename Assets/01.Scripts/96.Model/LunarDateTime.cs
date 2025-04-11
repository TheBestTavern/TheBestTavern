using System;
using System.Data;


/// <summary>
/// 음력 표기 구조체. 
/// 특징: 윤월이 포함되어있습니다.
/// 기능1: AddDays(): 지정한 일수 이후의 음력 날짜 반환. 
/// 기능2: ToString(): 날짜 string형으로 표기
/// </summary>
public struct LunarDateTime
{
    public int year;
    public int month;
    public int day;
    bool isLeapYear;
    bool isLeapMonth;
    string leapString;

    public LunarDateTime(int year, int month, int day, bool isLeapMonth)
    {
        this.year = year;
        this.month = month;
        this.day = day;
        this.isLeapYear = Extensions.lunarCalendar.IsLeapYear(year);
        this.isLeapMonth = isLeapMonth;
        if (isLeapMonth)
        {
            leapString = "윤";
        }
        else
        {
            leapString = "";
        }
    }

    public string ToString(bool korean = true)
    {
        if (korean)
        {
            return $"{year}년 {leapString}{month}월 {day}일";
        }
        else
        {
            return $"{year} {leapString}{month} {day}";
        }
    }

    public LunarDateTime AddDays(int days)
    {
        //윤년이고 윤월을 지났으면, month++
        if (isLeapYear)
        {
            if (Extensions.lunarCalendar.GetLeapMonth(year) <= month)
            {
                month++;
            }
        }

        //윤월을 그대로 양력으로 변환하면 윤월이 아닌 일반적인 월로 인식되어 변환된다. 따라 윤월의 경우 예외 처리가 필요함.
        //윤월 당월이면, 현재 day 킵해놓고 전월 마지막일로 돌린다.(윤월이므로 전월은 그냥 month임)
        //그리고 양력으로 변환한 후에 days + 킵해놓은 day을 같이 AddDays한다.
        int keepDay = 0;
        if (isLeapMonth)
        {
            keepDay = day;
            day = Extensions.lunarCalendar.GetDaysInMonth(year, month);
        }

        DateTime dateTime = Extensions.lunarCalendar.ToDateTime(year, month, day, 0, 0, 0, 0, 1);
        return dateTime.AddDays(days + keepDay).ToLunarConvertor();
    }
}

