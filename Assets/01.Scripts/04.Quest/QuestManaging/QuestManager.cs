using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QuestManager : MonoSingleton<QuestManager>
{
    public QuestData questData;
    [HideInInspector] public MailBoxContentOffer mailBoxContentQuest;
    [HideInInspector] public MailBoxContentResult mailBoxContentCompensation;
    public Action onNewDayAction;

    public List<Quest> AllQuests => questData.AllQuests;
    public List<Quest> AcceptedQuests => questData.AcceptedQuests;
    public List<Quest> OnceCompletedQuests => questData.OnceCompletedQuests;
    public List<Quest> TodayAvailableQuest => questData.TodayAvailableQuest;

    public override void Init()
    {
        if (_isInitialized) return;
        base.Init();

        isDontDestroyOnLoad = true;
        questData = new QuestData();
        questData.Init();
        GameManager.Instance.onNewDayAction += TriggerNewDay;
    }

    // 퀘스트 수령 조건 판단
    public bool TryAcceptQuest(Quest quest, int days)
    {
        if (AcceptedQuests.Count < 5 && !quest.IsAccepted)
        {
            questData.AcceptQuest(quest); // 리스트에 넣기
            quest.AcceptQuest(TimerManager.Instance.GetToday(), days); // 퀘스트 수락 상태로 전환 및 트리거

            //오늘의 퀘스트 리스트에서 삭제
            TodayAvailableQuest.Remove(quest);

            //퀘스트 슬롯 리스트에서 삭제, 슬롯 파괴
            //mailBoxContentQuest.RemoveQuestSlot(questSlot);

            return true;
        }
        else if (AcceptedQuests.Count >= 5)
        {
            Debug.Log("퀘스트 갯수 제한(5개) 초과");
            UIManager.Instance.ShowPopUp(PopUpType.Alarm);
            UIManager.Instance.alarmPopUp.SetAlarm("퀘스트 갯수 제한을 초과했습니다.");
            return false;
        }
        else
        {
            Debug.Log("이미 수락한 퀘스트임");
            UIManager.Instance.ShowPopUp(PopUpType.Alarm);
            UIManager.Instance.alarmPopUp.SetAlarm("이미 수락한 퀘스트입니다.");
            return false;
        }
    }

    // 퀘스트 완료
    public bool TryCompleteQuest(Quest quest)
    {
        if (true) // 퀘스트 성공 여부 검사
        {
            // 퀘스트 성공
            questData.CompleteQuest(quest);
            return true;
        }
        else
        {
            // 퀘스트 실패
            return false;
        }
    }

    public void AbortQuest(Quest quest)
    {
        questData.RemoveQuest(quest);
        quest.AbortQuest(TimerManager.Instance.GetToday()); // 아무 날짜나 임시로 지정 => 오늘 날짜로 변경
    }

    // 하루가 갱신될때마다 실행될 이벤트 실행 메서드.
    public void TriggerNewDay()
    {
        onNewDayAction?.Invoke();
        Debug.Log("퀘스트 파트 뉴데이 이벤트 실행");
    }
}
