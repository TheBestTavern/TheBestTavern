using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QuestManager : MonoSingleton<QuestManager>
{
    public QuestData questData;
    public Action OnNewDayStarted;

    public List<Quest> AllQuests => questData.AllQuests;
    public List<Quest> AcceptedQuests => questData.AcceptedQuests;
    public List<Quest> CompletedQuests => questData.CompletedQuests;
    public List<Quest> TodayAvailableQuest => questData.TodayAvailableQuest;

    private void Start()
    {
        isDontDestroyOnLoad = true;
        questData = new QuestData();
        questData.Init();
        TriggerNewDay();  // 테스트용
    }


    // 퀘스트 수령
    public void AcceptQuest(Quest quest, int days)
    {
        if (AcceptedQuests.Count <= 5)
        {
            questData.AcceptQuest(quest); // 리스트에 넣기
            quest.AcceptQuest(new LunarDateTime(), days); // 퀘스트 수락 상태로 전환 및 트리거
        }
        else
        {
            Debug.Log("퀘스트 갯수 제한(5개) 초과");
            UIManager.Instance.ShowPopUp(PopUpType.Alarm);
            UIManager.Instance.alarmPopUp.SetAlarm("퀘스트 갯수 제한 초과");
        }
    }

    // 퀘스트 완료
    public void CompleteQuest(Quest quest)
    {
        questData.CompleteQuest(quest);
        quest.CompleteQuest(new LunarDateTime()); // 아무 날짜나 임시로 지정 => 오늘 날짜로 변경
    }

    public void AbortQuest(Quest quest)
    {
        questData.RemoveQuest(quest);
        quest.AbortQuest(new LunarDateTime()); // 아무 날짜나 임시로 지정 => 오늘 날짜로 변경
    }

    // 하루가 갱신될때마다 실행될 이벤트 실행 메서드.
    public void TriggerNewDay()
    {
        OnNewDayStarted?.Invoke();
    }
}
