using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestResultLetter : QuestBaseLetter
{
    bool isSuccessful;
    TextMeshProUGUI yesBtnText;

    // 편지 생성시 한번 초기화
    public override void FirstInit(Quest quest, Action<QuestBaseSlot> action)
    {
        if (IsReady) return;

        base.FirstInit(quest, action);

        //  버튼 초기화
        buttons[0].onClick.AddListener(() => OnOKButton());
        yesBtnText = buttons[0].GetComponentInChildren<TextMeshProUGUI>();
        IsReady = true;
    }

    // 편지 열때마다 필요한 초기화.
    public override void EveryInit(Quest quest, QuestBaseSlot baseQuestSlot)
    {
        base.EveryInit(quest, baseQuestSlot);
        if (quest.IsSuccessful.HasValue)
        {
            isSuccessful = (bool)quest.IsSuccessful;
        }
        else
        {
            Debug.LogError("퀘스트의 성공 여부 변수가 할당되지 않았습니다.");
        }

        // 편지 내용 초기화
        if (isSuccessful)
        {
            bodyText.text = quest.origin.description; // 성공 편지 내용으로 교체 해야함
            yesBtnText.text = "수령";
        }
        else
        {
            bodyText.text = quest.origin.description; // 실패 편지 내용으로 교체 해야함.
            yesBtnText.text = "확인";
        }
    }

    /// <summary>
    /// 버튼 구독 메서드들
    /// </summary>

    // 수락 버튼 메서드
    protected override void OnOKButton()
    {
        base.OnOKButton();
        if (TakeResult())
        {
            TriggerOnCompleteLetter(); // 편지 읽고 퀘스트수락/결과수령 시 슬롯 파괴 이벤트 실행
            OnClickCloseButton(); //  퀘스트수락/결과수령 시 편지 닫기
        }
    }

    private bool TakeResult()
    {
        if (isSuccessful)
        {
            if (true) // 인벤토리 여유 공간 검사
            {
                //    보상 수령 로직 작성
                //    Debug.Log($"보상 수령");
                return true;
            }
            else
            {
                //    Debug.Log("인벤토리 칸이 부족합니다.");
                //    UIManager.Instance.ShowPopUp(PopUpType.Alarm);
                //    UIManager.Instance.alarmPopUp.SetAlarm("인벤토리 칸이 부족합니다.");
                return false;
            }
        }
        else
        {
            //퀘스트 실패 => 호감도 감소
            // 호감도 감소는 여기서 말고 하루 시작할때 적용되는 게 좋을듯.
            return true;
        }
    }
}
