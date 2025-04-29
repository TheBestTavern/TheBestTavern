
using System.Collections.Generic;
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

    private ICookingMiniGameHandler currentGame;
    private List<Data_Foods> selectedItems;

    string selectedCookingTool = null;


    private void Start()
    {
        cookingInventoryView.OnEnableTargetSlot = SetMiniGameItem;
        cookingInventoryView.OnDisalbeTargetSlot = ClearMiniGameItem;
    }

    async public void ShowMiniGame()
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
        switch (s)
        {
            case "Cooking_Grill_Test":
                cookingInventoryView.SetTargetSlotCount(1,1);
                break;
            case "Cooking_Grind_Test":
                cookingInventoryView.SetTargetSlotCount(1,1);
                break;
            case "Cooking_Mill_Test":
                cookingInventoryView.SetTargetSlotCount(1,1);
                break;
            default:
                break;
        }
        cookingInventoryView.SetAbleButton();


        if (!cookingInventoryView.gameObject.activeSelf)
        {
            cookingInventoryView.gameObject.SetActive(true);
        }

    }

    public List<Data_Foods> GetMiniGameItem()
    {
        return selectedItems;
    }
}
