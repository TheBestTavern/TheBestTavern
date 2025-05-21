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
    [SerializeField] private Button gameExitButton;

    [SerializeField] private GameObject tutorialSkipPanel;
    [SerializeField] private Button tutorialSkipButton;
    [SerializeField] private Button doTutorialButton;

    [SerializeField] private GameObject renewPanel;
    [SerializeField] private Button renewButton;
    [SerializeField] private Button rejectButton;
    void Start()
    {
        UIManager.Instance.startSceneUI = this;

        gameStartButton.onClick.AddListener(OnClickGameStartButton);
        gameLoadStartButton.onClick.AddListener(OnClickGameLoadStartButton);
        gameExitButton.onClick.AddListener(OnClickGameExitButton);

        tutorialSkipButton.onClick.AddListener(OnClicktutorialSkipButton);
        doTutorialButton.onClick.AddListener(OnClickDoTutorialButton);

        renewButton.onClick.AddListener(OnClicktutorialSkipButton);
        rejectButton.onClick.AddListener(OnClickRejectButton);
    }

    private async void OnClickDoTutorialButton()
    {
        await SceneLoader.Instance.LoadSceneAsync("TutorialScene");
    }

    private async void OnClicktutorialSkipButton()
    {
        await SceneLoader.Instance.LoadSceneAsync("MainScene");
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
}
