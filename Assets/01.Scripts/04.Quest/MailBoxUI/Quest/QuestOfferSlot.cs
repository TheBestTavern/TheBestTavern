using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestOfferSlot : QuestBaseSlot
{
    public override void SetSlot(Quest quest, int indexNum)
    {
        base.SetSlot(quest, indexNum);

        questName.text = quest.origin.name;

        Debug.Log($"{index}번 슬롯 준비 구독완료");
    }
}