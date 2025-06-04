using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// TAB 메뉴 NPC와의 관계 클래스 
/// </summary>
public class MenuRelationUI : BaseMenuContentUI
{
    Dictionary<int, RelationSlot> slots = new(); // npcID, npc정보슬롯
    [SerializeField] Transform slotTsr;

    bool isReady;
    /// <summary>
    /// TAB 메뉴 NPC와의 관계 생성 함수
    /// </summary>
    public async override void CreateContent()
    {
        // To Do - 관계 목록 생성
        base.CreateContent();

        if (isReady) return;
        isReady = true;

        var rawNpclist = DataManager.Instance.DataLoader_NPC.ItemsList;
        for (int i = 0; i < rawNpclist.Count; i++)
        {
            int npcID = rawNpclist[i].key;
            slots[npcID] = await PoolManager.Instance.GetAddressable<RelationSlot>("RelationSlot.prefab", Vector3.zero, slotTsr);
            slots[npcID].SetSlot(npcID);
        }
        EventBus.Subscribe<NPCChangeFavorEvent>(OnChangeFavor);
        EventBus.Subscribe<NPCFirstMetEvent>(OnFirstMet);
        EventBus.Subscribe<NPCGetQuestRewardEvent>(OnGetQuestReward);
    }

    public void OnChangeFavor(NPCChangeFavorEvent evt)
    {
        slots[evt.npc.Origin.key].UpdateFavor();
    }

    public void OnFirstMet(NPCFirstMetEvent evt)
    {
        slots[evt.npc.Origin.key].UpdateHasMet();
    }

    public void OnGetQuestReward(NPCGetQuestRewardEvent evt)
    {
        slots[evt.npc.Origin.key].UpdateQuestReward();
    }

    private void OnDestroy()
    {
        EventBus.UnSubscribe<NPCChangeFavorEvent>(OnChangeFavor);
        EventBus.UnSubscribe<NPCFirstMetEvent>(OnFirstMet);
        EventBus.UnSubscribe<NPCGetQuestRewardEvent>(OnGetQuestReward);

        foreach(var pair in slots)
        {
            pair.Value.TriggerReturn();
        }
        slots.Clear();
    }
}
