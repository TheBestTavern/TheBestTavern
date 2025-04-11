using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaitInventory : MonoBehaviour
{
    public ThrowManager throwManager;

    public void SelectBait(int index)
    {
        throwManager.SetBaitIndex(index);
    }

}
