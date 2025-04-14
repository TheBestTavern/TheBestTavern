using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInProgressQuestUI : BaseMenuContentUI
{
    public override void CreateContent()
    {
        if (QuestManager.Instance.questData.AcceptedQuests == null)
            return;

        for (int i = 0; i < QuestManager.Instance.questData.AcceptedQuests.Count; i++)
        {
            QuestSlot questSlot = Instantiate(contentPrefab, contentParent).GetComponent<QuestSlot>();
            questSlot.SetSlot(QuestManager.Instance.questData.AcceptedQuests[i], i);
        }
    }
}
