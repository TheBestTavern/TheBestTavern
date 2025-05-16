using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerGameData
{
    public PlayerTimeData playerTimeData = new();
    public PlayerInvenData playerInvenData = new();
}

[System.Serializable]
public class PlayerTimeData
{
    public int year;
    public int month;
    public int day;
    public bool isLeapYear;
    public bool isLeapMonth;

    public void SetPlayerTimeData(int year, int month, int day, bool isLeapMonth = false)
    {
        this.year = year;
        this.month = month;
        this.day = day;
        this.isLeapMonth = isLeapMonth;
    }
}

[System.Serializable]
public class PlayerInvenData
{
    public List<InvenData> ItemList = new();

    public void SetPlayerInvenData(Dictionary<int, ItemStack> ItemStack)
    {
        foreach(var item in ItemStack)
        {
            InvenData invenData = new InvenData(item.Value.Origin, item.Value.Count, item.Value.ID);
            ItemList.Add(invenData);
        }
    }
}

[System.Serializable]
public class InvenData
{
    public Data_Foods Origin;
    public int Count;
    public int ID;

    public InvenData(Data_Foods data_Foods, int Count, int ID)
    {
        Origin = data_Foods;
        this.Count = Count;
        this.ID = ID;
    }
}