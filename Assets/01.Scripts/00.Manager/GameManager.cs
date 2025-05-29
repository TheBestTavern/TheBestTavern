using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public Action onSceneMoveAfter;
    public Action onSceneMoveBefore;

    public bool isAnalyticsAgreed = false;

    public override void Init()
    {
        if (_isInitialized) return;
        base.Init();

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        UnityEngine.Application.targetFrameRate = 60;
        DataManager.Instance.Init();
        QuestManager.Instance.Init();
        NPCManager.Instance.Init();
        TimerManager.Instance.Init();
        InventoryManager.Instance.Init();
        DayAndNightManager.Instance.Init();
        //SoundManager.Instance.Init();
        SaveLoadManager.Instance.Init();
        UIManager.Instance.Init();
        SoundManager.Instance.Init();
        CalendarManager.Instance.Init();

        CommandManager.Instance.ExecuteCommands(1002);
    }    
}
