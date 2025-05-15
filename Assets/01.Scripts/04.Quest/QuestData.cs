using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public enum SuccessDegree
{
    good = 20,
    soso = 10,
    notBad = 0
}

public class QuestData
{
    public Dictionary<int, Quest> AllQuests { get; private set; } = new(); // 모든 퀘스트
    public List<int> AcceptedQuests { get; private set; } = new(); // 진행중인 퀘스트
    public Dictionary<int, SuccessDegree> OnceCompletedQuests { get; private set; } = new(); // 한번이라도 클리어해본 퀘스트. 최대 성공 정도
    public List<int> JustCompleteQuests { get; private set; } = new(); // 오늘 클리어한 퀘스트 (내일 보상 편지 생성에 사용)
    public List<int> TodayAvailableQuest { get; private set; } = new(); // 오늘의 퀘스트
    public Queue<(int questID, int itemID)> QuestCheckQueue { get; private set; } = new(); // 아이템 제출한 퀘스트 목록.

    Dictionary<SuccessDegree, int> favorMap;

    public QuestContainer questSO { get; private set; }
    private InventoryController playerInvenController;
    private Dictionary<int, NPC> allNPC;

    public Action<List<int>> onTriggerNPC; // npc 소환
    public Action onSpawnNPC; // 소환된 npc 정렬

    public async void Init()
    {
        //Debug.Log("퀘스트 인스턴스 생성");
        Quest quest;
        foreach (Data_Quest item in DataManager.Instance.DataLoader_Quest.ItemsList)
        {
            quest = new Quest(item);
            AllQuests.Add(quest.origin.key, quest);
        }

        // 참조 할당
        questSO = await AddressablesLoader.Instance.AddressablesLoadAsync<QuestContainer>("QuestContainer.SO");
        favorMap = new()
        {
            { SuccessDegree.good , questSO.goodQuest},
            { SuccessDegree.soso, questSO.sosoQuest},
            { SuccessDegree.notBad , questSO.notBadQuest},
        };
        playerInvenController = InventoryManager.Instance.Invens[InvenType.Player];
        allNPC = NPCManager.Instance.AllNPC;

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
        Quest tempQuest = Data.GetQuest(questID);
        tempQuest.FailQuest(TimerManager.Instance.GetToday());
        allNPC[tempQuest.origin.givingNPC].FailQuest();
    }

    public void CompleteQuest(int questID, SuccessDegree successDegree) // 퀘스트 완료
    {
        Quest tempQuest = Data.GetQuest(questID);

        AcceptedQuests.Remove(questID);
        tempQuest.CompleteQuest(TimerManager.Instance.GetToday());
        JustCompleteQuests.Add(questID);

        playerInvenController.아이템획득(tempQuest.origin.compensationID, 1);
        allNPC[tempQuest.origin.givingNPC].SuccessQuest(favorMap[successDegree]);

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
            //Debug.Log("진행중 퀘스트 체크");

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
