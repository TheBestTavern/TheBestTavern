using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class QuestManager : MonoSingleton<QuestManager>
{
    public QuestData questData;
    [HideInInspector] public MailBoxContentOffer mailBoxContentQuest;
    [HideInInspector] public MailBoxContentResult mailBoxContentCompensation;
    public Action onNewDayAction;

    public Dictionary<int, Quest> AllQuests => questData.AllQuests;
    public List<int> AcceptedQuests => questData.AcceptedQuests;
    public List<int> OnceCompletedQuests => questData.OnceCompletedQuests;
    public List<int> JustCompleteQuests => questData.JustCompleteQuests;
    public List<int> TodayAvailableQuest => questData.TodayAvailableQuest;

    public Queue<(int questID, int itemID)> questCheckQueue;


    public override void Init()
    {
        if (_isInitialized) return;
        base.Init();

        isDontDestroyOnLoad = true;
        questData = new QuestData();
        questData.Init();
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

    // 퀘스트 완료
    public bool TryCompleteQuest(int questID, Item item)
    {
        if (CheckSuccessQuest(questID, item))
        {
            // 퀘스트 성공
            questData.CompleteQuest(questID);
            return true;
        }
        else
        {
            // 퀘스트 실패
            return false;
        }
    }

    public bool CheckSuccessQuest(int questID, Item item)
    {
        return true; //// todo - 퀘스트 성공 여부 검사
    }

    public void AbortQuest(int questID)
    {
        questData.RemoveQuest(questID);
        Data.GetQuest(questID).AbortQuest(TimerManager.Instance.GetToday()); // 아무 날짜나 임시로 지정 => 오늘 날짜로 변경
    }

    // 하루가 갱신될때마다 실행될 이벤트 실행 메서드.
    public void TriggerNewDay() => onNewDayAction?.Invoke();
}
