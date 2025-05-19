using System;
using Newtonsoft.Json;
using UnityEngine;

public class ItemStack // 풀로 관리하기
{
    [JsonProperty]
    public Data_Foods Origin { get; set; }
    [JsonProperty]
    public int Count { get; private set; }
    [JsonProperty]
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