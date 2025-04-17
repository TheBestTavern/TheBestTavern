using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public Action onNewDayAction;

    private void Start()
    {
        DataManager.Instance.Init();
        QuestManager.Instance.Init();
        NPCManager.Instance.Init(); 

        TriggerNewDayAction();
    }

    public void TriggerNewDayAction()
    {
        onNewDayAction?.Invoke();
    }
}
