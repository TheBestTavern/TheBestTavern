using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoSingleton<ItemManager>
{
    public List<Item> AllItems { get; private set; }
    public override void Init()
    {
        if (_isInitialized) return;
        base.Init();

        InstantiateItems();
    }

    private void InstantiateItems()
    {
        // 아이템 래핑 인스턴스 생성
        foreach (var itemData in DataManager.Instance.DataLoader_Foods.ItemsList)
        {
            AllItems.Add(new Item(itemData));
        }
    }
}