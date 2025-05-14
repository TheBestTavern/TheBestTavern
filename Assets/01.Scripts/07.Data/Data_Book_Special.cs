using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class Data_Book_Special
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
    /// 주는 NPC 코드
    /// </summary>
    public int givingNPCID;

    /// <summary>
    /// 주는 NPC 이름
    /// </summary>
    public string givingNPCName;

    /// <summary>
    /// 설명
    /// </summary>
    public string description;

}
public class Data_Book_SpecialLoader
{
    public List<Data_Book_Special> ItemsList { get; private set; }
    public Dictionary<int, Data_Book_Special> ItemsDict { get; private set; }

    public Data_Book_SpecialLoader(string path = "JSON/Data_Book_Special")
    {
        string jsonData;
        jsonData = Resources.Load<TextAsset>(path).text;
        ItemsList = JsonUtility.FromJson<Wrapper>(jsonData).Items;
        ItemsDict = new Dictionary<int, Data_Book_Special>();
        foreach (var item in ItemsList)
        {
            ItemsDict.Add(item.key, item);
        }
    }

    [Serializable]
    private class Wrapper
    {
        public List<Data_Book_Special> Items;
    }

    public Data_Book_Special GetByKey(int key)
    {
        if (ItemsDict.ContainsKey(key))
        {
            return ItemsDict[key];
        }
        return null;
    }
    public Data_Book_Special GetByIndex(int index)
    {
        if (index >= 0 && index < ItemsList.Count)
        {
            return ItemsList[index];
        }
        return null;
    }
}
