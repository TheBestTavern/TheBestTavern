using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class InventoryModel
{
    public Dictionary<int, List<int>> foodType2IDs = new();  // <Data_Foods.key, ID리스트>
    public Dictionary<int, ItemStack> ID2stack = new(); // <ID, 아이템 스택>
    int SlotMaxCount { get; set; } // 슬롯 최대 갯수
    int PerStackMaxCount { get; set; } // 스택 당 아이템 갯수

    public Action<int> OnChanged;

    public void Init(int slotMaxCount, int perStackMaxCount, Action<int> OnModelChanged)
    {
        SlotMaxCount = slotMaxCount;
        PerStackMaxCount = perStackMaxCount;
        OnChanged = OnModelChanged;
    }

    public bool 아이템검사후추가(Data_Foods data_Foods, int amount)
    {
        // 1. 여유 공간 확인
        int space = 0;
        if (foodType2IDs.ContainsKey(data_Foods.key))
        {
            foreach (var id in foodType2IDs[data_Foods.key])
            {
                space += ID2stack[id].GetSpace(PerStackMaxCount);
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

        if (!foodType2IDs.ContainsKey(data_Foods.key))
        {
            foodType2IDs.Add(data_Foods.key, new List<int>());
        }
        List<int> IDList = foodType2IDs[data_Foods.key];

        foreach (int id in IDList)
        {
            remain = ID2stack[id].Add(amount, PerStackMaxCount);
        }

        while (remain > 0)
        {
            //var temp = new ItemStack(data_Foods, 0, 아이템삭제);
            var temp = ItemStackManager.Instance.InstantiateItem(data_Foods, 0, 아이템삭제, TriggerOnChange);
            remain = temp.Add(remain, PerStackMaxCount);
            IDList.Add(temp.ID);
        }
    }

    public bool 아이템감소(Data_Foods data_Foods, int amount)
    {
        // 1. 감소할만큼의 아이템이 있는지.
        int count = 0;
        if (foodType2IDs.ContainsKey(data_Foods.key))
        {
            foreach (int id in foodType2IDs[data_Foods.key])
            {
                count += ID2stack[id].Count;
            }
        }

        if (amount > count) return false;

        // 2. 감소시키기
        int remain = amount;
        foreach (int id in foodType2IDs[data_Foods.key])
        {
            remain = ID2stack[id].Subtract(remain);
            if (remain <= 0) break;
        }
        return true;
    }

    public int 여분의공간반환()
    {
        int stackCount = 0;
        foreach (var pair in foodType2IDs)
        {
            stackCount += pair.Value.Count;
        }

        return SlotMaxCount - stackCount;
    }

    public void 아이템정렬_합치기()
    {
        foreach (var pair in foodType2IDs)
        {
            int toMergeCount = 0;
            foreach (int id in pair.Value)
            {
                if (pair.Value.Count < 2) continue;
                if (ID2stack[id].Count != PerStackMaxCount)
                {
                    toMergeCount += ID2stack[id].Count;
                    ID2stack[id].TriggerOnDestroy();
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
        ItemStack itemStack = ID2stack[id];

        ID2stack.Remove(id);
        foodType2IDs[itemStack.Origin.key].Remove(id);
        if (foodType2IDs[itemStack.Origin.key].Count == 0) foodType2IDs.Remove(itemStack.Origin.key);
    }

    public void TriggerOnChange(int id)
    {
        OnChanged?.Invoke(id);
    }
}

public class ItemStack // 풀로 관리하기
{
    public Data_Foods Origin { get; set; }
    public int Count { get; private set; }
    public int ID { get; private set; }

    public Action<int> OnZero;
    public Action<int> OnChanged;

    public ItemStack(Data_Foods data_Foods, int amount, int id, Action<int> recoverID, Action<int> removeFromModel, Action<int> change)
    {
        Origin = data_Foods;
        Count = amount;
        ID = id;
        OnZero = removeFromModel;
        OnZero += recoverID;
        OnChanged = change;
    }

    public int Add(int amount, int maxCount)
    {
        int space = maxCount - Count;
        int toAdd = Mathf.Min(space, amount);
        Count += toAdd;
        TriggerOnChange();
        return amount - toAdd; // 남은 갯수.
    }

    public int Subtract(int amount)
    {
        int toSubtract = Mathf.Min(amount, Count);
        Count -= toSubtract;
        TriggerOnChange();
        if (Count == 0) TriggerOnDestroy();
        return amount - toSubtract;
    }

    public int GetSpace(int maxCount)
    {
        return maxCount - Count;
    }

    public void TriggerOnDestroy()
    {
        OnZero?.Invoke(ID);
    }

    public void TriggerOnChange()
    {
        OnChanged?.Invoke(ID);
    }
}

public class ItemStackManager : MonoSingleton<ItemStackManager>
{
    public Stack<int> IDs { get; private set; } = new();

    private int idRangeMin = 1100000;
    private int idRangeMax = 1200000;

    public override void Init()
    {
        if (_isInitialized) return;
        base.Init();

        for (int i = idRangeMin; i < idRangeMax; i++)
        {
            IDs.Push(i);
        }
    }

    public ItemStack InstantiateItem(Data_Foods data_Foods, int amount, Action<int> removeFromModel, Action<int> onChangeCount)
    {
        ItemStack item = new(data_Foods, amount, IDs.Pop(), ReCoverID, removeFromModel, onChangeCount);
        return item;
    }

    public void ReCoverID(int id)
    {
        IDs.Push(id);
    }
}
