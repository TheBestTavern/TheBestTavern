using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryModel
{
    Dictionary<int, List<ItemStack>> stacks = new();  // <Data_Foods.key, 아이템스택>
    int SlotMaxCount { get; set; } // 슬롯 최대 갯수
    int PerStackMaxCount { get; set; } // 스택 당 아이템 갯수

    public void Init(int slotMaxCount, int perStackMaxCount)
    {
        SlotMaxCount = slotMaxCount;
        PerStackMaxCount = perStackMaxCount;
    }

    public bool 아이템검사후추가(Data_Foods data_Foods, int amount)
    {
        // 1. 여유 공간 확인
        int space = 0;
        if (stacks.ContainsKey(data_Foods.key))
        {
            foreach (var itemStack in stacks[data_Foods.key])
            {
                space += itemStack.GetSpace(PerStackMaxCount);
            }
        }

        if (!(여분의공간반환() * PerStackMaxCount + space >= amount))
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

        if (!stacks.ContainsKey(data_Foods.key))
        {
            stacks.Add(data_Foods.key, new List<ItemStack>());
        }
        List<ItemStack> itemStackList = stacks[data_Foods.key];

        foreach (ItemStack itemStack in itemStackList)
        {
            remain = itemStack.Add(amount, PerStackMaxCount);
        }

        while (remain > 0)
        {
            var temp = new ItemStack(data_Foods, 0);
            remain = temp.Add(remain, PerStackMaxCount);
            itemStackList.Add(temp);
        }
    }

    public bool 아이템감소(Data_Foods data_Foods, int amount)
    {
        // 1. 감소할만큼의 아이템이 있는지.
        int count = 0;
        if (stacks.ContainsKey(data_Foods.key))
        {
            foreach (var itemStack in stacks[data_Foods.key])
            {
                count += itemStack.Count;
            }
        }

        if (amount > count) return false;

        // 2. 감소시키기
        int remain = amount;
        foreach (var itemStack in stacks[data_Foods.key])
        {
            remain = itemStack.Subtract(remain);
            if (remain <= 0) break;
        }
        return true;
    }

    public int 여분의공간반환()
    {
        int stackCount = 0;
        foreach (var pair in stacks)
        {
            stackCount += pair.Value.Count;
        }

        return SlotMaxCount - stackCount;
    }

    public void 아이템정렬_합치기()
    {
        foreach (var pair in stacks)
        {
            int toMergeCount = 0;
            foreach (ItemStack itemStack in pair.Value)
            {
                if (pair.Value.Count < 2) continue;
                if (itemStack.Count != PerStackMaxCount)
                {
                    toMergeCount += itemStack.Count;
                    아이템그냥삭제(itemStack);
                }
            }

            if (toMergeCount > 0)
            {
                아이템그냥추가(Data.GetRawItem(pair.Key), toMergeCount);
            }
        }
    }


    private void 아이템그냥삭제(ItemStack itemStack)
    {
        stacks[itemStack.Origin.key].Remove(itemStack);
    }

    public class ItemStack // 풀로 관리하기
    {
        public Data_Foods Origin { get; set; }
        public int Count { get; private set; }

        public ItemStack(Data_Foods data_Foods, int amount)
        {
            Origin = data_Foods;
            Count = amount;
        }

        public int Add(int amount, int maxCount)
        {
            int space = maxCount - Count;
            int toAdd = Mathf.Min(space, amount);
            Count += toAdd;
            return amount - toAdd; // 남은 갯수.
        }

        public int Subtract(int amount)
        {
            int toSubtract = Mathf.Min(amount, Count);
            Count -= toSubtract;
            return amount - toSubtract;
        }

        public int GetSpace(int maxCount)
        {
            return maxCount - Count;
        }


    }
}
