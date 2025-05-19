using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class Data_Book_Dish
{
    /// <summary>
    /// 음식군ID
    /// </summary>
    public int key;

    /// <summary>
    /// 레시피명 (VARCHAR(50)
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
public class Data_Book_DishLoader
{
    public List<Data_Book_Dish> ItemsList { get; private set; }
    public Dictionary<int, Data_Book_Dish> ItemsDict { get; private set; }

    public Data_Book_DishLoader(string path = "JSON/Data_Book_Dish")
    {
        string jsonData;
        jsonData = Resources.Load<TextAsset>(path).text;
        ItemsList = JsonUtility.FromJson<Wrapper>(jsonData).Items;
        ItemsDict = new Dictionary<int, Data_Book_Dish>();
        foreach (var item in ItemsList)
        {
            ItemsDict.Add(item.key, item);
        }
    }

    [Serializable]
    private class Wrapper
    {
        public List<Data_Book_Dish> Items;
    }

    public Data_Book_Dish GetByKey(int key)
    {
        if (ItemsDict.ContainsKey(key))
        {
            return ItemsDict[key];
        }
        return null;
    }
    public Data_Book_Dish GetByIndex(int index)
    {
        if (index >= 0 && index < ItemsList.Count)
        {
            return ItemsList[index];
        }
        return null;
    }
}
