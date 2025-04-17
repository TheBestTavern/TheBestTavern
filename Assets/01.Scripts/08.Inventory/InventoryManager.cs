using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoSingleton<InventoryManager>
{
    InventoryData inventoryData;
    public Dictionary<Item, int> InventoryModel => inventoryData.InventoryModel;

    public override void Init()
    {
        if (_isInitialized) return;
        base.Init();

        inventoryData = new();
    }

    public bool TryAddItemInModel(Item item, int number)
    {
        if (inventoryData.CheckRestSpace(item, number))
        {
            inventoryData.AddItems(item, number);
            return true;
        }
        else
        {
            Debug.Log("인벤토리에 공간이 없습니다.");
            return false;
        }
    }

    public bool TryRemoveItemInModel(Item item, int number)
    {
        if (inventoryData.CheckHavingItems(item, number))
        {
            inventoryData.RemoveItems(item, number);
            return true;
        }
        else
        {
            Debug.Log("인벤토리에 해당 아이템이 없거나 부족합니다.");
            return false;
        }
    }
}
