using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 날짜 매니저
/// </summary>
public class TimerManager : MonoSingleton<TimerManager>
{
    // 날짜 UI
    public TimerUI timerUI;
    // 날짜 모델
    public TimerModel timerModel;

    public override void Init()
    {
        if (_isInitialized) return;
        base.Init();

        OnNewDay command = new(this);
        DayManager.Instance.AddCommand(command);
    }

    private void Start()
    {
        // 날짜 UI 초기화 
        timerUI.SetDay(timerModel.GetFormatDay());

        // 파괴 금지
        isDontDestroyOnLoad = true;
    }

    // 날짜 바꾸기 함수
    public void DayChange(int day)
    {
        // 모델에서 날짜 변경 
        timerModel.DayChange(day);
        // UI에서 날짜 변경 
        ChangeDayUI();
    }

    // 하루 보내기 함수 
    public void OneDayPass()
    {
        DayChange(1);
        Debug.Log("1일 경과");

    }

    // 날짜 UI 변경 함수 
    private void ChangeDayUI()
    {
        // 날짜 포멧 불러오기
        string day = timerModel.GetFormatDay();

        // 날짜 UI 설정 
        timerUI.SetDay(day);
    }

    // 오늘 날짜 불러오기 함수
    public LunarDateTime GetToday()
    {
        return timerModel.dateTime;
    }

    public class OnNewDay : IDayCommand
    {
        TimerManager prt;

        public OnNewDay(TimerManager timerManager)
        {
            this.prt = timerManager;
        }

        public int Priority => 1000;

        public void Execute()
        {
            prt.OneDayPass();
        }
    }
}
