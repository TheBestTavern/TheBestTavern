using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using static Cinemachine.CinemachineTriggerAction.ActionSettings;

public class ItemStackManager : MonoSingleton<ItemStackManager>
{
    //public Stack<int> IDs { get; private set; } = new();
    [JsonProperty]
    public List<int> IDsForSerialization = new(); // 저장시 사용
    [JsonIgnore]
    public Stack<int> IDs { get; private set; }= new(); // 역순 주의
    public Dictionary<int, ItemStack> AllItemStack { get; private set; } = new();

    private int idRangeMin = 1100000;
    private int idRangeMax = 1119999;

    public override void Init()
    {
        if (_isInitialized) return;
        base.Init();

        DontDestroyOnLoad(this);
        for (int i = idRangeMax; i >= idRangeMin; i--)
        {
            IDs.Push(i);
        }

        //EventBus.Subscribe<ItemStackOnZeroEvent>(ReCoverID);
    }

    public void ApplyLoadData(List<int> IDsforSerial, Dictionary<int, ItemStack> AllItemStack)
    {
        IDsforSerial.Reverse();
        IDs = new(IDsforSerial);
        this.AllItemStack = AllItemStack;
    }

    public ItemStack InstantiateItem(Data_Foods data_Foods, int amount, InvenType invenType)
    {
        ItemStack item = new(data_Foods.key, amount, IDs.Pop(), invenType);
        AllItemStack.Add(item.ID, item);
        return item;
    }

    public void ReCoverID(int id)
    {
        AllItemStack.Remove(id);
        IDs.Push(id);
    }

    //public void ReCoverID(ItemStackOnZeroEvent evt)
    //{
    //    ReCoverID(evt.ID);
    //}

    protected override void OnDestroy()
    {
        //EventBus.UnSubscribe<ItemStackOnZeroEvent>(ReCoverID);
    }
}
