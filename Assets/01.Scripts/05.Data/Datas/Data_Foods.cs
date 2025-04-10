using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class Data_Foods
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
    public int foodCategory;

    /// <summary>
    /// 등급
    /// </summary>
    public string grade;

    /// <summary>
    /// 습득처
    /// </summary>
    public string getRoute;

}
public class Data_FoodsLoader
{
    public List<Data_Foods> ItemsList { get; private set; }
    public Dictionary<int, Data_Foods> ItemsDict { get; private set; }

    public Data_FoodsLoader(string path = "JSON/Data_Foods")
    {
        string jsonData;
        jsonData = Resources.Load<TextAsset>(path).text;
        ItemsList = JsonUtility.FromJson<Wrapper>(jsonData).Items;
        ItemsDict = new Dictionary<int, Data_Foods>();
        foreach (var item in ItemsList)
        {
            ItemsDict.Add(item.key, item);
        }
    }

    [Serializable]
    private class Wrapper
    {
        public List<Data_Foods> Items;
    }

    public Data_Foods GetByKey(int key)
    {
        if (ItemsDict.ContainsKey(key))
        {
            return ItemsDict[key];
        }
        return null;
    }
    public Data_Foods GetByIndex(int index)
    {
        if (index >= 0 && index < ItemsList.Count)
        {
            return ItemsList[index];
        }
        return null;
    }
}
