using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QuestData
{
    //private List<Quest> _acceptedQuests = new();
    //private List<Quest> _completedQuests = new();
    //private List<Quest> _allQuests = new();

    //public IReadOnlyList<Quest> acceptedQuestsList => _acceptedQuests;
    //public IReadOnlyList<Quest> completedQuestsList => _completedQuests;
    //public IReadOnlyList<Quest> allQuests => _allQuests;

    public List<Quest> AcceptedQuests { get; private set; } = new();
    public List<Quest> CompletedQuests { get; private set; } = new();
    public List<Quest> AllQuests { get; private set; } = new();
    public List<Quest> TodayAvailableQuest { get; private set; } = new();

    public void Init()
    {
        Debug.Log("퀘스트 인스턴스 생성");
        foreach (Data_Quest item in DataManager.Instance.DataLoader_Quest.ItemsList)
        {
            AllQuests.Add(new Quest(item));
        }

        QuestManager.Instance.OnNewDayStarted += HandleNewDay;
    }

    // 매일 QuestData가 할일
    public void HandleNewDay()
    {
        // 2.가능한 퀘스트 리스트 받아오기
        Debug.Log("매일 가능한 퀘스트 리스트 받아옴");
        TodayAvailableQuest = new();
        foreach (Quest item in AllQuests)
        {
            if (item.CheckAvailable(DateTime.Now)) // 날짜 임시로 아무거나 넣어놓음.
            {
                TodayAvailableQuest.Add(item);
            }
        }
    }

    public void AcceptQuest(Quest quest)
    {
        AcceptedQuests.Add(quest);
    }

    public void RemoveQuest(Quest quest)
    {
        AcceptedQuests.Remove(quest);
    }

    public void CompleteQuest(Quest quest)
    {
        AcceptedQuests.Remove(quest);
        CompletedQuests.Add(quest);
    }
}

public class QuestManager : MonoSingleton<QuestManager>
{
    public QuestData questData;
    public Action OnNewDayStarted;

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
