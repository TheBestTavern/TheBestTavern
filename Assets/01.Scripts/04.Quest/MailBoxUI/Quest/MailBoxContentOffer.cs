using System;
using System.Collections.Generic;
using UnityEngine;

public class MailBoxContentOffer : MailBoxContentBase
{
    protected override void OnEnable()
    {
        if (isReady) return;

        base.OnEnable();
        MakeSlot(QuestManager.Instance.questData.TodayAvailableQuest);
    }

    public override void MakeSlot(List<int> quests)
    {
        foreach (var slot in slots)
        {
            Destroy(slot.gameObject);
        }
        slots.Clear();

        base.MakeSlot(quests);
    }

    public async override void OpenLetter(Quest quest, QuestBaseSlot slot)
    {
        //1. 편지 띄우기
        currentLetter = await UIManager.Instance.ShowPopUp(PopUpType.OfferLetter) as QuestBaseLetter;

        base.OpenLetter(quest, slot);
    }
}