using UnityEngine;

public class ItemManager : MonoSingleton<ItemManager>
{
    public override void Init()
    {
        if(_isInitialized) return;
        base.Init();

        InstantiateItems();
    }

    private void InstantiateItems()
    {
        //foreach(DataManager.Instance.DataLoader_Foods.ItemsList)
    }
}