using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class Data_CookingSteps
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
public class Data_CookingStepsLoader
{
    public List<Data_CookingSteps> ItemsList { get; private set; }
    public Dictionary<int, Data_CookingSteps> ItemsDict { get; private set; }

    public Data_CookingStepsLoader(string path = "JSON/Data_CookingSteps")
    {
        string jsonData;
        jsonData = Resources.Load<TextAsset>(path).text;
        ItemsList = JsonUtility.FromJson<Wrapper>(jsonData).Items;
        ItemsDict = new Dictionary<int, Data_CookingSteps>();
        foreach (var item in ItemsList)
        {
            ItemsDict.Add(item.key, item);
        }
    }

    [Serializable]
    private class Wrapper
    {
        public List<Data_CookingSteps> Items;
    }

    public Data_CookingSteps GetByKey(int key)
    {
        if (ItemsDict.ContainsKey(key))
        {
            return ItemsDict[key];
        }
        return null;
    }
    public Data_CookingSteps GetByIndex(int index)
    {
        if (index >= 0 && index < ItemsList.Count)
        {
            return ItemsList[index];
        }
        return null;
    }
}
