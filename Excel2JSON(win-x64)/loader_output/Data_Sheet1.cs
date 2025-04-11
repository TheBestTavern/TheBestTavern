using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class Data_Sheet1
{
    /// <summary>
    /// 고유번호
    /// </summary>
    public int key;

    /// <summary>
    /// 음식명
    /// </summary>
    public string name;

    /// <summary>
    /// 음식군
    /// </summary>
    public int FoodCategory;

    /// <summary>
    /// 등급
    /// </summary>
    public DesignEnums.Grade grade;

    /// <summary>
    /// 습득처
    /// </summary>
    public List<DesignEnums.Route> getRoute;

}
public class Data_Sheet1Loader
{
    public List<Data_Sheet1> ItemsList { get; private set; }
    public Dictionary<int, Data_Sheet1> ItemsDict { get; private set; }

    public Data_Sheet1Loader(string path = "JSON/Data_Sheet1")
    {
        string jsonData;
        jsonData = Resources.Load<TextAsset>(path).text;
        ItemsList = JsonUtility.FromJson<Wrapper>(jsonData).Items;
        ItemsDict = new Dictionary<int, Data_Sheet1>();
        foreach (var item in ItemsList)
        {
            ItemsDict.Add(item.key, item);
        }
    }

    [Serializable]
    private class Wrapper
    {
        public List<Data_Sheet1> Items;
    }

    public Data_Sheet1 GetByKey(int key)
    {
        if (ItemsDict.ContainsKey(key))
        {
            return ItemsDict[key];
        }
        return null;
    }
    public Data_Sheet1 GetByIndex(int index)
    {
        if (index >= 0 && index < ItemsList.Count)
        {
            return ItemsList[index];
        }
        return null;
    }
}
