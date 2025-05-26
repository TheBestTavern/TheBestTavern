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
            yesBtnText.text = "<b><color=#3F0300><size=40>대 성공!!</size></color></b> \n`보상 수령`";
            buttons[0].onClick.RemoveAllListeners();
            buttons[0].onClick.AddListener(() => OnTakeButton());
        }
        else if (curSuccessDegree == SuccessDegree.soso)
        {
            yesBtnText.text = "<b><color=#980800><size=40>중 성공!</size></color></b> \n`보상 수령`";
            buttons[0].onClick.RemoveAllListeners();
            buttons[0].onClick.AddListener(() => OnTakeButton());
        }
        else if (curSuccessDegree == SuccessDegree.notBad)
        {
            yesBtnText.text = "<b><color=#FF0C00><size=40>하 성공</size></color></b>\n`호감도 상승, 보상 획득 실패`";
            buttons[0].onClick.RemoveAllListeners();
            buttons[0].onClick.AddListener(() =>
            {
                TriggerOnCompleteLetter();
                OnClickCloseButton();
            });
        }
        else if(curSuccessDegree == SuccessDegree.fail)
        {
            yesBtnText.text = "<b><color=#1D1D68><size=40>실패</size></color></b>\n`호감도 하락`";
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
        return InventoryManager.Instance.Invens[InvenType.Player].AcquireItem(quest.Origin.compensationID, 6);
    }

    public override void TriggerOnCompleteLetter()
    {
        base.TriggerOnCompleteLetter();
        QuestManager.Instance.JustCompleteQuests.Remove((quest.Origin.key, curSuccessDegree));
    }
}
