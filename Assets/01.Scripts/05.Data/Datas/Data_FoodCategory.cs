using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class Data_FoodCategory
{
    /// <summary>
    /// 음식군
    /// </summary>
    public int key;

    /// <summary>
    /// 전설음식명
    /// </summary>
    public string goodFoodName;

    /// <summary>
    /// 평범음식명
    /// </summary>
    public string sosoFoodName;

    /// <summary>
    /// 망가진음식명
    /// </summary>
    public string badFoodName;

    /// <summary>
    /// 전설음식id
    /// </summary>
    public int goodFoodID;

    /// <summary>
    /// 평범음식id
    /// </summary>
    public int sosoFoodID;

    /// <summary>
    /// 망가진음식id
    /// </summary>
    public int badFoodID;

}
public class Data_FoodCategoryLoader
{
    public List<Data_FoodCategory> ItemsList { get; private set; }
    public Dictionary<int, Data_FoodCategory> ItemsDict { get; private set; }

    public Data_FoodCategoryLoader(string path = "JSON/Data_FoodCategory")
    {
        string jsonData;
        jsonData = Resources.Load<TextAsset>(path).text;
        ItemsList = JsonUtility.FromJson<Wrapper>(jsonData).Items;
        ItemsDict = new Dictionary<int, Data_FoodCategory>();
        foreach (var item in ItemsList)
        {
            ItemsDict.Add(item.key, item);
        }
    }

    [Serializable]
    private class Wrapper
    {
        public List<Data_FoodCategory> Items;
    }

    public Data_FoodCategory GetByKey(int key)
    {
        if (ItemsDict.ContainsKey(key))
        {
            return ItemsDict[key];
        }
        return null;
    }
    public Data_FoodCategory GetByIndex(int index)
    {
        if (index >= 0 && index < ItemsList.Count)
        {
            return ItemsList[index];
        }
        return null;
    }
}
