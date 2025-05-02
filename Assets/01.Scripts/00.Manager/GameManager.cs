using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public Action onSceneMoveAfter;
    public Action onSceneMoveBefore;

    private void Start()
    {
        DataManager.Instance.Init();
        QuestManager.Instance.Init();
        NPCManager.Instance.Init();
        TimerManager.Instance.Init();
        InventoryManager.Instance.Init();

        DayManager.Instance.ExecuteCommands(1001); 
    }

    public void TriggerSceneMoveAfterEvents() => onSceneMoveAfter?.Invoke();
    public void TriggerSceneMoveBeforeEvents() => onSceneMoveBefore?.Invoke();
}
