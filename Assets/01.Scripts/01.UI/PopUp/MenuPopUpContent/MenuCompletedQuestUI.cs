using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCompletedQuestUI : BaseMenuContentUI
{
    public override void CreateContent()
    {
        if (QuestManager.Instance.questData.CompletedQuests == null)
            return;

        for (int i = 0; i < QuestManager.Instance.questData.CompletedQuests.Count; i++)
        {
            QuestSlot questSlot = Instantiate(contentPrefab, contentParent).GetComponent<QuestSlot>();
            questSlot.SetSlot(QuestManager.Instance.questData.CompletedQuests[i], i);
        }
    }
}
