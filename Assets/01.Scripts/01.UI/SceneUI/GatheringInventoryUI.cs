using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using static System.Collections.Specialized.BitVector32;

/// <summary>
/// 채집씬 인벤토리 UI
/// </summary>
public class GatheringInventoryUI : MonoBehaviour
{
    [SerializeField] private GatheringInventorySlot[] slots;

    public void SetSlot(Data_Foods foodData)
    {
        foreach (var slot in slots)
        {
            if (slot.data_Foods.key == 0)
            {
                slot.SetFoodData(foodData);
                return;
            }
            if(slot.data_Foods.key == foodData.key)
            {
                if (slot.itemCount < 10)
                {
                    slot.UpdateFoodCount();
                    return;
                }
            }
        }

    }
}
