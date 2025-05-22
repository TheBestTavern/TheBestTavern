using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Analytics;
using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEditor.AddressableAssets.HostingServices;

[System.Serializable]
public class ItemRecord
{
    [JsonProperty]
    public bool HasDiscovered { get; private set; }

    public ItemRecord()
    {
    }

    public void RecordDiscover()
    {
        HasDiscovered = true;
    }
}

public class ItemRecordManager : MonoSingleton<ItemRecordManager>
{
    public Dictionary<int, ItemRecord> itemRecords { get; private set; } = new(); 

    public override void Init()
    {
        if (_isInitialized) return;
        base.Init();

        DontDestroyOnLoad(this);
        CreateRecordDict();
    }

    public void ApplyLoadData(Dictionary<int, ItemRecord> itemRecords)
    {
        this.itemRecords = itemRecords;
    }


    private void CreateRecordDict()
    {
        foreach (var pair in DataManager.Instance.DataLoader_FoodCategory.ItemsDict)
        {
            ItemRecord item = new ItemRecord();
            itemRecords[pair.Key] = item;
        }
    }

    public ItemRecord GetItemObject(int key)
    {
        return itemRecords[key];
    }

    public void HasGainedItem(int key)
    {
        if (GameManager.Instance.isAnalyticsAgreed)
        {
            if (!itemRecords[key].HasDiscovered)
            {
                // string key.name 아이템 뭐 발견했는지 보내기
                string ItemName = DataManager.Instance.DataLoader_FoodCategory.GetByKey(key).categoryName;

                var ItemEvent = new AnalyticsItem("ItemData")
                {
                    ItemName = ItemName
                };
                AnalyticsService.Instance.RecordEvent(ItemEvent);
            }
        }

        itemRecords[key].RecordDiscover();
    }

    public bool IsDiscovered(int key)
    {
        return itemRecords[key].HasDiscovered;
    }
}