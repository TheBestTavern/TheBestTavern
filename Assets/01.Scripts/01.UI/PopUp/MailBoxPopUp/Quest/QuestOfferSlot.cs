using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestOfferSlot : QuestBaseSlot
{
    public override void SetSlot(int questID, SuccessDegree successDegree, int indexNum)
    {
        base.SetSlot(questID, successDegree, indexNum);
        // 버튼에 메서드 구독
        openLetterBtn.onClick.RemoveAllListeners();
        openLetterBtn.onClick.AddListener(() => OpenLetter());

        questName.text = slotQuest.Origin.name;
    }

    private async void OpenLetter()
    {
        //mailBoxContent.OpenLetter(slotQuest, this);
        var letter = (QuestOfferLetter)await PopUpManager.Instance.ShowPopUp(PopUpType.OfferLetter);
        letter.On(slotQuest,SuccessDegree.none, this);
        Debug.Log($"{index}번 편지 열람");
    }
}