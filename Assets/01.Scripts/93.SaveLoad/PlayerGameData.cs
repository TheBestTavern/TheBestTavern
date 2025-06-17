using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerGameData
{
    public LunarDateTime today = new();

    //public Stack<int> IDs = new();
    public List<int> IDsForSerialization = new();
    public Dictionary<int, ItemStack> AllItemStack = new();

    public Dictionary<int, ItemRecord> itemRecords = new();

    public Dictionary<int, NPC> AllNPC = new();

    public Dictionary<int, Quest> AllQuests = new();
    public List<int> AcceptedQuests = new();
    public Dictionary<int, SuccessDegree> OnceSuccessQuests = new();
    public List<(int, SuccessDegree)> JustCompleteQuests = new();
    public List<int> TodayAvailableQuest = new();
    //public Queue<(int questID, int itemID)> QuestCheckQueue = new();
    public List<(int questID, int itemID)> QuestCheckQueueForSerialization = new();
    public List<int> TodaySpawnNPC = new();

    public DesignEnums.SeasonType? CurrentSeasonType;

    public Dictionary<int, List<int>> foodKey2IDs = new();
    //public Dictionary<int, ItemStack> ID2ItemStack = new();
    public List<int> itemStackIDs = new();

    public TtrState tutorialState;

    public TtrStepInstance curTtrStepInstance;
    public int? curStepID;
    public int? nextStepID;


    public void SetSaveData()
    {
        today = TimerManager.Instance.GetToday();

        IDsForSerialization = ItemStackManager.Instance.IDs.ToList();
        AllItemStack = ItemStackManager.Instance.AllItemStack;


        itemRecords = ItemRecordManager.Instance.itemRecords;

        AllNPC = NPCManager.Instance.AllNPC;

        AllQuests = QuestManager.Instance.AllQuests;
        AcceptedQuests = QuestManager.Instance.AcceptedQuests;
        OnceSuccessQuests = QuestManager.Instance.OnceSuccessQuests;
        JustCompleteQuests = QuestManager.Instance.JustCompleteQuests;
        TodayAvailableQuest = QuestManager.Instance.TodayAvailableQuest;
        QuestCheckQueueForSerialization = QuestManager.Instance.QuestCheckQueue.ToList();
        TodaySpawnNPC = QuestManager.Instance.questData.TodaySpawnNPCQuestIDs;

        CurrentSeasonType = CalendarManager.Instance.CurrentSeasonType;

        //playerInvenData.SetPlayerInvenData(InventoryManager.Instance.Invens[InvenType.Player].model.ID2ItemStack);
        foodKey2IDs = InventoryManager.Instance.Invens[InvenType.Player].GetModel().itemID2ItemStackIDs;
        itemStackIDs = InventoryManager.Instance.Invens[InvenType.Player].GetModel().itemStackIDs;

        tutorialState = GameManager.Instance.tutorialState;
        if (tutorialState == TtrState.InProgress)
        {
            curTtrStepInstance = TutorialManager.Instance.curTtrStepInstance;
            curStepID = TutorialManager.Instance.curStepID;
            nextStepID = TutorialManager.Instance.nextStepID;
        }
    }

    //[System.Serializable]
    //public class PlayerInvenData
    //{
    //    public List<PlayerItemData> ItemList = new();

    //    public void SetPlayerInvenData(List<int> itemStackIDs)
    //    {
    //        ItemList.Clear();
    //        foreach (int itemStackID in itemStackIDs)
    //        {
    //            ItemStack itemStack = Data.GetItemStack(itemStackID);
    //            PlayerItemData invenData = new PlayerItemData(itemStack.OriginItemKey, itemStack.Count, itemStack.ID);
    //            ItemList.Add(invenData);
    //        }
    //    }
    //}

    [System.Serializable]
    public class PlayerItemData
    {
        public int originKey;
        public int Count;
        public int ID;

        public PlayerItemData(int originKey, int count, int iD)
        {
            this.originKey = originKey;
            Count = count;
            ID = iD;
        }
    }
}
