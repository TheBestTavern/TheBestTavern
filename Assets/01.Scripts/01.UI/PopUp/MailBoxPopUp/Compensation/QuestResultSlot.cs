using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class QuestResultSlot : QuestBaseSlot
{
    public override void SetSlot(int questID, int indexNum)
    {
        base.SetSlot(questID, indexNum);
        // 버튼에 메서드 구독
        openLetterBtn.onClick.RemoveAllListeners();
        openLetterBtn.onClick.AddListener(() => OpenLetter());

        questName.text = slotQuest.origin.name; // 나중에 퀘스트 이름 대신 실패 대사 넣기.

        if (slotQuest.lastSuccessDegree.HasValue)
        {
            //결과 슬롯 설정
        }
        else
        {
            Debug.LogError("퀘스트의 lastSuccessDegree변수가 할당되지 않았습니다.");
        }

        Debug.Log($"{index}번 슬롯 준비 구독완료");
    }

    private async void OpenLetter()
    {
        //mailBoxContent.OpenLetter(slotQuest, this);
        var letter = (QuestResultLetter)await UIManager.Instance.ShowPopUp(PopUpType.ResultLetter);
        letter.On(slotQuest, this);
        Debug.Log($"{index}번 편지 열람");
    }
}