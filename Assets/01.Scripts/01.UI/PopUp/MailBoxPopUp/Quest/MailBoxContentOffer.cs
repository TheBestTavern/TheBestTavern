using System;
using System.Collections.Generic;
using UnityEngine;

public class MailBoxContentOffer : MailBoxContentBase
{
    public override void OnEnable()
    {
        if (isReadyTodaySlot) return;
        isReadyTodaySlot = true;
        base.OnEnable();

        MakeSlot(QuestManager.Instance.questData.TodayAvailableQuest);
        //OnNewDay command = new(this);
        //DayManager.Instance.AddCommand(command);
    }

    public override void MakeSlot(List<int> quests)
    {
        foreach (var slot in slots)
        {
            Destroy(slot.gameObject);
        }
        slots.Clear();

        base.MakeSlot(quests);
        Debug.Log("의뢰 수주창 슬롯 생성");

    }

    public async override void OpenLetter(Quest quest, QuestBaseSlot slot)
    {
        //1. 편지 띄우기
        currentLetter = await PopUpManager.Instance.ShowPopUp(PopUpType.OfferLetter) as QuestBaseLetter;

        base.OpenLetter(quest, slot);
    }

    //public class OnNewDay : IDayCommand
    //{
    //    MailBoxContentOffer prt;
    //    public OnNewDay(MailBoxContentOffer mailBoxContentOffer)
    //    {
    //        this.prt = mailBoxContentOffer;
    //    }

    //    public int Priority => 2000;

    //    public void Execute()
    //    {
    //        prt.MakeSlot(QuestManager.Instance.questData.TodayAvailableQuest);
    //    }
    //}
}