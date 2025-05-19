using System;
using System.Collections.Generic;
using static Cinemachine.CinemachineTriggerAction.ActionSettings;

public class ItemStackManager : MonoSingleton<ItemStackManager>
{
    public Stack<int> IDs { get; private set; } = new();
    public Dictionary<int, ItemStack> AllItemStack { get; private set; } = new();

    private int idRangeMin = 1100000;
    private int idRangeMax = 1199999;

    public override void Init()
    {
        if (_isInitialized) return;
        base.Init();

        DontDestroyOnLoad(this);
        for (int i = idRangeMax; i >= idRangeMin; i--)
        {
            IDs.Push(i);
        }
    }

    public void ApplyLoadData(Stack<int> IDs, Dictionary<int, ItemStack> AllItemStack)
    {
        this.IDs = IDs;
        this.AllItemStack = AllItemStack;
    }

    public ItemStack InstantiateItem(Data_Foods data_Foods, int amount, Action<int> removeItem, Action<int> ChangeCount)
    {
        ItemStack item = new(data_Foods, amount, IDs.Pop(), ReCoverID, removeItem, ChangeCount);
        AllItemStack.Add(item.ID, item);
        return item;
    }

    public void ReCoverID(int id)
    {
        AllItemStack.Remove(id);
        IDs.Push(id);
    }
}
