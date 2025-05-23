using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;

public class QuestManager : MonoSingleton<QuestManager>
{
    public QuestData questData;

    public Dictionary<int, Quest> AllQuests { get { return questData.AllQuests; } }
    public List<int> AcceptedQuests { get { return questData.AcceptedQuests; } }
    public Dictionary<int, SuccessDegree> OnceSuccessQuests { get { return questData.OnceSuccessQuests; } }
    public List<(int, SuccessDegree)> JustCompleteQuests { get { return questData.JustCompleteQuests; } }
    public List<int> TodayAvailableQuest { get { return questData.TodayAvailableQuest; } }

    public Queue<(int questID, int itemID)> QuestCheckQueue { get { return questData.QuestCheckQueue; } }


    public override void Init()
    {
        if (_isInitialized) return;
        base.Init();

        DontDestroyOnLoad(gameObject);
        questData = new QuestData();
        questData.Init();

        // 커맨드 등록
        OnNewDay command = new(this);
        CommandManager.Instance.AddCommand(command);
    }

    private void Start()
    {
        questData.LateInit();
    }

    // 퀘스트 수령 조건 판단
    public async Task<bool> TryAcceptQuest(int questID, int days)
    {
        Quest tempQuest = Data.GetQuest(questID);
        if (AcceptedQuests.Count >= 5)
        {
            Debug.Log("퀘스트 갯수 제한(5개) 초과");
            await PopUpManager.Instance.ShowPopUp(PopUpType.Alarm);
            PopUpManager.Instance.alarmPopUp.SetAlarm("퀘스트 갯수 제한을 초과했습니다.");
            return false;
        }
        else if (tempQuest.IsAccepted)
        {
            Debug.Log("이미 수락한 퀘스트임");
            await PopUpManager.Instance.ShowPopUp(PopUpType.Alarm);
            PopUpManager.Instance.alarmPopUp.SetAlarm("이미 수락한 퀘스트입니다.");
            return false;
        }
        else if (NPCManager.Instance.AllNPC[tempQuest.Origin.givingNPC].isGivingQuest)
        {
            Debug.Log("이미 퀘스트를 준 npc입니다.");
            await PopUpManager.Instance.ShowPopUp(PopUpType.Alarm);
            PopUpManager.Instance.alarmPopUp.SetAlarm("이미 퀘스트를 준 npc입니다.");
            return false;
        }


        questData.AcceptQuest(questID); // 리스트에 넣기
        tempQuest.AcceptQuest(TimerManager.Instance.GetToday(), days); // 퀘스트 수락 상태로 전환 및 트리거
        NPCManager.Instance.AllNPC[tempQuest.Origin.givingNPC].GiveQuest(); // npc 퀘스트 준 상태로 전환
        TodayAvailableQuest.Remove(questID); //오늘의 퀘스트 리스트에서 삭제
        return true;
    }

    public void SubmissionComplete(int npcID)
    {
        questData.TodaySpawnNPC.Remove(npcID);
    }

    public class OnNewDay : IDayCommand
    {
        QuestManager prt;

        public OnNewDay(QuestManager questManager)
        {
            this.prt = questManager;
        }

        public int Priority => 200;

        public Task Execute()
        {
            //prt.JustCompleteQuests.Clear();
            CheckQuestCheckQueue();

            return Task.CompletedTask;
        }

        void CheckQuestCheckQueue()
        {
            while (prt.QuestCheckQueue.Count > 0)
            {
                var pair = prt.QuestCheckQueue.Dequeue();
                var quest = Data.GetQuest(pair.questID);

                // 데이터 테이블에 아직 들여오지 않은 변수.
                if(pair.itemID == 88888888)
                {
                    Debug.Log("대성공");
                    prt.questData.SuccessQuest(pair.questID, SuccessDegree.good);
                }
                else if (quest.Origin.goodFood.Contains(pair.itemID))
                {
                    Debug.Log("대성공");
                    prt.questData.SuccessQuest(pair.questID, SuccessDegree.good);
                }
                else if (quest.Origin.sosoFood.Contains(pair.itemID))
                {
                    Debug.Log("중성공");
                    prt.questData.SuccessQuest(pair.questID, SuccessDegree.soso);
                }
                else if (quest.Origin.notBadFood.Contains(pair.itemID))
                {
                    Debug.Log("소성공");
                    prt.questData.SuccessQuest(pair.questID, SuccessDegree.notBad);
                }
                else
                {
                    Debug.Log("실패");
                    prt.questData.FailQuest(pair.questID);
                }

                ///*  테스트용 */
                //Debug.Log("Dev-퀘스트 중성공");
                //prt.questData.CompleteQuest(pair.questID, SuccessDegree.soso);
                ///*  테스트용 */

            }
            //Debug.Log("퀘스트 대기열 체크");
        }

        public bool isValid()
        {
            return prt != null;
        }
    }
}
