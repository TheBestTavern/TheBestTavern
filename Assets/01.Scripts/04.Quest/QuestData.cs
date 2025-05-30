using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Analytics;
using UnityEngine.Analytics;
using Unity.VisualScripting;
using UnityEngine;
//using static UnityEditor.Searcher.Searcher;

public enum SuccessDegree
{
    good = 30,
    soso = 20,
    notBad = 10,
    none = 0,
    fail = -10,
}

public class QuestData
{
    public Dictionary<int, Quest> AllQuests { get; private set; } = new(); // 모든 퀘스트
    public List<int> AcceptedQuests { get; private set; } = new(); // 진행중인 퀘스트
    public Dictionary<int, SuccessDegree> OnceSuccessQuests { get; private set; } = new(); // 한번이라도 클리어해본 퀘스트. 최대 성공 정도
    public List<(int questID, SuccessDegree successDegree)> JustCompleteQuests { get; private set; } = new(); // 오늘 클리어한 퀘스트 (내일 보상 편지 생성에 사용) => 클리어한 퀘스트 목록<순서, (퀘스트id, 결과 수준)> 의뢰함 열때마다 새로이 생성
    public List<int> TodayAvailableQuest { get; private set; } = new(); // 오늘의 퀘스트
    public List<(int questID, int itemID)> QuestCheckQueueForSerialization = new(); // 저장시 사용
    public Queue<(int questID, int itemID)> QuestCheckQueue { get; private set; } = new(); // 아이템 제출한 퀘스트 목록.
    public List<int> TodaySpawnNPC { get; private set; } = new(); // 오늘 찾아올 NPC

    public QuestContainer questSO { get; private set; }
    Dictionary<SuccessDegree, int> favorMap;

    private Dictionary<int, NPC> allNPC;

    public Action<List<int>> onTriggerNPC; // npc 소환
    public Action onSpawnNPC; // 소환된 npc 정렬

    public void Init()
    {
        //Debug.Log("퀘스트 인스턴스 생성");
        Quest quest;
        foreach (Data_Quest item in DataManager.Instance.DataLoader_Quest.ItemsList)
        {
            quest = new Quest(item);
            AllQuests.Add(quest.Origin.key, quest);
        }

        allNPC = NPCManager.Instance.NPCData.AllNPC;

        questSO = Resources.Load<QuestContainer>("QuestContainer");
        favorMap = new()
        {
            { SuccessDegree.good , questSO.goodQuest},
            { SuccessDegree.soso, questSO.sosoQuest},
            { SuccessDegree.notBad , questSO.notBadQuest},
            { SuccessDegree.fail , questSO.failQuest}
        };

        // 커맨드 등록
        OnNewDay command = new(this);
        CommandManager.Instance.AddCommand(command);
    }

    public void ApplyLoadData(Dictionary<int, Quest> AllQuests, List<int> AcceptedQuests,
        Dictionary<int, SuccessDegree> OnceSuccessQuests, List<(int, SuccessDegree)> JustCompleteQuests, List<int> TodayAvailableQuest,
        List<(int questID, int itemID)> QuestCheckQueue, List<int> TodaySpawnNPC)
    {
        this.AllQuests = AllQuests;
        this.AcceptedQuests = AcceptedQuests;
        this.OnceSuccessQuests = OnceSuccessQuests;
        this.JustCompleteQuests = JustCompleteQuests;
        this.TodayAvailableQuest = TodayAvailableQuest;
        this.QuestCheckQueue = new (QuestCheckQueue);
        this.TodaySpawnNPC = TodaySpawnNPC;
    }

    public void AcceptQuest(int questID)
    {
        AcceptedQuests.Add(questID);
        EventBus.Publish<QuestAcceptEvent>(new QuestAcceptEvent());
    }

    public void FailQuest(int questID)
    {
        AcceptedQuests.Remove(questID);
        Quest tempQuest = Data.GetQuest(questID);
        tempQuest.FailQuest(TimerManager.Instance.GetToday());
        allNPC[tempQuest.Origin.givingNPC].FailQuest(favorMap[SuccessDegree.fail]);
        JustCompleteQuests.Add((questID, SuccessDegree.fail));
        EventBus.Publish<QuestCompleteEvent>(new QuestCompleteEvent());
    }

    public void SuccessQuest(int questID, SuccessDegree successDegree) // 퀘스트 완료
    {
        Quest tempQuest = Data.GetQuest(questID);

        AcceptedQuests.Remove(questID);
        tempQuest.SuccessQuest(TimerManager.Instance.GetToday(), successDegree);
        JustCompleteQuests.Add((questID, successDegree));

        allNPC[tempQuest.Origin.givingNPC].SuccessQuest(favorMap[successDegree]);
        EventBus.Publish<QuestCompleteEvent>(new QuestCompleteEvent());

        if (!OnceSuccessQuests.TryGetValue(questID, out var prev) || prev < successDegree)
        {
            OnceSuccessQuests[questID] = successDegree;
            EventBus.Publish<QuestSuccessFirstEvent>(new QuestSuccessFirstEvent());
        }

        Dictionary<string, object> eventData = new();
        eventData.Add("완료한 퀘스트 이름", tempQuest.Origin.name);

        if (GameManager.Instance.isAnalyticsAgreed)
        {
            // questID 이름 어떤 퀘스트 했는지 이벤트 보내기
            var ItemEvent = new AnalyticsQuest("QuestData")
            {
                questName = tempQuest.Origin.name
            };
            AnalyticsService.Instance.RecordEvent(ItemEvent);
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
            prt.TodaySpawnNPC.Clear();
            List<int> AcceptedQuestsClone =  new();
            foreach(var questKey in prt.AcceptedQuests)
            {
                AcceptedQuestsClone.Add(questKey);
            }

            for (int i = 0; i < AcceptedQuestsClone.Count; i++)
            {
                int key = AcceptedQuestsClone[i];
                Quest tempQuest = Data.GetQuest(key);
                if (tempQuest.TriggerDate > TimerManager.Instance.GetToday())
                {
                    //아직
                }
                else if (tempQuest.TriggerDate == TimerManager.Instance.GetToday())
                {
                    //당일
                    // 소환할 NPC 목록 구성
                    prt.TodaySpawnNPC.Add(key);
                    Debug.Log($"{tempQuest.Origin.name}퀘스트의 NPC를 소환 목록에 등록");
                }
                else
                {
                    //지남
                    // to do - 퀘스트 실패 처리.
                    prt.FailQuest(key);
                    Debug.Log("기한 초과로 인한 퀘스트 실패");
                }
            }

            AcceptedQuestsClone.Clear();
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
            //Debug.Log("오늘의 퀘스트 받아오기");

        }

        public bool isValid()
        {
            return prt != null;
        }
    }

}
