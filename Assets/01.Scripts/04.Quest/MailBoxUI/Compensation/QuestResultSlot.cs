using System;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class QuestResultSlot : QuestBaseSlot
{
    public override void SetSlot(Quest quest, int indexNum)
    {
        base.SetSlot(quest, indexNum);

        questName.text = quest.origin.name; // 나중에 퀘스트 이름 대신 실패 대사 넣기.

        if (quest.IsSuccessful.HasValue)
        {
            isSuccessful = (bool)quest.IsSuccessful;
            //icon.sprite = isSuccessful? 성공이미지 : 실패이미지 ;
        }
        else
        {
            Debug.LogError("퀘스트의 성공 여부 변수가 할당되지 않았습니다.");
        }

        Debug.Log($"{index}번 슬롯 준비 구독완료");
    }
}