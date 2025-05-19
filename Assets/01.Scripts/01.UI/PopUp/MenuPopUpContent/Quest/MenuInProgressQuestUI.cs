using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInProgressQuestUI : MenuQuestUIBase<List<int>>
{
    private void Awake()
    {
        toShowList = QuestManager.Instance.AcceptedQuests;
    }

    public override void CreateContent()
    {
        base.CreateContent();

        EventBus.Subscribe<QuestAcceptEvent>(UpdateList);
        EventBus.Subscribe<QuestCompleteEvent>(UpdateList);
    }

    public async override void SetList()
    {
        foreach (var slot in slots)
        {
            slot.TriggerReturn();
        }
        slots.Clear();

        foreach (int questID in toShowList)
        {
            QuestSlot slot = await PoolManager.Instance.GetAddressable<QuestSlot>("QuestSlot.prefab", Vector3.zero, spawnTsr);
            slot.SetSlot(questID, true);
            slots.Add(slot);
        }
    }

    private void OnDestroy()
    {
        EventBus.Subscribe<QuestClickLetterBtnEvent>(OpenLetter);
        EventBus.UnSubscribe<QuestAcceptEvent>(UpdateList);
        EventBus.UnSubscribe<QuestCompleteEvent>(UpdateList);
    }

    // 이벤트 버스 함수
    public async override void OpenLetter(QuestClickLetterBtnEvent evt)
    {
        var letter = await PopUpManager.Instance.ShowPopUp(PopUpType.Letter) as QuestLetter;
        letter.SetLetter(evt.quest, true);
    }

    public void UpdateList(QuestAcceptEvent evt)
    {
        SetList();
    }
    public void UpdateList(QuestCompleteEvent evt)
    {
        SetList();
    }
}