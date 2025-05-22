using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class StartSceneUI : MonoBehaviour
{
    [SerializeField] private Button gameStartButton;
    [SerializeField] private Button gameLoadStartButton;
    [SerializeField] private Button gameExitButton;

    [SerializeField] private GameObject tutorialSkipPanel;
    [SerializeField] private Button tutorialSkipButton;
    [SerializeField] private Button doTutorialButton;

    [SerializeField] private GameObject renewPanel;
    [SerializeField] private Button renewButton;
    [SerializeField] private Button rejectButton;

    [SerializeField] private GameObject acceptAnalyticsPanel;
    [SerializeField] private Button acceptAnalyticsButton;
    [SerializeField] private Button rejectAnalyticsButton;

    string sceneName = "";

    void Start()
    {
        UIManager.Instance.startSceneUI = this;

        gameStartButton.onClick.AddListener(OnClickGameStartButton);
        gameLoadStartButton.onClick.AddListener(OnClickGameLoadStartButton);
        gameExitButton.onClick.AddListener(OnClickGameExitButton);

        tutorialSkipButton.onClick.AddListener(OnClicktutorialSkipButton);
        doTutorialButton.onClick.AddListener(OnClickDoTutorialButton);

        renewButton.onClick.AddListener(OnClickRenewButton);
        rejectButton.onClick.AddListener(OnClickRejectButton);

        acceptAnalyticsButton.onClick.AddListener(OnClickAcceptAnalyticsButton);
        rejectAnalyticsButton.onClick.AddListener(OnClickRejectAnalyticsButton);
    }

    private async void OnClickAcceptAnalyticsButton()
    {
        ConfirmAnalytics();
        await SceneLoader.Instance.LoadSceneAsync(sceneName);
    }

    private async void OnClickRejectAnalyticsButton()
    {
        await SceneLoader.Instance.LoadSceneAsync(sceneName);
    }

    private void OnClickDoTutorialButton()
    {
        sceneName = "TutorialScene";
        acceptAnalyticsPanel.SetActive(true);
        tutorialSkipPanel.SetActive(false);
    }

    private void OnClicktutorialSkipButton()
    {
        sceneName = "MainScene";
        acceptAnalyticsPanel.SetActive(true);
        tutorialSkipPanel.SetActive(false);
    }

    private void OnClickRenewButton()
    {
        renewPanel.SetActive(false);
        tutorialSkipPanel.SetActive(true);        
    }

    private void OnClickRejectButton()
    {
        renewPanel.SetActive(false);
    }

    private void OnClickGameExitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    private void OnClickGameStartButton()
    {
        if (!SaveLoadManager.Instance.LoadData(out PlayerGameData playerGameData))
        {
            tutorialSkipPanel.SetActive(true);
        }
        else
        {
            renewPanel.SetActive(true);
        }
    }

    private async void OnClickGameLoadStartButton()
    {
        if (SaveLoadManager.Instance.LoadData(out PlayerGameData playerGameData))
        {
            TimerManager.Instance.ApplyLoadData(playerGameData.today);

            //ItemStackManager.Instance.ApplyLoadData(playerGameData.IDs, playerGameData.AllItemStack);

            ItemRecordManager.Instance.ApplyLoadData(playerGameData.itemRecords);

            NPCManager.Instance.NPCData.ApplyLoadData(playerGameData.AllNPC);

            QuestManager.Instance.questData.ApplyLoadData(playerGameData.AllQuests, playerGameData.AcceptedQuests, playerGameData.OnceSuccessQuests, playerGameData.JustCompleteQuests, playerGameData.TodayAvailableQuest, playerGameData.QuestCheckQueue);

            CalendarManager.Instance.ApplyLoadData(playerGameData.CurrentSeasonType);

            foreach (var item in playerGameData.playerInvenData.ItemList)
            {
                InventoryManager.Instance.Invens[InvenType.Player].아이템획득(item.Origin, item.Count);
            }

            await SceneLoader.Instance.LoadSceneAsync("MainScene");
        }
        else
        {
            Debug.Log("로드 실패");
        }
    }

    async void ConfirmAnalytics()
    {
        await UnityServices.InitializeAsync();
        AnalyticsService.Instance.StartDataCollection();
    }
}
