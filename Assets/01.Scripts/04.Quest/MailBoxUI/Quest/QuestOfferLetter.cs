using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestOfferLetter : QuestBaseLetter
{
    private int days;
    bool isSetDays;

    // 편지 생성시 한번 초기화
    public override void FirstInit(Quest quest, Action<QuestBaseSlot> action)
    {
        if (IsReady) return;

        base.FirstInit(quest, action);

        // 버튼 초기화
        buttons[0].onClick.AddListener(() => OnClickDays(7));
        buttons[1].onClick.AddListener(() => OnClickDays(11));
        buttons[2].onClick.AddListener(() => OnClickDays(14));
        buttons[3].onClick.AddListener(() => OnOKButton());
        buttons[4].onClick.AddListener(() => RejectQuest());
        IsReady = true;
    }

    // 편지 열때마다 필요한 초기화.
    public override void EveryInit(Quest quest, QuestBaseSlot baseQuestSlot)
    {
        // 문구 초기화
        base.EveryInit(quest, baseQuestSlot);
        isSetDays = false;
        days = 0;
        bodyText.text = quest.origin.description;
    }

    private void OnClickDays(int day)
    {
        days = day;
        isSetDays = true;
    }

    // 수락 버튼 메서드
    protected override void OnOKButton()
    {
        base.OnOKButton();
        if (AcceptQuest())
        {
            TriggerOnCompleteLetter(); // 편지 읽고 퀘스트수락/보상수령 시 슬롯 파괴 이벤트 실행
            OnClickCloseButton(); //  퀘스트수락/보상수령 시 편지 닫기
        }
    }

    private bool AcceptQuest()
    {
        if (isSetDays)
        {
            Debug.Log($"{days}일 뒤로 퀘스트 수락 시도");
            if (QuestManager.Instance.TryAcceptQuest(quest, days))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            Debug.Log($"일수가 선택안됨");
            UIManager.Instance.ShowPopUp(PopUpType.Alarm);
            UIManager.Instance.alarmPopUp.SetAlarm("일수를 먼저 선택해주세요.");
            return false;
        }
    }

    // 거절 버튼 메서드 ( 필요할지 의문 )
    private void RejectQuest()
    {
        //미구현, 퀘스트 거절 시 퀘스트 목록에서 지우고 한동안 퀘스트 안뜨게 하는 방식 생각해봄.
        Debug.Log($"퀘스트 거절");
    }
}
