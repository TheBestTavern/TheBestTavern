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
                ItemStack item = Data.GetItemStack(slot.Value.GetSlotItemStackID());
                InventoryManager.Instance.Invens[InvenType.Player].AcquireItem(Data.GetRawItem(item.OriginItemKey), item.Count);
                InventoryManager.Instance.Invens[InvenType.Gathering].LooseItem(Data.GetRawItem(item.OriginItemKey), item.Count);
            }
        }
    }

    public override void TargetingSlot(int index)
    {
        base.TargetingSlot(index);

        if (!index2Slots[index].HasItem) return;
        ItemStack item = Data.GetItemStack(index2Slots[index].GetSlotItemStackID());

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

    public void LoseAllItem()
    {
        foreach (var slot in index2Slots)
        {
            if (slot.Value.HasItem)
            {
                ItemStack item = Data.GetItemStack(slot.Value.GetSlotItemStackID());
                InventoryManager.Instance.Invens[InvenType.Gathering].LooseItem(Data.GetRawItem(item.OriginItemKey), item.Count);
            }
        }
    }

}
