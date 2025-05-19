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
        if(SaveLoadManager.Instance.LoadData(out PlayerGameData playerGameData))
        {
            TimerManager.Instance.ApplyLoadData(playerGameData.today);

            //ItemStackManager.Instance.ApplyLoadData(playerGameData.IDs, playerGameData.AllItemStack);

            ItemRecordManager.Instance.ApplyLoadData(playerGameData.itemRecords);

            NPCManager.Instance.NPCData.ApplyLoadData(playerGameData.AllNPC);

            QuestManager.Instance.questData.ApplyLoadData(playerGameData.AllQuests, playerGameData.AcceptedQuests, playerGameData.OnceSuccessQuests, playerGameData.JustCompleteQuests, playerGameData.TodayAvailableQuest, playerGameData.QuestCheckQueue);

            foreach(var item in playerGameData.playerInvenData.ItemList)
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
