using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class Data_NPC
{
    /// <summary>
    /// NPC ID (INT)
    /// </summary>
    public int key;

    /// <summary>
    /// NPC 이름 (VARCHAR(50) )
    /// </summary>
    public string name;

    /// <summary>
    /// 설화 출처 (VARCHAR (50) ) 
    /// </summary>
    public string story;

    /// <summary>
    /// 성격 (VARCHAR (50)
    /// </summary>
    public string personality;

    /// <summary>
    /// 선호 요리 속성
    /// </summary>
    public string taste;

    /// <summary>
    /// 기본 호감도
    /// </summary>
    public float favorability ;

    /// <summary>
    /// 비고
    /// </summary>
    public string speech;

    /// <summary>
    /// 부여 퀘스트
    /// </summary>
    public List<int> givingQuest;

}
public class Data_NPCLoader
{
    public List<Data_NPC> ItemsList { get; private set; }
    public Dictionary<int, Data_NPC> ItemsDict { get; private set; }

    public Data_NPCLoader(string path = "JSON/Data_NPC")
    {
        string jsonData;
        jsonData = Resources.Load<TextAsset>(path).text;
        ItemsList = JsonUtility.FromJson<Wrapper>(jsonData).Items;
        ItemsDict = new Dictionary<int, Data_NPC>();
        foreach (var item in ItemsList)
        {
            ItemsDict.Add(item.key, item);
        }
    }

    [Serializable]
    private class Wrapper
    {
        public List<Data_NPC> Items;
    }

    public Data_NPC GetByKey(int key)
    {
        if (ItemsDict.ContainsKey(key))
        {
            return ItemsDict[key];
        }
        return null;
    }
    public Data_NPC GetByIndex(int index)
    {
        if (index >= 0 && index < ItemsList.Count)
        {
            return ItemsList[index];
        }
        return null;
    }
}
