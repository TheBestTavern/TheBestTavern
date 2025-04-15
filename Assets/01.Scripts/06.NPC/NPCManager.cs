using System;
using System.Collections.Generic;
using UnityEngine;


public class NPCManager : MonoSingleton<NPCManager>
{
    public NPCData NPCData;
    public Action onNewDayStarted;

    private void Start()
    {
        NPCData = new NPCData();
        NPCData.Init();
        GameManager.Instance.onNewDayAction = TriggerNewDay;
    }

    // 하루가 갱신될때마다 실행될 이벤트 실행 메서드.
    public void TriggerNewDay()
    {
        onNewDayStarted?.Invoke();
    }
}