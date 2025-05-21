using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class QuestResultLetter : QuestBaseLetter
{
    TextMeshProUGUI yesBtnText;
    SuccessDegree curSuccessDegree;

    private InventoryController playerInvenController;

    // 편지 생성시 한번 초기화
    public override void FirstInit(Action<QuestBaseSlot> action)
    {
        if (IsReady) return;

        base.FirstInit(action);


        playerInvenController = InventoryManager.Instance.Invens[InvenType.Player];

        //  버튼 초기화
        yesBtnText = buttons[0].GetComponentInChildren<TextMeshProUGUI>();
        IsReady = true;
    }

    // 편지 열때마다 필요한 초기화.
    public override void On(Quest quest, SuccessDegree successDegree, QuestBaseSlot baseQuestSlot)
    {
        base.On(quest, successDegree, baseQuestSlot);
        if ((int)successDegree != 0)
        {
            curSuccessDegree = successDegree;

            switch (curSuccessDegree)
            {
                case SuccessDegree.fail:
                    bodyText.text = quest.Origin.letterFail;
                    break;
                case SuccessDegree.notBad:
                    bodyText.text = quest.Origin.letterNotBadSuccess;
                    break;
                case SuccessDegree.soso:
                    bodyText.text = quest.Origin.letterSuccess;
                    break;
                case SuccessDegree.good:
                    bodyText.text = quest.Origin.letterSuccess;
                    break;
            }
        }
        else
        {
            Debug.LogError("퀘스트의 성공 여부 변수가 할당되지 않았습니다.");
        }

        // 편지 내용 초기화
        if (curSuccessDegree == SuccessDegree.good)
        {
            yesBtnText.text = "더할나위 없군\n`보상 수령`";
            buttons[0].onClick.RemoveAllListeners();
            buttons[0].onClick.AddListener(() => OnTakeButton());
        }
        else if (curSuccessDegree == SuccessDegree.soso)
        {
            yesBtnText.text = "좋아하는 걸 보니 기분 좋군\n`보상 수령`";
            buttons[0].onClick.RemoveAllListeners();
            buttons[0].onClick.AddListener(() => OnTakeButton());
        }
        else if (curSuccessDegree == SuccessDegree.notBad)
        {
            yesBtnText.text = "좀 더 잘할 수 있을 거 같아\n`호감도 상승`";
            buttons[0].onClick.RemoveAllListeners();
            buttons[0].onClick.AddListener(() =>
            {
                TriggerOnCompleteLetter();
                OnClickCloseButton();
            });
        }
        else if(curSuccessDegree == SuccessDegree.fail)
        {
            yesBtnText.text = "미안하네..\n`호감도 하락`";
            buttons[0].onClick.RemoveAllListeners();
            buttons[0].onClick.AddListener(() =>
            {
                TriggerOnCompleteLetter();
                OnClickCloseButton();
            });
        }
    }

    /// <summary>
    /// 버튼 구독 메서드들
    /// </summary>

    // 수락 버튼 메서드
    protected void OnTakeButton()
    {
        if (TakeResult())
        {
            TriggerOnCompleteLetter(); // 편지 읽고 퀘스트수락/결과수령 시 슬롯 파괴 이벤트 실행
            OnClickCloseButton(); //  퀘스트수락/결과수령 시 편지 닫기
        }
        else
        {
            Debug.Log("인벤토리 칸이 부족합니다.");
            PopUpManager.Instance.ShowPopUp(PopUpType.Alarm);
            PopUpManager.Instance.alarmPopUp.SetAlarm("인벤토리 칸이 부족합니다.");
        }
    }

    private bool TakeResult()
    {
        return InventoryManager.Instance.Invens[InvenType.Player].아이템획득(quest.Origin.compensationID, 1);
    }

    public override void TriggerOnCompleteLetter()
    {
        base.TriggerOnCompleteLetter();
        QuestManager.Instance.JustCompleteQuests.Remove((quest.Origin.key, curSuccessDegree));
    }
}
