using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InvenType
{
    Player,
    Gathering,
}

public class InventoryManager : MonoSingleton<InventoryManager>
{
    public override void Init()
    {
        if (_isInitialized) return;
        base.Init();

        DontDestroyOnLoad(this);
        InventoryManager.Instance.CreateInventory(InvenType.Player, 54, 10);
        InventoryManager.Instance.CreateInventory(InvenType.Gathering, 6, 5);
    }

    public Dictionary<InvenType, InventoryController> Invens { get; private set; } = new();
    public void CreateInventory(InvenType invenType, int slotCount, int maxStackSize)
    {
        InventoryController controller = new();
        controller.Init(invenType, slotCount, maxStackSize);
        Invens.Add(invenType, controller);
    }

    public InventoryView[] FindInventoryView()
    {
        return FindObjectsOfType<InventoryView>();
    }
}
