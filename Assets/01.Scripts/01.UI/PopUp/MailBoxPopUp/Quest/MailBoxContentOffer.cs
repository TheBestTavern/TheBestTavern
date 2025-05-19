using System;
using System.Collections.Generic;
using UnityEngine;

public class MailBoxContentOffer : MailBoxContentBase
{
    public async override void OnEnable()
    {
        if (isReadyTodaySlot) return;
        isReadyTodaySlot = true;
        base.OnEnable();

        MakeSlot(QuestManager.Instance.questData.TodayAvailableQuest);

        currentLetter = (QuestBaseLetter)await PopUpManager.Instance.ShowPopUp(PopUpType.OfferLetter);
        currentLetter.OnClickCloseButton();
        currentLetter.FirstInit(RemoveSlot);
        //OnNewDay command = new(this);
        //DayManager.Instance.AddCommand(command);
    }

    public void MakeSlot(List<int> quests)
    {
        foreach (var slot in slots)
        {
            Destroy(slot.gameObject);
        }
        slots.Clear();

        QuestBaseSlot pref;
        int i = 1;
        foreach (var questID in quests)
        {
            pref = Instantiate(slotPref, slotPrt);
            pref.Init(this);
            pref.SetSlot(questID, i);
            slots.Add(pref);
            i++;
        }
    }

    //public async override void OpenLetter(Quest quest, QuestBaseSlot slot)
    //{
    //    //1. 편지 띄우기
    //    currentLetter = await PopUpManager.Instance.ShowPopUp(PopUpType.OfferLetter) as QuestBaseLetter;
    //    base.OpenLetter(quest, slot);
    //}

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