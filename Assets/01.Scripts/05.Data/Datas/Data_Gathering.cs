using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

[Serializable]
public class Data_Gathering
{
    /// <summary>
    /// 고유번호
    /// </summary>
    public int key;

    /// <summary>
    /// 지역
    /// </summary>
    public DesignEnums.RegionType condition_region;

    /// <summary>
    /// 생태
    /// </summary>
    public DesignEnums.BiomeType condition_biome;

    /// <summary>
    /// 계절
    /// </summary>
    public DesignEnums.SeasonType condition_season;

    /// <summary>
    /// 확률
    /// </summary>
    public DesignEnums.ChanceType condition_chance;

    /// <summary>
    /// 얻을수있는아이템ID
    /// </summary>
    public List<int> availableFood;

}
public class Data_GatheringLoader
{
    public List<Data_Gathering> ItemsList { get; private set; }
    public Dictionary<int, Data_Gathering> ItemsDict { get; private set; }

    public Data_GatheringLoader(string path = "JSON/Data_Gathering")
    {
        string jsonData;
        jsonData = Resources.Load<TextAsset>(path).text;
        ItemsList = JsonUtility.FromJson<Wrapper>(jsonData).Items;
        ItemsDict = new Dictionary<int, Data_Gathering>();
        foreach (var item in ItemsList)
        {
            ItemsDict.Add(item.key, item);
        }
    }

    [Serializable]
    private class Wrapper
    {
        public List<Data_Gathering> Items;
    }

    public Data_Gathering GetByKey(int key)
    {
        if (ItemsDict.ContainsKey(key))
        {
            return ItemsDict[key];
        }
        return null;
    }
    public Data_Gathering GetByIndex(int index)
    {
        if (index >= 0 && index < ItemsList.Count)
        {
            return ItemsList[index];
        }
        return null;
    }
}
