using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestOfferSlot : QuestBaseSlot
{
    public override void SetSlot(int questID, int indexNum)
    {
        base.SetSlot(questID, indexNum);

        questName.text = slotQuest.origin.name;
    }
}