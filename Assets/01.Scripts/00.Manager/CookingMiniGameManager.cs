
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cinemachine;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public class CookingMiniGameManager : MonoSingleton<CookingMiniGameManager>
{
    public MiniGameUI miniGameUI;
    public CookingSceneUI cookingSceneUI;
    public CookingInventoryView cookingInventoryView;

    public ICookingMiniGameHandler currentGame;
    private List<Data_Foods> selectedItems = new();

    string selectedCookingTool = null;

    private CookingResultGrade resultGrade;

    public Action miniGameAnim;

    private void Start()
    {
        cookingInventoryView.OnEnableTargetSlot = SetMiniGameItem;
        cookingInventoryView.OnDisalbeTargetSlot = ClearMiniGameItem;


    }

    public void OnSelectTool(string s)
    {
        SetMiniGameTool(s);
    }

    public void OnDeselectTool()
    {
        selectedCookingTool = null;
    }

    public async Task ClickStartButton()
    {
        Debug.Log(selectedCookingTool);
        if (selectedCookingTool == "Plate")
        {
            selectedItems = GetMiniGameItem();
            RecipeManager.Instance.StartCooking(selectedItems, "Plate");
            RecipeManager.Instance.CompleteDish();
            await UIManager.Instance.ShowPopUp(PopUpType.CookingResult);

            GetCookingResultData();
        }
        else
        {
            await ShowMiniGame();
        }
    }

    async private Task ShowMiniGame()
    {
        await SceneLoader.Instance.LoadSceneAsyncMiniGame(selectedCookingTool);
        miniGameUI.ResetTimer();
        SettingMiniGame(true);
        selectedCookingTool = null;
    }

    public void GetCurrentMiniGame(ICookingMiniGameHandler game)
    {
        currentGame = game;
        currentGame?.StartGame();
        SetCookingData();
    }

    async public void CloseMiniGame()
    {
        await SceneLoader.Instance.UnLoadSceneAsyncMiniGame();
        SettingMiniGame(false);
    }

    void SettingMiniGame(bool active)
    {
        miniGameUI.gameObject.SetActive(active);
    }

    public void SetMiniGameItem(List<Data_Foods> items)
    {
        this.selectedItems = items;
        cookingInventoryView.SetAbleButton();
    }

    public void ClearMiniGameItem()
    {
        selectedItems.Clear();
        cookingInventoryView.SetAbleButton();
    }

    public void SetMiniGameTool(string s)
    {
        selectedCookingTool = s;

        if (!cookingInventoryView.gameObject.activeSelf)
        {
            cookingInventoryView.gameObject.SetActive(true);
        }
    }

    public List<Data_Foods> GetMiniGameItem()
    {
        return selectedItems;
    }

    /// <summary>
    /// 데이터 관련
    /// </summary>
    // 선택한 재료/도구 넘겨주기
    public void SetCookingData()
    {
        RecipeManager.Instance.StartCooking(selectedItems, selectedCookingTool);
    }

    // 아이템키 가져와서 인벤토리에 넣어주기
    public void GetCookingResultData()
    {
        int newItemKey = RecipeManager.Instance.EndCooking();

        var controller = InventoryManager.Instance.Invens[InvenType.Player];

        // 기존 아이템 없애주기
        foreach (var item in selectedItems)
        {
            controller.아이템잃음(item, 1);
        }

        if (newItemKey == -1) return;


        var itemData = DataManager.Instance.DataLoader_Foods.GetByKey(newItemKey);

        if (itemData == null) { Debug.Log("아이템 키에 해당하는 아이템 데이터가 없음"); }

        controller.아이템획득(itemData, 1);
        if (controller.아이템획득(itemData, 1))
        {
            Debug.Log("아이템 인벤토리에 추가 성공");
        }
        else
        {
            Debug.Log("아이템 추가 불가능 상태");
        }

    }

    /// <summary>
    /// 미니게임 결과등급 저장/반환
    /// </summary>
    /// <param name="grade"></param>
    public void SetMiniGameResult(CookingResultGrade grade)
    {
        resultGrade = grade;
    }

    public CookingResultGrade GetMiniGameResult()
    {
        return resultGrade;
    }

    // 게임 바로 종료
    public void InstantGameOver()
    {
        if (currentGame is CookingMiniGameBase baseGame)
        {
            baseGame.InstantGameOver();
        }
    }
}
