using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestData
{
    private List<Quest> _acceptedQuests = new();
    private List<Quest> _completedQuests = new();

    public IReadOnlyList<Quest> acceptedQuestsList => _acceptedQuests;
    public IReadOnlyList<Quest> completedQuestsList => _completedQuests;

    public void Init()
    {
        //퀘스트 인스턴스 생성
        Debug.Log("퀘스트 인스턴스 생성");
    }

    public void AcceptQuest(Quest quest)
    {
        _acceptedQuests.Add(quest);
    }

    public void RemoveQuest(Quest quest)
    {
        _acceptedQuests.Remove(quest);
    }

    public void CompleteQuest(Quest quest)
    {
        _acceptedQuests.Remove(quest);
        _completedQuests.Add(quest);
    }
}

public class QuestManager : MonoSingleton<QuestManager>
{
    QuestData questData;
    public Action newDayAction;

    void Start()
    {
        questData = new QuestData();
        questData.Init();
        newDayAction += OnNewDayAction;
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

    // 하루가 갱신될때마다 실행될 퀘스트 관련 할일.
    public void OnNewDayAction()
    {
        newDayAction?.Invoke();
    }
}
