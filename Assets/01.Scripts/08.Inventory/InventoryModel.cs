using System;
using System.Collections.Generic;

public class InventoryModel
{
    InvenType Inven;
    public Dictionary<int, List<int>> foodKey2IDs = new();  // <Data_Foods.key, ID리스트>
    public Dictionary<int, ItemStack> ID2ItemStack = new(); // <ID, 아이템 스택>
    int SlotCount { get; set; } // 슬롯 최대 갯수
    int maxStackSize { get; set; } // 스택 당 아이템 갯수

    public Action<int> OnChanged;

    public void Init(int slotCount, int maxStackSize, Action<int> OnModelChanged)
    {
        this.SlotCount = slotCount;
        this.maxStackSize = maxStackSize;
        this.OnChanged = OnModelChanged;
    }

    public bool 아이템검사후추가(Data_Foods data_Foods, int amount)
    {
        // 1. 여유 공간 확인
        int space = 0;
        if (foodKey2IDs.ContainsKey(data_Foods.key))
        {
            foreach (var id in foodKey2IDs[data_Foods.key])
            {
                space += ID2ItemStack[id].GetSpace(maxStackSize);
            }
        }

        if (!(여분의공간반환() * maxStackSize + space >= amount))
        {
            return false;
        }

        // 2. 추가
        아이템그냥추가(data_Foods, amount);
        return true;
    }

    private void 아이템그냥추가(Data_Foods data_Foods, int amount)
    {
        int remain = amount;

        if (!foodKey2IDs.ContainsKey(data_Foods.key))
        {
            foodKey2IDs.Add(data_Foods.key, new List<int>());
        }
        List<int> IDList = foodKey2IDs[data_Foods.key];

        foreach (int id in IDList)
        {
            remain = ID2ItemStack[id].Add(remain, maxStackSize);
        }

        while (remain > 0)
        {
            //var temp = new ItemStack(data_Foods, 0, 아이템삭제);
            var temp = ItemStackManager.Instance.InstantiateItem(data_Foods, 0, 아이템삭제, OnChanged);
            remain = temp.Add(remain, maxStackSize);
            IDList.Add(temp.ID);
            ID2ItemStack.Add(temp.ID, temp);
        }
    }

    public bool 아이템감소(Data_Foods data_Foods, int amount)
    {
        // 1. 감소할만큼의 아이템이 있는지.
        int count = 0;
        if (foodKey2IDs.ContainsKey(data_Foods.key))
        {
            foreach (int id in foodKey2IDs[data_Foods.key])
            {
                count += ID2ItemStack[id].Count;
            }
        }

        if (amount > count) return false;

        // 2. 감소시키기
        int remain = amount;
        foreach (int id in foodKey2IDs[data_Foods.key])
        {
            remain = ID2ItemStack[id].Subtract(remain);
            if (remain <= 0) break;
        }
        return true;
    }

    public int 여분의공간반환()
    {
        int stackCount = 0;
        foreach (var pair in foodKey2IDs)
        {
            stackCount += pair.Value.Count;
        }

        return SlotCount - stackCount;
    }

    public void 아이템정렬_합치기()
    {
        foreach (var pair in foodKey2IDs)
        {
            int toMergeCount = 0;
            foreach (int id in pair.Value)
            {
                if (pair.Value.Count < 2) continue;
                if (ID2ItemStack[id].Count != maxStackSize)
                {
                    toMergeCount += ID2ItemStack[id].Count;
                    ID2ItemStack[id].TriggerOnDestroy();
                }
            }

            if (toMergeCount > 0)
            {
                아이템그냥추가(Data.GetRawItem(pair.Key), toMergeCount);
            }
        }
    }

    private void 아이템삭제(int id)
    {
        ItemStack itemStack = ID2ItemStack[id];

        ID2ItemStack.Remove(id);
        foodKey2IDs[itemStack.Origin.key].Remove(id);
        if (foodKey2IDs[itemStack.Origin.key].Count == 0) foodKey2IDs.Remove(itemStack.Origin.key);
    }

    //public void TriggerOnChange(int id)
    //{
    //    OnChanged?.Invoke(id);
    //}
}