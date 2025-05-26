using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class QuestOfferLetter : QuestBaseLetter
{
    private int days;
    bool isSetDays;

    Image[] buttonImages;

    // 편지 생성시 한번 초기화
    public override void FirstInit(Action<QuestBaseSlot> action)
    {
        if (IsReady) return;

        base.FirstInit(action);

        int count = buttons.Count;
        buttonImages = new Image[count];
        for (int i = 0; i < count; i++)
        {
            buttonImages[i] = buttons[i].GetComponent<Image>();
        }

        // 버튼 초기화
        buttons[0].onClick.AddListener(() =>
        {
            OnClickDays(7);
            OnClickButtonEffect(0);
        });
        buttons[1].onClick.AddListener(() =>
        {
            OnClickDays(11);
            OnClickButtonEffect(1);
        });
        buttons[2].onClick.AddListener(() =>
        {
            OnClickDays(14);
            OnClickButtonEffect(2);
        });
        buttons[3].onClick.AddListener(() =>
        {
            OnOKButton();
            OnClickButtonEffect(3);
        });

        IsReady = true;
    }

    // 편지 열때마다 필요한 초기화.
    public override void On(Quest quest, SuccessDegree successDegree, QuestBaseSlot baseQuestSlot)
    {
        // 문구 초기화
        base.On(quest, successDegree, baseQuestSlot);
        isSetDays = false;
        days = 0;
        string colored = quest.Origin.letterOffer;
        foreach (string keyword in quest.Origin.letterOfferKeyword)
        {
            colored = colored.Replace(keyword, $"<b><color=#2C6DA6>{keyword}</color></b>");
        }
        bodyText.text = colored;

        foreach (var button in buttonImages)
        {
            button.color = Color.white;
        }
    }

    private void OnClickDays(int day)
    {
        days = day;
        isSetDays = true;
    }

    // 수락 버튼 메서드
    protected async void OnOKButton()
    {
        if (await AcceptQuest())
        {
            TriggerOnCompleteLetter(); // 편지 읽고 퀘스트수락/보상수령 시 슬롯 파괴 이벤트 실행
            OnClickCloseButton(); //  퀘스트수락/보상수령 시 편지 닫기
        }
    }

    private async Task<bool> AcceptQuest()
    {
        if (isSetDays)
        {
            //bool success = await QuestManager.Instance.TryAcceptQuest(quest, days); // 함수를 결국 전부 async로 바꿔야하는건가?
            Debug.Log($"{days}일 뒤로 퀘스트 수락 시도");
            return await QuestManager.Instance.TryAcceptQuest(quest.Origin.key, days);
        }
        else
        {
            Debug.Log($"일수가 선택안됨");
            await PopUpManager.Instance.ShowPopUp(PopUpType.Alarm);
            PopUpManager.Instance.alarmPopUp.SetAlarm("일수를 먼저 선택해주세요.");
            return false;
        }
    }

    public void OnClickButtonEffect(int index)
    {
        for(int i = 0; i < buttons .Count; i++)
        {
            buttonImages[i].color = Color.white;
        }
        buttonImages[index].color = new Color(0.7f, 0.7f, 0.7f);
    }

    // 거절 버튼 메서드 ( 필요할지 의문 )
    private void RejectQuest()
    {
        //미구현, 퀘스트 거절 시 퀘스트 목록에서 지우고 한동안 퀘스트 안뜨게 하는 방식 생각해봄.
        Debug.Log($"퀘스트 거절");
    }
}
