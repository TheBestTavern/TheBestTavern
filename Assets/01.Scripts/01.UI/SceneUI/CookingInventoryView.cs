using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 요리 씬 인벤토리 UI
/// </summary>
public class CookingInventoryView : InventoryViewLoose
{
    public override void 아이템타게팅(int index)
    {
        base.아이템타게팅(index);
        CookingMiniGameManager.Instance.SetMiniGameItem(index2Slots[index].GetSlotItem());
    }
}
