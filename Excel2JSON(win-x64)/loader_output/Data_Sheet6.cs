using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class Data_Sheet6
{
    /// <summary>
    /// 레시피ID (INT)
    /// </summary>
    public int key;

    /// <summary>
    /// 레시피명 (VARCHAR(50)
    /// </summary>
    public string name;

    /// <summary>
    /// 결과 요리 음식군 번호
    /// </summary>
    public int resultCategory;

    /// <summary>
    /// 재료가 될 음식군 번호 
    /// </summary>
    public List<int> ingredients;

    /// <summary>
    /// 도구
    /// </summary>
    public int usingTool;

    /// <summary>
    /// 요리 속성
    /// </summary>
    public string cookingProperty;

}
public class Data_Sheet6Loader
{
    public List<Data_Sheet6> ItemsList { get; private set; }
    public Dictionary<int, Data_Sheet6> ItemsDict { get; private set; }

    public Data_Sheet6Loader(string path = "JSON/Data_Sheet6")
    {
        string jsonData;
        jsonData = Resources.Load<TextAsset>(path).text;
        ItemsList = JsonUtility.FromJson<Wrapper>(jsonData).Items;
        ItemsDict = new Dictionary<int, Data_Sheet6>();
        foreach (var item in ItemsList)
        {
            ItemsDict.Add(item.key, item);
        }
    }

    [Serializable]
    private class Wrapper
    {
        public List<Data_Sheet6> Items;
    }

    public Data_Sheet6 GetByKey(int key)
    {
        if (ItemsDict.ContainsKey(key))
        {
            return ItemsDict[key];
        }
        return null;
    }
    public Data_Sheet6 GetByIndex(int index)
    {
        if (index >= 0 && index < ItemsList.Count)
        {
            return ItemsList[index];
        }
        return null;
    }
}
