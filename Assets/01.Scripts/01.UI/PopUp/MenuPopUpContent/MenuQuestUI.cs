using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuQuestUI : MonoBehaviour
{
    [SerializeField] private QuestSlot questSlotPrefab;
    [SerializeField] private Transform inProgressQuestParent;
    [SerializeField] private Transform completedQuestParent;

    // Start is called before the first frame update
    void Start()
    {
    
    }

    private void OnEnable()
    {
        SetInProgressQuests(); 
        SetCompletedQuests();
    }

    void SetInProgressQuests()
    {
        for (int i = 0; i < QuestManager.Instance.questData.AcceptedQuests.Count; i++)
        {
            QuestSlot questSlot = Instantiate(questSlotPrefab, inProgressQuestParent).GetComponent<QuestSlot>();
            questSlot.SetSlot(QuestManager.Instance.questData.AcceptedQuests[i], i);
        }
    }

    void SetCompletedQuests()
    {
        for (int i = 0; i < QuestManager.Instance.questData.CompletedQuests.Count; i++)
        {
            QuestSlot questSlot = Instantiate(questSlotPrefab, completedQuestParent).GetComponent<QuestSlot>();
            questSlot.SetSlot(QuestManager.Instance.questData.CompletedQuests[i], i);
        }
    }
}
