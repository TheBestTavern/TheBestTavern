using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QuestData
{
    public Dictionary<int, Quest> AllQuests { get; private set; } = new(); // 모든 퀘스트
    public List<int> AcceptedQuests { get; private set; } = new(); // 진행중인 퀘스트
    public List<int> OnceCompletedQuests { get; private set; } = new(); // 한번이라도 클리어해본 퀘스트
    public List<int> JustCompleteQuests { get; private set; } = new(); // 오늘 클리어한 퀘스트 (내일 보상 편지 생성에 사용)
    public List<int> TodayAvailableQuest { get; private set; } = new(); // 오늘의 퀘스트

    public Action<int> onTriggerNPC;
    public Action onSpawnNPC;

    public void Init()
    {
        Debug.Log("퀘스트 인스턴스 생성");
        Quest quest;
        foreach (Data_Quest item in DataManager.Instance.DataLoader_Quest.ItemsList)
        {
            quest = new Quest(item);
            AllQuests.Add(quest.origin.key, quest);
        }

        QuestManager.Instance.onNewDayAction += HandleNewDay;
    }

    // 매일 QuestData가 할일
    public void HandleNewDay()
    {
        //1.진행중 퀘스트 상태 확인(당일 - NPC방문, 아직 - 무, 지남 - 퀘스트 실패 처리) 
        for (int i = 0; i < AcceptedQuests.Count; i++)
        {
            Quest tempQuest = Data.GetQuest(AcceptedQuests[i]);
            if (tempQuest.TriggerDate > TimerManager.Instance.GetToday())
            {
                //아직
            }
            else if (tempQuest.TriggerDate == TimerManager.Instance.GetToday())
            {
                //당일
                // NPC 소환.
                onTriggerNPC?.Invoke(AcceptedQuests[i]);
                Debug.Log($"{tempQuest.origin.name}퀘스트의 NPC 소환");

                if (i + 1 == AcceptedQuests.Count)
                {
                    onSpawnNPC?.Invoke();
                }
            }
            else
            {
                //지남
                // to do - 퀘스트 실패 처리.
                Debug.Log("기한 초과로 인한 퀘스트 실패");
            }
        }

        //2.가능한 퀘스트 리스트 받아오기
        Debug.Log("매일 가능한 퀘스트 리스트 받아옴");
        if (TodayAvailableQuest == null)
        {
            TodayAvailableQuest = new();
        }
        else
        {
            TodayAvailableQuest.Clear();
        }

        foreach (var item in AllQuests)
        {
            if (item.Value.CheckAvailable()) 
            {
                TodayAvailableQuest.Add(item.Key);
            }
        }
    }

    public void AcceptQuest(int questID)
    {
        AcceptedQuests.Add(questID);
    }

    public void RemoveQuest(int questID)
    {
        AcceptedQuests.Remove(questID);
    }

    public void CompleteQuest(int questID)
    {
        AcceptedQuests.Remove(questID);
        Data.GetQuest(questID).CompleteQuest(TimerManager.Instance.GetToday()); // 아무 날짜나 임시로 지정 => 오늘 날짜로 변경
        OnceCompletedQuests.Add(questID);
        JustCompleteQuests.Add(questID);
    }
}
