using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class Data_Recipes
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
public class Data_RecipesLoader
{
    public List<Data_Recipes> ItemsList { get; private set; }
    public Dictionary<int, Data_Recipes> ItemsDict { get; private set; }

    public Data_RecipesLoader(string path = "JSON/Data_Recipes")
    {
        string jsonData;
        jsonData = Resources.Load<TextAsset>(path).text;
        ItemsList = JsonUtility.FromJson<Wrapper>(jsonData).Items;
        ItemsDict = new Dictionary<int, Data_Recipes>();
        foreach (var item in ItemsList)
        {
            ItemsDict.Add(item.key, item);
        }
    }

    [Serializable]
    private class Wrapper
    {
        public List<Data_Recipes> Items;
    }

    public Data_Recipes GetByKey(int key)
    {
        if (ItemsDict.ContainsKey(key))
        {
            return ItemsDict[key];
        }
        return null;
    }
    public Data_Recipes GetByIndex(int index)
    {
        if (index >= 0 && index < ItemsList.Count)
        {
            return ItemsList[index];
        }
        return null;
    }
}
