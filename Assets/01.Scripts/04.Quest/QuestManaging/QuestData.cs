using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QuestData
{
    public List<Quest> AcceptedQuests { get; private set; } = new(); // 진행중인 퀘스트
    public List<Quest> OnceCompletedQuests { get; private set; } = new(); // 한번이라도 클리어해본 퀘스트
    public List<Quest> AllQuests { get; private set; } = new(); // 모든 퀘스트
    public List<Quest> TodayAvailableQuest { get; private set; } = new(); // 오늘의 퀘스트
    public List<Quest> JustCompleteQuests { get; private set; } = new(); // 오늘 클리어한 퀘스트 (내일 보상 편지 생성에 사용)

    public void Init()
    {
        Debug.Log("퀘스트 인스턴스 생성");
        foreach (Data_Quest item in DataManager.Instance.DataLoader_Quest.ItemsList)
        {
            AllQuests.Add(new Quest(item));
        }

        QuestManager.Instance.onNewDayAction += HandleNewDay;
    }

    // 매일 QuestData가 할일
    public void HandleNewDay()
    {
        //1.진행중 퀘스트 상태 확인(당일 - NPC방문, 아직 - 무, 지남 - 퀘스트 실패 처리) 
        for (int i = 0; i < AcceptedQuests.Count; i++)
        {
            if (AcceptedQuests[i].TriggerDate > TimerManager.Instance.GetToday())
            {
                //아직
            }
            else if (AcceptedQuests[i].TriggerDate == TimerManager.Instance.GetToday())
            {
                //당일
                // NPC 소환.
                Debug.Log($"{AcceptedQuests[i].origin.name}퀘스트의 NPC 소환");
            }
            else
            {
                //지남
                // 퀘스트 실패 처리.
                Debug.Log("기한 초과로 인한 퀘스트 실패");
            }
        }

        //2.가능한 퀘스트 리스트 받아오기
        Debug.Log("매일 가능한 퀘스트 리스트 받아옴");
        TodayAvailableQuest = new();
        foreach (Quest item in AllQuests)
        {
            if (item.CheckAvailable(TimerManager.Instance.GetToday())) // 날짜 임시로 아무거나 넣어놓음.
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
        quest.CompleteQuest(TimerManager.Instance.GetToday()); // 아무 날짜나 임시로 지정 => 오늘 날짜로 변경
        OnceCompletedQuests.Add(quest);
        JustCompleteQuests.Add(quest);
    }
}
