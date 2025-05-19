using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class Data_Book_Ingredient
{
    /// <summary>
    /// 음식군id
    /// </summary>
    public int key;

    /// <summary>
    /// 음식명
    /// </summary>
    public string name;

    /// <summary>
    /// 영어이름
    /// </summary>
    public string englishName;

    /// <summary>
    /// 지역
    /// </summary>
    public List<DesignEnums.RegionType> region;

    /// <summary>
    /// 생태
    /// </summary>
    public DesignEnums.BiomeType biome;

    /// <summary>
    /// 계절
    /// </summary>
    public List<DesignEnums.SeasonType> season;

    /// <summary>
    /// 설명
    /// </summary>
    public string description;

}
public class Data_Book_IngredientLoader
{
    public List<Data_Book_Ingredient> ItemsList { get; private set; }
    public Dictionary<int, Data_Book_Ingredient> ItemsDict { get; private set; }

    public Data_Book_IngredientLoader(string path = "JSON/Data_Book_Ingredient")
    {
        string jsonData;
        jsonData = Resources.Load<TextAsset>(path).text;
        ItemsList = JsonUtility.FromJson<Wrapper>(jsonData).Items;
        ItemsDict = new Dictionary<int, Data_Book_Ingredient>();
        foreach (var item in ItemsList)
        {
            ItemsDict.Add(item.key, item);
        }
    }

    [Serializable]
    private class Wrapper
    {
        public List<Data_Book_Ingredient> Items;
    }

    public Data_Book_Ingredient GetByKey(int key)
    {
        if (ItemsDict.ContainsKey(key))
        {
            return ItemsDict[key];
        }
        return null;
    }
    public Data_Book_Ingredient GetByIndex(int index)
    {
        if (index >= 0 && index < ItemsList.Count)
        {
            return ItemsList[index];
        }
        return null;
    }
}
