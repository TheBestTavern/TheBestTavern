using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GatheringInventorySlot : MonoBehaviour
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemCountText;
    public Data_Foods data_Foods;
    public int itemCount = 0;

    public void SetFoodData(Data_Foods data)
    {
        data_Foods = data;
        //itemIcon = data_Foods.icon;
        itemCountText.gameObject.SetActive(true);
        UpdateFoodCount();
    }

    public void UpdateFoodCount()
    {
        itemCount++;
        itemCountText.text = itemCount.ToString();
    }
}
