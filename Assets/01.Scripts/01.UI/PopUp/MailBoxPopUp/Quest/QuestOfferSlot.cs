using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestOfferSlot : QuestBaseSlot
{
    public override void SetSlot(int questID, int indexNum)
    {
        base.SetSlot(questID, indexNum);
        // 버튼에 메서드 구독
        openLetterBtn.onClick.RemoveAllListeners();
        openLetterBtn.onClick.AddListener(() => OpenLetter());

        questName.text = slotQuest.origin.name;
    }

    private async void OpenLetter()
    {
        //mailBoxContent.OpenLetter(slotQuest, this);
        var letter = (QuestOfferLetter)await UIManager.Instance.ShowPopUp(PopUpType.OfferLetter);
        letter.On(slotQuest, this);
        Debug.Log($"{index}번 편지 열람");
    }
}