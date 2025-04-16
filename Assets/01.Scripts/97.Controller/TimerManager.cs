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
        // 매일 날짜가 변할때마다 필요한 액션 
        GameManager.Instance.TriggerNewDayAction();
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

}
