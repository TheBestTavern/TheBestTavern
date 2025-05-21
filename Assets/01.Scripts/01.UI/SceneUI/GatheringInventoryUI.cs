using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.InputSystem;
using static System.Collections.Specialized.BitVector32;

/// <summary>
/// 채집씬 인벤토리 UI
/// </summary>
public class GatheringInventoryUI : InventoryViewLoose
{
    public void AddItemToPlayerInventory()
    {
        foreach (var slot in index2Slots)
        {
            if (slot.Value.HasItem)
            {
                ItemStack item = slot.Value.GetSlotItem();
                InventoryManager.Instance.Invens[InvenType.Player].아이템획득(Data.GetRawItem(item.Origin.key), item.Count);
                InventoryManager.Instance.Invens[InvenType.Gathering].아이템잃음(Data.GetRawItem(item.Origin.key), item.Count);
            }
        }
    }

    public override void 아이템타게팅(int index)
    {
        base.아이템타게팅(index);

        if (!index2Slots[index].HasItem) return;
        ItemStack item = index2Slots[index].GetSlotItem();

        var dropArea = CaptureManager.Instance?.baitDropArea;
        if (dropArea != null)
        {
            dropArea.SetItem(item);
        }
        else
        {
            Debug.LogWarning("BaitDropArea null발생");
        }
    }

}
