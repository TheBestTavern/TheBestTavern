using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class Data_Gathering_Region
{
    /// <summary>
    /// 고유번호
    /// </summary>
    public int key;

    /// <summary>
    /// 채집 가능 지역
    /// </summary>
    public DesignEnums.Region region;

    /// <summary>
    /// 얻을 수 있는 아이템 ID
    /// </summary>
    public List<int> availableFood;

}
public class Data_Gathering_RegionLoader
{
    public List<Data_Gathering_Region> ItemsList { get; private set; }
    public Dictionary<int, Data_Gathering_Region> ItemsDict { get; private set; }

    public Data_Gathering_RegionLoader(string path = "JSON/Data_Gathering_Region")
    {
        string jsonData;
        jsonData = Resources.Load<TextAsset>(path).text;
        ItemsList = JsonUtility.FromJson<Wrapper>(jsonData).Items;
        ItemsDict = new Dictionary<int, Data_Gathering_Region>();
        foreach (var item in ItemsList)
        {
            ItemsDict.Add(item.key, item);
        }
    }

    [Serializable]
    private class Wrapper
    {
        public List<Data_Gathering_Region> Items;
    }

    public Data_Gathering_Region GetByKey(int key)
    {
        if (ItemsDict.ContainsKey(key))
        {
            return ItemsDict[key];
        }
        return null;
    }
    public Data_Gathering_Region GetByIndex(int index)
    {
        if (index >= 0 && index < ItemsList.Count)
        {
            return ItemsList[index];
        }
        return null;
    }
}
