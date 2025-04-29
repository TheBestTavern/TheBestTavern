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

        isDontDestroyOnLoad = true;
    }

    public Dictionary<InvenType, InventoryController> Invens { get; private set; } = new();
    public void CreateInventory(InvenType invenType, int slotCount, int maxStackSize)
    {
        InventoryController controller = new();
        controller.Init(invenType, slotCount, maxStackSize);
        Invens.Add(InvenType.Player, controller);
    }

    public InventoryView[] FindInventoryView()
    {
        return FindObjectsOfType<InventoryView>();
    }


}
