using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class QuestManager : MonoSingleton<QuestManager>
{
    public QuestData questData;

    public Dictionary<int, Quest> AllQuests => questData.AllQuests;
    public List<int> AcceptedQuests => questData.AcceptedQuests;
    public Dictionary<int, SuccessDegree> OnceCompletedQuests => questData.OnceCompletedQuests;
    public List<int> JustCompleteQuests => questData.JustCompleteQuests;
    public List<int> TodayAvailableQuest => questData.TodayAvailableQuest;

    public Queue<(int questID, int itemID)> QuestCheckQueue => questData.QuestCheckQueue;


    public override void Init()
    {
        if (_isInitialized) return;
        base.Init();

        DontDestroyOnLoad(gameObject);
        questData = new QuestData();
        questData.Init();

        // 커맨드 등록
        OnNewDay command = new(this);
        DayManager.Instance.AddCommand(command);
    }

    // 퀘스트 수령 조건 판단
    public async Task<bool> TryAcceptQuest(int questID, int days)
    {
        Quest tempQuest = Data.GetQuest(questID);
        if (AcceptedQuests.Count >= 5)
        {
            Debug.Log("퀘스트 갯수 제한(5개) 초과");
            await UIManager.Instance.ShowPopUp(PopUpType.Alarm);
            UIManager.Instance.alarmPopUp.SetAlarm("퀘스트 갯수 제한을 초과했습니다.");
            return false;
        }
        else if (tempQuest.IsAccepted)
        {
            Debug.Log("이미 수락한 퀘스트임");
            await UIManager.Instance.ShowPopUp(PopUpType.Alarm);
            UIManager.Instance.alarmPopUp.SetAlarm("이미 수락한 퀘스트입니다.");
            return false;
        }
        else if (NPCManager.Instance.AllNPC[tempQuest.origin.givingNPC].isGivingQuest)
        {
            Debug.Log("이미 퀘스트를 준 npc입니다.");
            await UIManager.Instance.ShowPopUp(PopUpType.Alarm);
            UIManager.Instance.alarmPopUp.SetAlarm("이미 퀘스트를 준 npc입니다.");
            return false;
        }


        questData.AcceptQuest(questID); // 리스트에 넣기
        tempQuest.AcceptQuest(TimerManager.Instance.GetToday(), days); // 퀘스트 수락 상태로 전환 및 트리거
        NPCManager.Instance.AllNPC[tempQuest.origin.givingNPC].GiveQuest(); // npc 퀘스트 준 상태로 전환
        TodayAvailableQuest.Remove(questID); //오늘의 퀘스트 리스트에서 삭제
        return true;

    }

    public class OnNewDay : IDayCommand
    {
        QuestManager prt;

        public OnNewDay(QuestManager questManager)
        {
            this.prt = questManager;
        }

        public int Priority => 200;

        public void Execute()
        {
            prt.JustCompleteQuests.Clear();
            CheckQuestCheckQueue();
        }

        void CheckQuestCheckQueue()
        {
            while (prt.QuestCheckQueue.Count > 0)
            {
                var pair = prt.QuestCheckQueue.Dequeue();
                var quest = Data.GetQuest(pair.questID);

                // 데이터 테이블에 아직 들여오지 않은 변수.
                if (quest.origin.goodFood.Contains(pair.itemID))
                {
                    Debug.Log("대성공");
                    prt.questData.CompleteQuest(pair.questID, SuccessDegree.good);
                }
                else if (quest.origin.sosoFood.Contains(pair.itemID))
                {
                    Debug.Log("중성공");
                    prt.questData.CompleteQuest(pair.questID, SuccessDegree.soso);
                }
                else if (quest.origin.notBadFood.Contains(pair.itemID))
                {
                    Debug.Log("소성공");
                    prt.questData.CompleteQuest(pair.questID, SuccessDegree.bad);
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
            Debug.Log("퀘스트 대기열 체크");
        }

        public bool isValid()
        {
            return prt != null;
        }
    }
}
