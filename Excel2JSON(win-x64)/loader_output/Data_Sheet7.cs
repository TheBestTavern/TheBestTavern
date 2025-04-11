using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class Data_Sheet7
{
    /// <summary>
    /// 조리 방식 (INT)
    /// </summary>
    public int key;

    /// <summary>
    /// 이름 ( VACHAR 50)
    /// </summary>
    public string name;

    /// <summary>
    /// 도구명 (VACHAR 20)
    /// </summary>
    public DesignEnums.CookingTool tool;

}
public class Data_Sheet7Loader
{
    public List<Data_Sheet7> ItemsList { get; private set; }
    public Dictionary<int, Data_Sheet7> ItemsDict { get; private set; }

    public Data_Sheet7Loader(string path = "JSON/Data_Sheet7")
    {
        string jsonData;
        jsonData = Resources.Load<TextAsset>(path).text;
        ItemsList = JsonUtility.FromJson<Wrapper>(jsonData).Items;
        ItemsDict = new Dictionary<int, Data_Sheet7>();
        foreach (var item in ItemsList)
        {
            ItemsDict.Add(item.key, item);
        }
    }

    [Serializable]
    private class Wrapper
    {
        public List<Data_Sheet7> Items;
    }

    public Data_Sheet7 GetByKey(int key)
    {
        if (ItemsDict.ContainsKey(key))
        {
            return ItemsDict[key];
        }
        return null;
    }
    public Data_Sheet7 GetByIndex(int index)
    {
        if (index >= 0 && index < ItemsList.Count)
        {
            return ItemsList[index];
        }
        return null;
    }
}
