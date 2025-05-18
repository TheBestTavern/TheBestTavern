using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public enum SuccessDegree
{
    good = 20,
    soso = 10,
    bad = 0
}

public class QuestData
{
    public Dictionary<int, Quest> AllQuests { get; private set; } = new(); // 모든 퀘스트
    public List<int> AcceptedQuests { get; private set; } = new(); // 진행중인 퀘스트
    public Dictionary<int, SuccessDegree> OnceCompletedQuests { get; private set; } = new(); // 한번이라도 클리어해본 퀘스트. 최대 성공 정도
    public List<int> JustCompleteQuests { get; private set; } = new(); // 오늘 클리어한 퀘스트 (내일 보상 편지 생성에 사용)
    public List<int> TodayAvailableQuest { get; private set; } = new(); // 오늘의 퀘스트
    public Queue<(int questID, int itemID)> QuestCheckQueue { get; private set; } = new(); // 아이템 제출한 퀘스트 목록.

    public Action<List<int>> onTriggerNPC; // npc 소환
    public Action onSpawnNPC; // 소환된 npc 정렬

    public void Init()
    {
        Debug.Log("퀘스트 인스턴스 생성");
        Quest quest;
        foreach (Data_Quest item in DataManager.Instance.DataLoader_Quest.ItemsList)
        {
            quest = new Quest(item);
            AllQuests.Add(quest.origin.key, quest);
        }

        // 커맨드 등록
        OnNewDay command = new(this);
        CommandManager.Instance.AddCommand(command);
    }

    public void AcceptQuest(int questID)
    {
        AcceptedQuests.Add(questID);
    }

    public void FailQuest(int questID)
    {
        AcceptedQuests.Remove(questID);
        Data.GetQuest(questID).FailQuest(TimerManager.Instance.GetToday());
    }

    public void CompleteQuest(int questID, SuccessDegree successDegree) // 퀘스트 완료
    {
        AcceptedQuests.Remove(questID);
        Data.GetQuest(questID).CompleteQuest(TimerManager.Instance.GetToday());
        JustCompleteQuests.Add(questID);

        if (!OnceCompletedQuests.TryGetValue(questID, out var prev) || prev < successDegree)
        {
            OnceCompletedQuests[questID] = successDegree;
            // 읽기작업: 없는 키값에 접근할 경우, KeyNotFoundException 오류가 발생 dic[index];
            // 쓰기작업: 없는 키값에 접근할 경우, 새로운 키-값 쌍을 추가함. dic[index] = value;
        }
    }

    // 매일 할일
    public class OnNewDay : IDayCommand
    {
        QuestData prt;
        public OnNewDay(QuestData questData)
        {
            this.prt = questData;
        }

        public int Priority => 1500;

        public Task Execute()
        {
            CheckAcceptedQuests();
            TakeTodayAvailableQuest();

            return Task.CompletedTask;
        }

        public void CheckAcceptedQuests()
        {
            //진행중 퀘스트 상태 확인(당일 - NPC방문, 아직 - 무, 지남 - 퀘스트 실패 처리) 
            List<int> spawnNPCs = new();
            for (int i = 0; i < prt.AcceptedQuests.Count; i++)
            {
                int key = prt.AcceptedQuests[i];
                Quest tempQuest = Data.GetQuest(key);
                if (tempQuest.TriggerDate > TimerManager.Instance.GetToday())
                {
                    //아직
                }
                else if (tempQuest.TriggerDate == TimerManager.Instance.GetToday())
                {
                    //당일
                    // 소환할 NPC 목록 구성
                    spawnNPCs.Add(key);
                    Debug.Log($"{tempQuest.origin.name}퀘스트의 NPC를 소환 목록에 등록");
                }
                else
                {
                    //지남
                    // to do - 퀘스트 실패 처리.
                    prt.FailQuest(key);
                    Debug.Log("기한 초과로 인한 퀘스트 실패");
                }

            }
            // 소환할 npc 있다면, 
            if (spawnNPCs.Count > 0)
            {
                prt.onTriggerNPC?.Invoke(spawnNPCs);
                prt.onSpawnNPC?.Invoke();
                spawnNPCs.Clear();
            }
            Debug.Log("진행중 퀘스트 체크");

        }

        public void TakeTodayAvailableQuest()
        {
            //가능한 퀘스트 리스트 받아오기
            if (prt.TodayAvailableQuest == null)
            {
                prt.TodayAvailableQuest = new();
            }
            else
            {
                prt.TodayAvailableQuest.Clear();
            }

            foreach (var item in prt.AllQuests)
            {
                if (item.Value.CheckAvailable())
                {
                    prt.TodayAvailableQuest.Add(item.Key);
                }
            }
            Debug.Log("오늘의 퀘스트 받아오기");

        }

        public bool isValid()
        {
            return prt != null;
        }
    }

}
