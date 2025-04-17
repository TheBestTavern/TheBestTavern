using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class Data_Quest
{
    /// <summary>
    /// 퀘스트 ID (INT)
    /// </summary>
    public int key;

    /// <summary>
    /// 퀘스트 이름 (VARCHAR 100)
    /// </summary>
    public string name;

    /// <summary>
    /// NPC ID (INT)
    /// </summary>
    public int givingNPC;

    /// <summary>
    /// 보상 재료 ID
    /// </summary>
    public int compensationID;

    /// <summary>
    /// 요리 속성
    /// </summary>
    public string dishProperty;

    /// <summary>
    /// 요구 조건 설명
    /// </summary>
    public string description;

    /// <summary>
    /// 조건 타겟 npc
    /// </summary>
    public int conditionNPC;

    /// <summary>
    /// 조건 호감도
    /// </summary>
    public float conditionFavorability;

}
public class Data_QuestLoader
{
    public List<Data_Quest> ItemsList { get; private set; }
    public Dictionary<int, Data_Quest> ItemsDict { get; private set; }

    public Data_QuestLoader(string path = "JSON/Data_Quest")
    {
        string jsonData;
        jsonData = Resources.Load<TextAsset>(path).text;
        ItemsList = JsonUtility.FromJson<Wrapper>(jsonData).Items;
        ItemsDict = new Dictionary<int, Data_Quest>();
        foreach (var item in ItemsList)
        {
            ItemsDict.Add(item.key, item);
        }
    }

    [Serializable]
    private class Wrapper
    {
        public List<Data_Quest> Items;
    }

    public Data_Quest GetByKey(int key)
    {
        if (ItemsDict.ContainsKey(key))
        {
            return ItemsDict[key];
        }
        return null;
    }
    public Data_Quest GetByIndex(int index)
    {
        if (index >= 0 && index < ItemsList.Count)
        {
            return ItemsList[index];
        }
        return null;
    }
}
