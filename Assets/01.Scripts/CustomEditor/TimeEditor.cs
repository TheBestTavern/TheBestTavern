using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TimeEditor : MonoBehaviour
{
    public TimerController timerController;
    public int day;

    public void AdvanceDay()
    {
        timerController.DayChange(day);
    }
}

[CustomEditor(typeof(TimeEditor))]
public class TimeAdvanceButton : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        TimeEditor TimeEditor = (TimeEditor)target;
        if (EditorApplication.isPlaying)
        {
            if (GUILayout.Button($"{TimeEditor.day}일 보내기"))
            {
                TimeEditor.AdvanceDay();
            }
        }
    }
}
