using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;


public enum HolidayType
{
    newYear,
}
public class CalendarManager : MonoSingleton<CalendarManager>
{
    public DesignEnums.SeasonType CurrentSeasonType { get; private set; }

    Dictionary<LunarDateTime, HolidayType> holidays = new()
    {
        { new LunarDateTime(1000, 1, 1, false), HolidayType.newYear }
    };

    public override void Init()
    {
        if (_isInitialized) return;
        base.Init();

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        OnNewDay command = new(this);
        CommandManager.Instance.AddCommand(command);
    }

    public class OnNewDay : IDayCommand
    {
        private CalendarManager prt;
        public int Priority => 1900;

        public OnNewDay(CalendarManager calendarManager)
        {
            this.prt = calendarManager;
        }

        public Task Execute()
        {
            LunarDateTime today = TimerManager.Instance.GetToday();

            DesignEnums.SeasonType? newSeason = CheckChangeSeason(today);
            if (newSeason.HasValue)
            {
                // 계절 변화 실행
                Debug.Log($"계절이 {prt.CurrentSeasonType}에서 {newSeason}으로 변화합니다.");
                ChangeCurrentSeason(newSeason.Value);
                EventBus.Publish<SeasonChangeEvent>(new SeasonChangeEvent(newSeason.Value));
            }

            HolidayType? holiday = CheckHoliday(today);
            if (holiday.HasValue)
            {
                // 공휴일 이벤트 실행.
                Debug.Log($"오늘은 명절 {holiday}입니다");
            }

            return Task.CompletedTask;
        }

        public bool isValid()
        {
            return prt != null;
        }

        private DesignEnums.SeasonType? CheckChangeSeason(LunarDateTime dateTime)
        {
            var todaySeason = WhichSeason(dateTime);
            if (prt.CurrentSeasonType != todaySeason) // 계절 변화 판단
            {
                return todaySeason;

            }
            return null;
        }

        private void ChangeCurrentSeason(DesignEnums.SeasonType newSeason)
        {
            prt.CurrentSeasonType = newSeason;
        }

        private DesignEnums.SeasonType WhichSeason(LunarDateTime lunarDate)
        {
            //DateTime sunDate = lunarDate.ToDateTime();
            int year = lunarDate.year;

            if (lunarDate >= new LunarDateTime(year, 10, 1))
            {
                //겨울
                return DesignEnums.SeasonType.winter;
            }
            else if (lunarDate >= new LunarDateTime(year, 7, 1))
            {
                // 가을
                return DesignEnums.SeasonType.fall;
            }
            else if (lunarDate >= new LunarDateTime(year, 4, 1))
            {
                // 여름
                return DesignEnums.SeasonType.summer;
            }
            else if (lunarDate >= new LunarDateTime(year, 1, 1))
            {
                //봄
                return DesignEnums.SeasonType.spring;
            }
            else
            {
                // 겨울
                return DesignEnums.SeasonType.winter;
            }

            // 양력 기반 실제 계절 도입 (게임에서 쓸려면 달력 메뉴가 있어야함)
            //DateTime sunDate = lunarDate.ToDateTime();
            //int year = sunDate.Year;
            //Debug.Log($"오늘의 태양력은 {sunDate.ToString()}입니다.");

            //if (sunDate >= new DateTime(year, 11, 7))
            //{
            //    //겨울
            //    return DesignEnums.SeasonType.winter;
            //    Debug.Log($"겨울");
            //}
            //else if (sunDate >= new DateTime(year, 8, 7))
            //{
            //    // 가을
            //    return DesignEnums.SeasonType.fall;
            //    Debug.Log($"가을");
            //}
            //else if (sunDate >= new DateTime(year, 5, 5))
            //{
            //    // 여름
            //    return DesignEnums.SeasonType.summer;
            //    Debug.Log($"여름");
            //}
            //else if (sunDate >= new DateTime(year, 2, 4))
            //{
            //    //봄
            //    return DesignEnums.SeasonType.spring;
            //    Debug.Log($"봄");
            //}
            //else
            //{
            //    // 겨울
            //    return DesignEnums.SeasonType.winter;
            //    Debug.Log($"겨울");
            //}
        }

        private HolidayType? CheckHoliday(LunarDateTime dateTime)
        {
            LunarDateTime dateWithoutYear = new LunarDateTime(1000, dateTime.month, dateTime.day, dateTime.isLeapMonth);
            if (prt.holidays.TryGetValue(dateWithoutYear, out HolidayType holiday))
                return holiday;
            return null;
        }
    }
}