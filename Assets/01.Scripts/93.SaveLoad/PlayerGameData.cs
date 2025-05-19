using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerGameData
{
    public PlayerTimeData playerTimeData = new();
    public PlayerInvenData playerInvenData = new();
    public PlayerQuestData playerQuestData = new();
}

[System.Serializable]
public class PlayerTimeData
{
    public int year;
    public int month;
    public int day;
    public bool isLeapYear;
    public bool isLeapMonth;

    public void SetPlayerTimeData(int year, int month, int day, bool isLeapMonth = false)
    {
        this.year = year;
        this.month = month;
        this.day = day;
        isLeapYear = Extensions.lunarCalendar.IsLeapYear(year);
        this.isLeapMonth = isLeapMonth;
    }
}

[System.Serializable]
public class PlayerInvenData
{
    public List<SaveInvenData> ItemList = new();

    public void SetPlayerInvenData(Dictionary<int, ItemStack> ItemStack)
    {
        foreach(var item in ItemStack)
        {
            SaveInvenData invenData = new SaveInvenData(item.Value.Origin, item.Value.Count, item.Value.ID);
            ItemList.Add(invenData);
        }
    }
}

[System.Serializable]
public class SaveInvenData
{
    public Data_Foods Origin;
    public int Count;
    public int ID;

    public SaveInvenData(Data_Foods data_Foods, int Count, int ID)
    {
        Origin = data_Foods;
        this.Count = Count;
        this.ID = ID;
    }
}


[System.Serializable]
public class PlayerQuestData
{
    public Dictionary<int, PlayerTimeData> acceptedQuestDic = new();
    public Dictionary<int, PlayerTimeData> triggerQuestDaysDic = new();
    public Dictionary<int, SuccessDegree> onceCompletedQuests = new();
    public List<int> justCompleteQuests = new();
    public void SetPlayerQuestData(List<int> acceptedQuests, Dictionary<int, SuccessDegree> onceCompletedQuests, List<int> justCompleteQuests)
    {
        acceptedQuestDic.Clear();
        triggerQuestDaysDic.Clear();
        onceCompletedQuests.Clear();
        justCompleteQuests.Clear();

        foreach (var questID in acceptedQuests)
        {
            LunarDateTime acceptedDate = (LunarDateTime)QuestManager.Instance.AllQuests[questID].AcceptedDate;
            PlayerTimeData accetedQuestDate = new PlayerTimeData();
            accetedQuestDate.SetPlayerTimeData(acceptedDate.year, acceptedDate.month, acceptedDate.day, acceptedDate.isLeapMonth);
            acceptedQuestDic[questID] = accetedQuestDate;

            LunarDateTime triggerDate = (LunarDateTime)QuestManager.Instance.AllQuests[questID].TriggerDate;
            PlayerTimeData triggerQuestDate = new PlayerTimeData();
            triggerQuestDate.SetPlayerTimeData(triggerDate.year, triggerDate.month, triggerDate.day, triggerDate.isLeapMonth);
            triggerQuestDaysDic[questID] = triggerQuestDate;
        }

        this.onceCompletedQuests = onceCompletedQuests;
        this.justCompleteQuests = justCompleteQuests;
    }
}