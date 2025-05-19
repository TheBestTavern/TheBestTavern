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

    public Dictionary<int, ItemStack> AllItemStack = new();

    public Dictionary<int, ItemRecord> itemRecords = new();

    public Dictionary<int, NPC> AllNPC = new();

    public Dictionary<int, Quest> AllQuests = new();
    public List<int> AcceptedQuests = new();
    public Dictionary<int, SuccessDegree> OnceSuccessQuests = new();
    public List<int> JustCompleteQuests = new();
    public List<int> TodayAvailableQuest = new();
    public Queue<(int questID, int itemID)> QuestCheckQueue = new();

    public Dictionary<int, List<int>> foodKey2IDs = new();
    public Dictionary<int, ItemStack> ID2ItemStack = new();

    public void SetSaveData()
    {
        today = TimerManager.Instance.GetToday();

        IDs = ItemStackManager.Instance.IDs;

        AllItemStack = ItemStackManager.Instance.AllItemStack;

        itemRecords = ItemRecordManager.Instance.itemRecords;

        AllNPC = NPCManager.Instance.AllNPC;

        AllQuests = QuestManager.Instance.AllQuests;
        AcceptedQuests = QuestManager.Instance.AcceptedQuests;
        OnceSuccessQuests = QuestManager.Instance.OnceSuccessQuests;
        JustCompleteQuests = QuestManager.Instance.JustCompleteQuests;
        TodayAvailableQuest = QuestManager.Instance.TodayAvailableQuest;
        QuestCheckQueue = QuestManager.Instance.QuestCheckQueue;

        foodKey2IDs = InventoryManager.Instance.Invens[InvenType.Player].model.foodKey2IDs;
        ID2ItemStack = InventoryManager.Instance.Invens[InvenType.Player].model.ID2ItemStack;
    }
}
