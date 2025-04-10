using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    public TimerUI timerUI;
    public TimerModel timerModel;

    private void Awake()
    {
        timerUI.SetTime(timerModel.GetFormatTime());
        timerUI.SetDay(timerModel.GetFormatDay());
    }

    public void TimeChange(int minute)
    {
        timerModel.TimeChange(minute);
        ChangeTimeUI();
    }


    private void ChangeTimeUI()
    {
        string hour = timerModel.GetFormatTime();
        timerUI.SetTime(hour);
    }

    public void DayChange(int day)
    {
        timerModel.DayChange(day);
        ChangeDayUI();
    }

    private void ChangeDayUI()
    {
        string day = timerModel.GetFormatDay();
        timerUI.SetDay(day);
    }
}
