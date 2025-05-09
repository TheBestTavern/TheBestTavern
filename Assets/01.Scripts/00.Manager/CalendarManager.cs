using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;


public enum HolidayType
{
    newYear,
}
public class CalendarManager : MonoSingleton<CalendarManager>
{
    DesignEnums.SeasonType currentSeasonType;

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
                Debug.Log($"계절이 {prt.currentSeasonType}에서 {newSeason}으로 변화합니다.");
                ChangeSeason(newSeason.Value);
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
            if (prt.currentSeasonType != todaySeason) // 계절 변화 판단
            {
                return todaySeason;

            }
            return null;
        }

        private void ChangeSeason(DesignEnums.SeasonType newSeason)
        {
            prt.currentSeasonType = newSeason;
        }

        private DesignEnums.SeasonType WhichSeason(LunarDateTime dateTime)
        {
            int year = dateTime.year;

            if (dateTime >= new LunarDateTime(year, 11, 7))
            {
                //겨울
                return DesignEnums.SeasonType.winter;
            }
            else if (dateTime >= new LunarDateTime(year, 8, 7))
            {
                // 가을
                return DesignEnums.SeasonType.fall;
            }
            else if (dateTime >= new LunarDateTime(year, 5, 5))
            {
                // 여름
                return DesignEnums.SeasonType.summer;
            }
            else if (dateTime >= new LunarDateTime(year, 2, 4))
            {
                //봄
                return DesignEnums.SeasonType.spring;
            }
            else
            {
                // 겨울
                return DesignEnums.SeasonType.winter;
            }
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