using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class Data_Book_Mix
{
    /// <summary>
    /// 음식군ID
    /// </summary>
    public int key;

    /// <summary>
    /// 음식명
    /// </summary>
    public string name;

    /// <summary>
    /// 결과 요리 영어 이름
    /// </summary>
    public string resultFoodEnglishName;

    /// <summary>
    /// 재료가 될 음식군 번호 
    /// </summary>
    public List<int> ingredients;

    /// <summary>
    /// 재료 이름
    /// </summary>
    public List<string> ingredientsName;

    /// <summary>
    /// 설명
    /// </summary>
    public string description;

}
public class Data_Book_MixLoader
{
    public List<Data_Book_Mix> ItemsList { get; private set; }
    public Dictionary<int, Data_Book_Mix> ItemsDict { get; private set; }

    public Data_Book_MixLoader(string path = "JSON/Data_Book_Mix")
    {
        string jsonData;
        jsonData = Resources.Load<TextAsset>(path).text;
        ItemsList = JsonUtility.FromJson<Wrapper>(jsonData).Items;
        ItemsDict = new Dictionary<int, Data_Book_Mix>();
        foreach (var item in ItemsList)
        {
            ItemsDict.Add(item.key, item);
        }
    }

    [Serializable]
    private class Wrapper
    {
        public List<Data_Book_Mix> Items;
    }

    public Data_Book_Mix GetByKey(int key)
    {
        if (ItemsDict.ContainsKey(key))
        {
            return ItemsDict[key];
        }
        return null;
    }
    public Data_Book_Mix GetByIndex(int index)
    {
        if (index >= 0 && index < ItemsList.Count)
        {
            return ItemsList[index];
        }
        return null;
    }
}
