using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class Data_Sheet4
{
    /// <summary>
    /// 고유번호
    /// </summary>
    public int key;

    /// <summary>
    /// 채집 가능 계절
    /// </summary>
    public DesignEnums.Season season;

    /// <summary>
    /// 얻을 수 있는 아이템 ID
    /// </summary>
    public List<int> availableFood;

}
public class Data_Sheet4Loader
{
    public List<Data_Sheet4> ItemsList { get; private set; }
    public Dictionary<int, Data_Sheet4> ItemsDict { get; private set; }

    public Data_Sheet4Loader(string path = "JSON/Data_Sheet4")
    {
        string jsonData;
        jsonData = Resources.Load<TextAsset>(path).text;
        ItemsList = JsonUtility.FromJson<Wrapper>(jsonData).Items;
        ItemsDict = new Dictionary<int, Data_Sheet4>();
        foreach (var item in ItemsList)
        {
            ItemsDict.Add(item.key, item);
        }
    }

    [Serializable]
    private class Wrapper
    {
        public List<Data_Sheet4> Items;
    }

    public Data_Sheet4 GetByKey(int key)
    {
        if (ItemsDict.ContainsKey(key))
        {
            return ItemsDict[key];
        }
        return null;
    }
    public Data_Sheet4 GetByIndex(int index)
    {
        if (index >= 0 && index < ItemsList.Count)
        {
            return ItemsList[index];
        }
        return null;
    }
}
