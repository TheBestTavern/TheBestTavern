using System;
using System.Collections.Generic;
using System.Linq;

public class InventoryModel
{
    InvenType invenType;
    public Dictionary<int, List<int>> itemID2ItemStackIDs = new();  // <Data_Foods.key, ID리스트> => 
    public List<int> itemStackIDs = new(); // <ID, 아이템 스택> => <스택ID> 해당 모델이 보유한 itemStack의 ID
    IItemStackFactory itemStackFactory;
    int SlotCount { get; set; } // 슬롯 최대 갯수
    int maxStackSize { get; set; } // 스택 당 아이템 갯수

    public Action<int, InvenType> OnChanged;

    public void Init(InvenType invenType, int slotCount, int maxStackSize, Action<int, InvenType> OnModelChanged, IItemStackFactory itemStackFactory)
    {
        this.invenType = invenType;
        this.SlotCount = slotCount;
        this.maxStackSize = maxStackSize;
        this.OnChanged = OnModelChanged;
        this.itemStackFactory = itemStackFactory;

        EventBus.Subscribe<ItemStackOnChangeEvent>(TriggerOnChange);
        EventBus.Subscribe<ItemStackOnZeroEvent>(RemoveItem);
        //EventBus.Subscribe<ItemStackOnZeroEvent>(ItemStackManager.Instance.ReCoverID);
    }

    public bool AddItemWithCheck(Data_Foods data_Foods, int amount)
    {
        // 1. 여유 공간 확인
        int space = 0;
        if (itemID2ItemStackIDs.ContainsKey(data_Foods.key))
        {
            foreach (var id in itemID2ItemStackIDs[data_Foods.key])
            {
                space += Data.GetItemStack(id).GetSpace(maxStackSize);
            }
        }

        if (!(CalcRestSpace() * maxStackSize + space >= amount))
        {
            return false;
        }

        // 2. 추가
        JustAddItem(data_Foods, amount);
        return true;
    }

    private void JustAddItem(Data_Foods data_Foods, int amount)
    {
        // 아이템 획득 기록.
        ItemRecordManager.Instance.HasGainedItem(data_Foods.FoodCategory);

        int remain = amount;

        if (!itemID2ItemStackIDs.ContainsKey(data_Foods.key))
        {
            itemID2ItemStackIDs.Add(data_Foods.key, new List<int>());
        }
        List<int> IDList = itemID2ItemStackIDs[data_Foods.key];

        foreach (int id in IDList)
        {
            remain = Data.GetItemStack(id).Add(remain, maxStackSize);
        }

        while (remain > 0)
        {
            //var temp = new ItemStack(data_Foods, 0, 아이템삭제);
            var itemStack = itemStackFactory.Create(data_Foods, 0, invenType);
            remain = itemStack.Add(remain, maxStackSize);
            IDList.Add(itemStack.ID);
            itemStackIDs.Add(itemStack.ID);
        }
    }

    public bool DecreaseItemWithCheck(Data_Foods data_Foods, int amount)
    {
        // 1. 감소할만큼의 아이템이 있는지.
        int count = 0;
        if (itemID2ItemStackIDs.TryGetValue(data_Foods.key, out List<int> IDs))
        {
            foreach (int id in IDs)
            {
                count += Data.GetItemStack(id).Count;
            }
        }

        if (amount > count) return false;

        // 2. 감소시키기
        int remain = amount;
        foreach (int id in IDs.ToList())
        {
            remain = Data.GetItemStack(id).Subtract(remain);
            if (remain <= 0) break;
        }
        return true;
    }

    public int CalcRestSpace()
    {
        int stackCount = 0;
        foreach (var pair in itemID2ItemStackIDs)
        {
            stackCount += pair.Value.Count;
        }

        return SlotCount - stackCount;
    }

    public void SortingModel_Merge()
    {
        foreach (var pair in itemID2ItemStackIDs)
        {
            int toMergeCount = 0;
            foreach (int id in pair.Value)
            {
                if (pair.Value.Count < 2) continue;
                if (Data.GetItemStack(id).Count != maxStackSize)
                {
                    toMergeCount += Data.GetItemStack(id).Count;
                    Data.GetItemStack(id).TriggerOnDestroy();
                }
            }

            if (toMergeCount > 0)
            {
                JustAddItem(Data.GetRawItem(pair.Key), toMergeCount);
            }
        }
    }

    private void JustRemoveItem(int id, InvenType invenType)
    {
        if (invenType == this.invenType)
        {
            ItemStack itemStack = Data.GetItemStack(id);
            itemStackIDs.Remove(id);
            itemID2ItemStackIDs[itemStack.OriginItemKey].Remove(id);
            if (itemID2ItemStackIDs[itemStack.OriginItemKey].Count == 0) itemID2ItemStackIDs.Remove(itemStack.OriginItemKey);
        }
    }

    private void RemoveItem(ItemStackOnZeroEvent evt)
    {
        JustRemoveItem(evt.ID, evt.invenType);
    }

    private void TriggerOnChange(ItemStackOnChangeEvent evt)
    {
        OnChanged?.Invoke(evt.ID, evt.invenType);
    }

    public void Dipose()
    {
        EventBus.UnSubscribe<ItemStackOnChangeEvent>(TriggerOnChange);
        EventBus.UnSubscribe<ItemStackOnZeroEvent>(RemoveItem);
    }

    public void ApplyLoadData(Dictionary<int, List<int>> foodKey2IDs, List<int> ID2ItemStack)
    {
        this.itemID2ItemStackIDs = foodKey2IDs;
        this.itemStackIDs = ID2ItemStack;
    }
}