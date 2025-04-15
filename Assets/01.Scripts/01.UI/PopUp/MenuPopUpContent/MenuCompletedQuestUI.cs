using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCompletedQuestUI : BaseMenuContentUI
{
    public override void CreateContent()
    {
        if (QuestManager.Instance.questData.OnceCompletedQuests == null)
            return;

        for (int i = 0; i < QuestManager.Instance.questData.OnceCompletedQuests.Count; i++)
        {
            QuestOfferSlot questSlot = Instantiate(contentPrefab, contentParent).GetComponent<QuestOfferSlot>();
            questSlot.SetSlot(QuestManager.Instance.questData.OnceCompletedQuests[i], i);
        }
    }
}
