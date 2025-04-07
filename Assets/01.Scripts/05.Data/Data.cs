using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class Data
{
    /// <summary>
    /// 고유번호
    /// </summary>
    public int key;

    /// <summary>
    /// 재료명
    /// </summary>
    public string name;

    /// <summary>
    /// 등급
    /// </summary>
    public DesignEnums.Grade grade;

    /// <summary>
    /// 채집 가능 지역
    /// </summary>
    public List<DesignEnums.Region> availableRegion;

    /// <summary>
    /// 채집 영역
    /// </summary>
    public DesignEnums.Biome availableBiome;

    /// <summary>
    /// 해외 식자재
    /// </summary>
    public DesignEnums.Foreign foreignIngredients;

    /// <summary>
    /// 채집 가능 계절
    /// </summary>
    public List<DesignEnums.Season> availableSeason;

    /// <summary>
    /// 채집 확률
    /// </summary>
    public DesignEnums.Chance chance;

}
public class DataLoader
{
    public List<Data> ItemsList { get; private set; }
    public Dictionary<int, Data> ItemsDict { get; private set; }

    public DataLoader(string path = "JSON/Data")
    {
        string jsonData;
        jsonData = Resources.Load<TextAsset>(path).text;
        ItemsList = JsonUtility.FromJson<Wrapper>(jsonData).Items;
        ItemsDict = new Dictionary<int, Data>();
        foreach (var item in ItemsList)
        {
            ItemsDict.Add(item.key, item);
        }
    }

    [Serializable]
    private class Wrapper
    {
        public List<Data> Items;
    }

    public Data GetByKey(int key)
    {
        if (ItemsDict.ContainsKey(key))
        {
            return ItemsDict[key];
        }
        return null;
    }
    public Data GetByIndex(int index)
    {
        if (index >= 0 && index < ItemsList.Count)
        {
            return ItemsList[index];
        }
        return null;
    }
}
