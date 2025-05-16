using System;
using System.Collections;
using System.Collections.Generic;
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
        SoundManager.Instance.PlayBGM("MainBGM");
        await SceneLoader.Instance.LoadSceneAsync("MainScene");
    }
    private async void OnClickGameLoadStartButton()
    {
        LoadData();

        await SceneLoader.Instance.LoadSceneAsync("MainScene");
    }

    void LoadData()
    {
        PlayerGameData playerGameData = SaveLoadManager.Instance.LoadData();
        LoadTime(playerGameData);
        LoadInven(playerGameData);
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
}
