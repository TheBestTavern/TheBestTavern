using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// TAB 메뉴 완료된 퀘스트 클래스
/// </summary>
public class MenuCompletedQuestUI : BaseMenuContentUI
{
    /// <summary>
    /// TAB 메뉴 완료된 퀘스트 생성 함수
    /// </summary>
    public override void CreateContent()
    {
        //// 완료된 퀘스트가 없다면 리턴
        //if (QuestManager.Instance.questData.CompletedQuests == null)
        //    return;

        //// 있다면 완료된 퀘스트 목록 순회
        //for (int i = 0; i < QuestManager.Instance.questData.CompletedQuests.Count; i++)
        //{
        //    // To Do - 완료된 퀘스트 생성
        //    if (QuestManager.Instance.questData.OnceCompletedQuests == null)
        //        return;

        //    for (int i = 0; i < QuestManager.Instance.questData.OnceCompletedQuests.Count; i++)
        //    {
        //        QuestOfferSlot questSlot = Instantiate(contentPrefab, contentParent).GetComponent<QuestOfferSlot>();
        //        questSlot.SetSlot(QuestManager.Instance.questData.OnceCompletedQuests[i], i);
        //    }
        //}
    }
}
