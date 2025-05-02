
using System;
using Unity.VisualScripting;
using UnityEngine;

public class Item
{
    public Data_Foods origin { get; private set; }
    public int Count { get; private set; }
    public int ID { get; private set; }
    public Action<int> OnDestroy;
    public void Init(Data_Foods origin, Action<int> onDestroy, int id, int count)
    {
        ID = id;
        this.origin = origin;
        this.Count = count;
        this.OnDestroy = onDestroy;
    }

    public void Destroy()
    {
        OnDestroy?.Invoke(ID);
    }
}