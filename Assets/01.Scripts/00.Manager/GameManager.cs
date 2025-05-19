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
        DayAndNightManager.Instance.Init();
        //SoundManager.Instance.Init();
        SaveLoadManager.Instance.Init();
        UIManager.Instance.Init();

        CommandManager.Instance.ExecuteCommands(1001); 
    }
}
