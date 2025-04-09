using System;
using System.Collections;
using System.Collections.Generic;
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
        //퀘스트 인스턴스 생성
        Debug.Log("퀘스트 인스턴스 생성");
        AllQuests = new()
        {
            new Quest(1, "1번퀘", "흥부가 배가 고파", "흥부"),
            new Quest(2, "2번퀘", "놀부가 심술이 나", "놀부"),
            new Quest(3, "3번퀘", "심청이가 집을 나가", "심봉"),
            new Quest(4, "4번퀘", "산신령이 갑자기", "산신령"),
            new Quest(5, "5번퀘", "호랑이가 하늘에서", "호랑이"),
            new Quest(6, "6번퀘", "까치가 박씨를", "까치"),
            new Quest(7, "7번퀘", "그물타고 영차", "그물"),
            new Quest(8, "8번퀘", "거미가 수십마리가", "거미")
        };

        QuestManager.Instace.OnNewDayStarted += HandleNewDay;
    }

    public void HandleNewDay()
    {
        Debug.Log("매일 가능한 퀘스트 리스트 받아옴");
        // 1.가능한 퀘스트 리스트 받아오기
        TodayAvailableQuest = QuestManager.Instace.questData.AllQuests;
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
        questData = new QuestData();
        questData.Init();
        TriggerNewDay();
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
