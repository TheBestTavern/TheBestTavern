using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class QuestResultSlot : QuestBaseSlot
{
    private SuccessDegree successDegree;

    public override void SetSlot(int questID, SuccessDegree successDegree, int indexNum)
    {
        base.SetSlot(questID, successDegree, indexNum);
        // 버튼에 메서드 구독
        openLetterBtn.onClick.RemoveAllListeners();
        openLetterBtn.onClick.AddListener(() => OpenLetter());

        this.successDegree = successDegree;
        questName.text = slotQuest.Origin.name; // 나중에 퀘스트 이름 대신 실패 대사 넣기.

        //if (slotQuest.lastSuccessDegree.HasValue)
        //{
        //    //결과 슬롯 설정
        //}
        if ((int)successDegree != 0)
        {
            
        }
        else
        {
            Debug.LogError($"{questID}퀘스트의 successDegree가 {successDegree}로 설정돼있습니다.");
        }

        Debug.Log($"{index}번 슬롯 준비 구독완료");
    }

    private async void OpenLetter()
    {
        //mailBoxContent.OpenLetter(slotQuest, this);
        var letter = (QuestResultLetter)await PopUpManager.Instance.ShowPopUp(PopUpType.ResultLetter);
        letter.On(slotQuest, successDegree, this);
        Debug.Log($"{index}번 편지 열람");
    }
}