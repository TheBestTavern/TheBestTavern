using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MenuCompletedQuestUI : MenuQuestUIBase<Dictionary<int, SuccessDegree>>
{
    private void Awake()
    {
        toShowList = QuestManager.Instance.OnceSuccessQuests;
    }

    public override void CreateContent()
    {
        base.CreateContent();

        EventBus.Subscribe<QuestSuccessFirstEvent>(UpdateList);
    }

    public async override void SetList()
    {
        foreach(var slot in slots)
        {
            slot.TriggerReturn();
        }
        slots.Clear();

        foreach (var pair in toShowList)
        {
            QuestSlot slot = await PoolManager.Instance.GetAddressable<QuestSlot>("QuestSlot.prefab", Vector3.zero, spawnTsr);
            slot.SetSlot(pair.Key, false);
            slots.Add(slot);
        }
    }

    private void OnDestroy()
    {
        EventBus.UnSubscribe<QuestClickLetterBtnEvent>(OpenLetter);
        EventBus.UnSubscribe<QuestSuccessFirstEvent>(UpdateList);
    }

    // 이벤트 버스 함수
    public async override void OpenLetter(QuestClickLetterBtnEvent evt)
    {
        var letter = await PopUpManager.Instance.ShowPopUp(PopUpType.Letter) as QuestLetter;
        letter.SetLetter(evt.quest, false);
    }

    public void UpdateList(QuestSuccessFirstEvent evt)
    {
        SetList();
    }
}
