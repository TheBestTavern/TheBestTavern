using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// TAB 메뉴 NPC와의 관계 클래스 
/// </summary>
public class MenuRelationUI : BaseMenuContentUI
{
    Dictionary<int, RelationSlot> slots = new(); // npcID, npc정보슬롯
    [SerializeField] RelationSlot slotPref;
    Transform slotTsr;
    /// <summary>
    /// TAB 메뉴 NPC와의 관계 생성 함수
    /// </summary>
    public async override void CreateContent()
    {
        // To Do - 관계 목록 생성
        base.CreateContent();
        var rawNpclist = DataManager.Instance.DataLoader_NPC.ItemsList;
        for (int i = 0; i < rawNpclist.Count; i++)
        {
            slots[i] = await PoolManager.Instance.GetAddressable<RelationSlot>("RelationSlot.prefab", Vector3.zero, slotTsr);
        }
    }

    public void OnChangeNPC(int npcID)
    {
        slots[npcID].UpdateSlot();
    }
}
