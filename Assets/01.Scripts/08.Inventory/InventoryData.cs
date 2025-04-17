using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryData
{
    public Dictionary<Item, int> InventoryModel { get; private set; }
    //public Dictionary<int, Item> TabInventory { get; private set; }
    //public Dictionary<int, Item> BottomIngredientInventory { get; private set; }
    //public Dictionary<int, Item> BottomDishInventory { get; private set; }

    public void AddItems(Item item, int number)
    {
        if (InventoryModel.ContainsKey(item))
        {
            InventoryModel[item] += number;
        }
        else
        {
            InventoryModel.Add(item, number);
        }
    }

    public void RemoveItems(Item item, int number)
    {
        InventoryModel[item] -= number;
    }

    public bool CheckRestSpace(Item item, int number)
    {
        if (true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CheckHavingItems(Item item, int number)
    {
        if (InventoryModel.ContainsKey(item))
        {
            return InventoryModel[item] >= number;
        }
        else
        {
            return false;
        }
    }
}
