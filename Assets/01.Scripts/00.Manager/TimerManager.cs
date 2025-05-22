using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Mathematics;
using Unity.Services.Analytics;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static Cinemachine.CinemachineTriggerAction.ActionSettings;

/// <summary>
/// 날짜 매니저
/// </summary>
public class TimerManager : MonoSingleton<TimerManager>
{
    // 날짜 UI
    public TimerUI timerUI;
    // 날짜 모델
    public TimerModel timerModel;

    public int startyear = 1447;
    public int startmonth = 1;
    public int startday = 1;

    public override void Init()
    {
        if (_isInitialized) return;
        base.Init();

        DontDestroyOnLoad(this);

        if (SceneParameter.TryGet<LunarDateTime>("savedDate", out LunarDateTime savedDate))
        {
            startyear = savedDate.year;
            startmonth = savedDate.month;
            startday = savedDate.day;
        }

        timerModel = new TimerModel(startyear, startmonth, startday, false);
        OnSceneMove();

        OnNewDay_DayPass command1 = new(this);
        CommandManager.Instance.AddCommand(command1);
        OnNewDay_SetTimeUI command2 = new(this);
        CommandManager.Instance.AddCommand(command2);
    }

    public void ApplyLoadData(LunarDateTime savedDay)
    {
        timerModel.dateTime = savedDay;
    }

    public void OnSceneMove() // 씬이동, 게임시작할때 한번씩 실행.
    {
        if (timerUI == null)
        {
            timerUI = FindObjectOfType<TimerUI>();
            if (timerUI != null)
            {
                ChangeDayUI();
            }
        }
    }

    //private void Start()
    //{
    //    // 날짜 UI 초기화 
    //    timerUI.SetDay(timerModel.GetFormatDay());

    //    // 파괴 금지
    //    isDontDestroyOnLoad = true;
    //}

    // 날짜 바꾸기 함수
    public void DaysPass(int day)
    {
        // 모델에서 날짜 변경 
        timerModel.DayChange(day);
    }

    // 하루 보내기 함수 
    public void OneDayPass()
    {
        DaysPass(1);
        Debug.Log("1일 경과");

        // 플레이어 날짜 -> 이전 데이터 갱신(최대 날짜용)
        if (GameManager.Instance.isAnalyticsAgreed)
        {
            var today = GetToday();
            string date = $"{today.year:D4}-{today.month:D2}-{today.day:D2}";
            var TimeEvent = new AnalyticsTime("TimeData")
            {
                dateData = date
            };
            AnalyticsService.Instance.RecordEvent(TimeEvent);
        }
    }

    // 날짜 UI 변경 함수 
    public void ChangeDayUI()
    {
        // 날짜 포멧 불러오기
        string day = timerModel.GetFormatDay();

        string season;
        switch (CalendarManager.Instance.CurrentSeasonType.ToString())
        {
            case "spring":
                season = "봄";
                break;
            case "summer":
                season = "여름";
                break;
            case "fall":
                season = "가을";
                break;
            case "winter":
                season = "겨울";
                break;
            default:
                season = "Error";
                break;
        }

        // 날짜 UI 설정 
        timerUI.SetTimer(day, season);
    }

    // 오늘 날짜 불러오기 함수
    public LunarDateTime GetToday()
    {
        return timerModel.dateTime;
    }

    public class OnNewDay_DayPass : IDayCommand
    {
        TimerManager prt;

        public OnNewDay_DayPass(TimerManager timerManager)
        {
            this.prt = timerManager;
        }

        public int Priority => 1000;

        public Task Execute()
        {
            prt.OneDayPass();

            return Task.CompletedTask;
        }

        public bool isValid()
        {
            return prt != null;
        }
    }

    public class OnNewDay_SetTimeUI : IDayCommand
    {
        TimerManager prt;

        public OnNewDay_SetTimeUI(TimerManager timerManager)
        {
            this.prt = timerManager;
        }

        public int Priority => 1990;

        public Task Execute()
        {
            prt.ChangeDayUI();

            return Task.CompletedTask;
        }

        public bool isValid()
        {
            return prt != null;
        }
    }
}
