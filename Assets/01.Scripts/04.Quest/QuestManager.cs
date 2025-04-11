using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QuestManager : MonoSingleton<QuestManager>
{
    public QuestData questData;
    public Action OnNewDayStarted;

    public List<Quest> AllQuests => questData.AllQuests;
    public List<Quest> AcceptedQuests => questData.AcceptedQuests;
    public List<Quest> CompletedQuests => questData.CompletedQuests;
    public List<Quest> TodayAvailableQuest => questData.TodayAvailableQuest;

    private void Start()
    {
        isDontDestroyOnLoad = true;
        questData = new QuestData();
        questData.Init();
        TriggerNewDay();  // 테스트용
     }


    // 퀘스트 수령
    public void AcceptQuest(Quest quest)
    {
        questData.AcceptQuest(quest);
    }

    // 퀘스트 완료
    public void CompleteQuest(Quest quest)
    {
        questData.CompleteQuest(quest);
    }

    // 하루가 갱신될때마다 실행될 이벤트 실행 메서드.
    public void TriggerNewDay()
    {
        OnNewDayStarted?.Invoke();
    }
}
