using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    public TimerUI timerUI;
    public TimerModel timerModel;

    private void Start()
    {
        timerUI.SetDay(timerModel.GetFormatDay());
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
