using System;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;


/// <summary>
/// 음력 표기 구조체. 
/// 특징: 윤월이 포함되어있습니다.
/// 기능1: AddDays():일수 추가하여 반환, CompareTo():차이 반환,  ToString(), ToDateTime:양력으로 변환, 대소비교
/// </summary>
public struct LunarDateTime
{
    [field: SerializeField]
    public int year { get; private set; }
    [field: SerializeField]
    public int month { get; private set; }
    [field: SerializeField]
    public int day { get; private set; }
    [field: SerializeField]
    public bool isLeapYear { get; private set; }
    [field: SerializeField]
    public bool isLeapMonth { get; private set; }
    private readonly string leapString;

    /// <summary>
    /// 윤월 여부는 웬만하면 false(기본값)유지 해주세요. 데이터로 갖고 있는 윤년/윤월 정보와 다를 시 버그 발생.
    /// </summary>
    public LunarDateTime(int year, int month, int day, bool isLeapMonth = false)
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

    public DateTime ToDateTime()
    {
        int tempMonth = month;
        int tempDay = day;

        //윤년이고 윤월을 지났으면, month++
        if (isLeapYear)
        {
            if (Extensions.lunarCalendar.GetLeapMonth(year) <= month)
            {
                tempMonth++;
            }
        }

        //윤월을 그대로 양력으로 변환하면 윤월이 아닌 일반적인 월로 인식되어 변환된다. 따라 윤월의 경우 예외 처리가 필요함.
        //윤월 당월이면, 현재 day 킵해놓고 전월 마지막일로 돌린다.(윤월이므로 전월은 그냥 month임)
        //그리고 양력으로 변환한 후에 days + 킵해놓은 day을 같이 AddDays한다.
        int keepDay = 0;
        if (isLeapMonth)
        {
            keepDay = tempDay;
            tempDay = Extensions.lunarCalendar.GetDaysInMonth(year, tempMonth);
        }

        DateTime dateTime = Extensions.lunarCalendar.ToDateTime(year, tempMonth, tempDay, 0, 0, 0, 0, 1);
        return dateTime.AddDays(keepDay);
    }

    public LunarDateTime AddDays(int days)
    {
        return ToDateTime().AddDays(days).ToLunarConvertor();
    }

    public int CompareTo(LunarDateTime value)
    {
        DateTime date1 = ToDateTime();
        DateTime date2 = value.ToDateTime();

        return date1.CompareTo(date2);
    }

    public static bool operator <(LunarDateTime left, LunarDateTime right)
    {
        return left.ToDateTime() < right.ToDateTime();
    }

    public static bool operator >(LunarDateTime left, LunarDateTime right)
    {
        return left.ToDateTime() > right.ToDateTime();
    }

    public static bool operator <=(LunarDateTime left, LunarDateTime right)
    {
        return left.ToDateTime() <= right.ToDateTime();
    }

    public static bool operator >=(LunarDateTime left, LunarDateTime right)
    {
        return left.ToDateTime() >= right.ToDateTime();
    }

    public static bool operator ==(LunarDateTime left, LunarDateTime right)
    {
        return left.ToDateTime() == right.ToDateTime();
    }
    public static bool operator !=(LunarDateTime left, LunarDateTime right)
    {
        return left.ToDateTime() != right.ToDateTime();
    }


    /// <summary>
    /// 구조체 기본 함수 오버라이드
    /// </summary>
    public override bool Equals(System.Object obj)
    {
        if(!(obj is LunarDateTime)) return false;
        return this == (LunarDateTime)obj;
    }

    public override int GetHashCode()
    {
        return ToDateTime().GetHashCode();
    }

    public override string ToString()
    {
        return ToString(true);
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
}

