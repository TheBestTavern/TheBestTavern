using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoSingleton<TimerManager>
{
    public TimerUI timerUI;
    public TimerModel timerModel;

    private void Start()
    {
        timerUI.SetDay(timerModel.GetFormatDay());
        isDontDestroyOnLoad = true;
    }

    public void DayChange(int day)
    {
        timerModel.DayChange(day);
        ChangeDayUI();
    }

    public void OneDayPass()
    {
        DayChange(1);
        GameManager.Instance.TriggerNewDayAction();
    }

    private void ChangeDayUI()
    {
        string day = timerModel.GetFormatDay();
        timerUI.SetDay(day);
    }

    public LunarDateTime GetToday()
    {
        return timerModel.dateTime;
    }

}
