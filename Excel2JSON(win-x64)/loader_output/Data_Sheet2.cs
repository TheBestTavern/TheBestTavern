using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class Data_Sheet2
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
public class Data_Sheet2Loader
{
    public List<Data_Sheet2> ItemsList { get; private set; }
    public Dictionary<int, Data_Sheet2> ItemsDict { get; private set; }

    public Data_Sheet2Loader(string path = "JSON/Data_Sheet2")
    {
        string jsonData;
        jsonData = Resources.Load<TextAsset>(path).text;
        ItemsList = JsonUtility.FromJson<Wrapper>(jsonData).Items;
        ItemsDict = new Dictionary<int, Data_Sheet2>();
        foreach (var item in ItemsList)
        {
            ItemsDict.Add(item.key, item);
        }
    }

    [Serializable]
    private class Wrapper
    {
        public List<Data_Sheet2> Items;
    }

    public Data_Sheet2 GetByKey(int key)
    {
        if (ItemsDict.ContainsKey(key))
        {
            return ItemsDict[key];
        }
        return null;
    }
    public Data_Sheet2 GetByIndex(int index)
    {
        if (index >= 0 && index < ItemsList.Count)
        {
            return ItemsList[index];
        }
        return null;
    }
}
