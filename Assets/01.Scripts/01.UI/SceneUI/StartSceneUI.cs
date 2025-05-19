using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class StartSceneUI : MonoBehaviour
{
    [SerializeField] private Button gameStartButton;
    [SerializeField] private Button gameLoadStartButton;
    void Start()
    {
        UIManager.Instance.startSceneUI = this;

        gameStartButton.onClick.AddListener(OnClickGameStartButton);
        gameLoadStartButton.onClick.AddListener(OnClickGameLoadStartButton);
    }

    private async void OnClickGameStartButton()
    {
        SoundManager.Instance.PlayBGM("MainBGM1");
        await SceneLoader.Instance.LoadSceneAsync("MainScene");
    }
    private async void OnClickGameLoadStartButton()
    {
        LoadData();

        await SceneLoader.Instance.LoadSceneAsync("MainScene");
    }

    async void LoadData()
    {
        PlayerGameData playerGameData = SaveLoadManager.Instance.LoadData();
        LoadTime(playerGameData);
        LoadInven(playerGameData);
        await LoadQuest(playerGameData);
    }
    void LoadTime(PlayerGameData playerGameData)
    {
        LunarDateTime savedDate = new LunarDateTime(
        playerGameData.playerTimeData.year,
        playerGameData.playerTimeData.month,
        playerGameData.playerTimeData.day,
        playerGameData.playerTimeData.isLeapMonth
        );
        SceneParameter.Set("savedDate", savedDate);
    }

    void LoadInven(PlayerGameData playerGameData)
    {
        foreach (var item in playerGameData.playerInvenData.ItemList)
        {
            InventoryManager.Instance.Invens[InvenType.Player].아이템획득(item.Origin, item.Count);
        }
    }

    async Task LoadQuest(PlayerGameData playerGameData)
    {
        foreach (var acceptedQuest in playerGameData.playerQuestData.acceptedQuestDic)
        {
            PlayerTimeData savedAcceptedLunarDate = acceptedQuest.Value;
            LunarDateTime accteptedLunaDate = new LunarDateTime(savedAcceptedLunarDate.year, savedAcceptedLunarDate.month, savedAcceptedLunarDate.day, savedAcceptedLunarDate.isLeapMonth);
            
            PlayerTimeData savedTriggerLunarDate = playerGameData.playerQuestData.triggerQuestDaysDic[acceptedQuest.Key];
            LunarDateTime triggerLunaDate = new LunarDateTime(savedTriggerLunarDate.year, savedTriggerLunarDate.month, savedTriggerLunarDate.day, savedTriggerLunarDate.isLeapMonth);

            int day = (triggerLunaDate.ToDateTime() - accteptedLunaDate.ToDateTime()).Days;

            Quest quest = Data.GetQuest(acceptedQuest.Key);
            quest.AcceptQuest(accteptedLunaDate, day);
            QuestManager.Instance.questData.AcceptQuest(acceptedQuest.Key);
        }
        foreach (var onceCompletedQuest in playerGameData.playerQuestData.onceCompletedQuests)
        {
            QuestManager.Instance.questData.SuccessQuest(onceCompletedQuest.Key, onceCompletedQuest.Value);
        }
        foreach (var justCompleteQuest in playerGameData.playerQuestData.justCompleteQuests)
        {
            QuestManager.Instance.questData.JustCompleteQuests.Add(justCompleteQuest);
        }

        await Task.CompletedTask;
    }
}
