using System.Collections.Generic;

public class ItemRecord
{
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
    public override void Init()
    {
        if (_isInitialized) return;
        base.Init();

        DontDestroyOnLoad(this);
        CreateRecordDict();
    }

    Dictionary<int, ItemRecord> itemRecords = new();

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
        itemRecords[key].RecordDiscover();
    }

    public bool IsDiscovered(int key)
    {
        return itemRecords[key].HasDiscovered;
    }
}