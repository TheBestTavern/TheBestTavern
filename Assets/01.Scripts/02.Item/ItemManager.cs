using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoSingleton<ItemManager>
{
    public Stack<int> IDs { get; private set; }

    private Item InstantiateItem(int itemKey, int count)
    {
        Item item = new();
        item.Init(Data.GetRawItem(itemKey), ReCoverID, IDs.Pop(), count);
        return item;
    }

    public void ReCoverID(int id)
    {
        IDs.Push(id);
    }

}