using System;
using System.Collections.Generic;
using UnityEngine;

public class NPCData
{
    public Dictionary<int, NPC> AllNPC { get; private set; } = new();

    public void Init()
    {
        Debug.Log("NPC 인스턴스 생성");
        foreach (Data_NPC item in DataManager.Instance.DataLoader_NPC.ItemsList)
        {
            AllNPC.Add(item.key, new NPC(item));
        }

        NPCManager.Instance.OnNewDayStarted += HandleNewDay;
    }

    // 매일 NPCData가 할일
    public void HandleNewDay()
    {
    }
}

public class NPCManager : MonoSingleton<NPCManager>
{
    public NPCData NPCData;
    public Action OnNewDayStarted;

    private void Start()
    {
        NPCData = new NPCData();
        NPCData.Init();
        TriggerNewDay();  // 테스트용
    }

    // 하루가 갱신될때마다 실행될 이벤트 실행 메서드.
    public void TriggerNewDay()
    {
        OnNewDayStarted?.Invoke();
    }
}