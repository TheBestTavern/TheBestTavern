using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  TAB 메뉴 진행중인 퀘스트 클래스
/// </summary> 
public class MenuInProgressQuestUI : BaseMenuContentUI
{
    /// <summary>
    /// TAB 메뉴 진행중인 퀘스트 생성 함수
    /// </summary>
    public override void CreateContent()
    {
        // 진행중인 퀘스트가 없다면 리턴
        if (QuestManager.Instance.questData.AcceptedQuests == null)
            return;

        // 있다면 진행중인 퀘스트 목록 순회
        for (int i = 0; i < QuestManager.Instance.questData.AcceptedQuests.Count; i++)
        {
            // To Do - 진행중인 퀘스트 생성
        }
    }
}
