using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerGameData
{
    public LunarDateTime today = new();

    public Stack<int> IDs = new();

    //public Dictionary<int, ItemStack> AllItemStack = new();

    public Dictionary<int, ItemRecord> itemRecords = new();

    public Dictionary<int, NPC> AllNPC = new();

    public Dictionary<int, Quest> AllQuests = new();
    public List<int> AcceptedQuests = new();
    public Dictionary<int, SuccessDegree> OnceSuccessQuests = new();
    public List<(int, SuccessDegree)> JustCompleteQuests = new();
    public List<int> TodayAvailableQuest = new();
    public Queue<(int questID, int itemID)> QuestCheckQueue = new();

    public DesignEnums.SeasonType? CurrentSeasonType;

    public PlayerInvenData playerInvenData = new();

    public void SetSaveData()
    {
        today = TimerManager.Instance.GetToday();

        IDs = ItemStackManager.Instance.IDs;

        //AllItemStack = ItemStackManager.Instance.AllItemStack;

        itemRecords = ItemRecordManager.Instance.itemRecords;

        AllNPC = NPCManager.Instance.AllNPC;

        AllQuests = QuestManager.Instance.AllQuests;
        AcceptedQuests = QuestManager.Instance.AcceptedQuests;
        OnceSuccessQuests = QuestManager.Instance.OnceSuccessQuests;
        JustCompleteQuests = QuestManager.Instance.JustCompleteQuests;
        TodayAvailableQuest = QuestManager.Instance.TodayAvailableQuest;
        QuestCheckQueue = QuestManager.Instance.QuestCheckQueue;

        CurrentSeasonType = CalendarManager.Instance.CurrentSeasonType;

        playerInvenData.SetPlayerInvenData(InventoryManager.Instance.Invens[InvenType.Player].model.ID2ItemStack);
    }

    [System.Serializable]
    public class PlayerInvenData
    {
        public List<PlayerItemData> ItemList = new();

        public void SetPlayerInvenData(Dictionary<int, ItemStack> ItemStack)
        {
            ItemList.Clear();
            foreach (var item in ItemStack)
            {
                PlayerItemData invenData = new PlayerItemData(item.Value.Origin, item.Value.Count, item.Value.ID);
                ItemList.Add(invenData);
            }
        }
    }

    [System.Serializable]
    public class PlayerItemData
    {
        public Data_Foods Origin;
        public int Count;
        public int ID;

        public PlayerItemData(Data_Foods origin, int count, int iD)
        {
            Origin = origin;
            Count = count;
            ID = iD;
        }
    }
}
