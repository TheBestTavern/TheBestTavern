using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CookingTutorialController : BaseTutorialController
{
    // 굽기 미니게임 시작 버튼 
    [SerializeField] private Button grillMiniGameButton;

    // 절구 미니게임 시작 버튼
    [SerializeField] private Button grindMiniGameButton;

    // 맷돌 미니게임 시작 버튼
    [SerializeField] private Button millMiniGameButton;

    // 도마 미니게임 시작 버튼
    [SerializeField] private Button cuttingMiniGameButton;

    // 끓이기 미니게임 시작 버튼
    [SerializeField] private Button boilMiniGameButton;

    // 믹싱 볼 미니게임 시작 버튼
    [SerializeField] private Button mixingMiniGameButton;

    // 합치기 버튼
    [SerializeField] private Button plateButton;

    private string[] cookingScenetexts = {"이곳은 요리를 하는 주방입니다.", 
        "먼저 재료를 갈아주는 맷돌" , "제한시간 동안 맷돌 손잡이를 잡고 돌리면서", "정해진 회전 방향과 속도를 유지해보세요.",
        "다음으로는 재료를 자르는 도마", "제한시간 내로 스페이스 바를 눌러 재료를 균일하게 잘라보세요.", 
        "다음으로 재료를 빻는 절구" , "제한시간 동안 노트가 내려오는 타이밍에 스페이스 바를 정확하게 눌러보세요.",
        "다음으로 재료를 굽는 가마솥 뚜껑", "제한시간 내 같은 숫자의 카드를 최대한 많이 뒤집어보세요.",
        "다음으로 재료를 끓이는 가마솥", "제한시간 내 빨간 네모를 클릭하면 나타나는 화살표를 따라 그려보세요.", 
        "다음으로 믹싱볼", "게이지가 다 찰 때까지 숟가락을 클릭해 재료를 섞어보세요.", 
        "마지막으로 접시에서는 재료들을 넣고 음식을 완성해보세요." };

    List<Data_Foods> tempFoods = new();
    Data_Foods tempFood = new();

    public async override void OnClickNextButton()
    {
        if (textIndex >= cookingScenetexts.Length)
            return;

        if (isTexting)
        {
            return;
        }

        ShowText(cookingScenetexts[textIndex]);

        switch (textIndex)
        {
            case 1:
                npcImage.DOFade(0, 1f);
                UIManager.Instance.cookingSceneUI.ReadyMiniGame(millMiniGameButton);
                CookingMiniGameManager.Instance.SetMiniGameTool("Cooking_Mill_Test");
                break;
            case 2:
                tempFood = Data.GetRawItem(101001);
                tempFoods.Add(tempFood);
                CookingMiniGameManager.Instance.SetMiniGameItem(tempFoods);
                UIManager.Instance.cookingSceneUI.StartMiniGame();
                break;
            case 4:
                await SceneLoader.Instance.UnLoadSceneAsyncMiniGame();
                UIManager.Instance.cookingSceneUI.ButtonsBack();
                UIManager.Instance.cookingSceneUI.ReadyMiniGame(cuttingMiniGameButton);
                CookingMiniGameManager.Instance.SetMiniGameTool("Cooking_Cutting_Test");
                break;
            case 5:
                tempFood = Data.GetRawItem(101001);
                tempFoods.Add(tempFood);
                CookingMiniGameManager.Instance.SetMiniGameItem(tempFoods);
                UIManager.Instance.cookingSceneUI.StartMiniGame();
                break;
            case 6:
                await SceneLoader.Instance.UnLoadSceneAsyncMiniGame();
                UIManager.Instance.cookingSceneUI.ButtonsBack();
                UIManager.Instance.cookingSceneUI.ReadyMiniGame(grindMiniGameButton);
                CookingMiniGameManager.Instance.SetMiniGameTool("Cooking_Grind_Test");
                break;
            case 7:
                tempFood = Data.GetRawItem(101001);
                tempFoods.Add(tempFood);
                CookingMiniGameManager.Instance.SetMiniGameItem(tempFoods);
                UIManager.Instance.cookingSceneUI.StartMiniGame();
                break;
            case 8:
                await SceneLoader.Instance.UnLoadSceneAsyncMiniGame();
                UIManager.Instance.cookingSceneUI.ButtonsBack();
                UIManager.Instance.cookingSceneUI.ReadyMiniGame(grillMiniGameButton);
                CookingMiniGameManager.Instance.SetMiniGameTool("Cooking_Grill_Test");
                break;
            case 9:
                tempFood = Data.GetRawItem(101001);
                tempFoods.Add(tempFood);
                CookingMiniGameManager.Instance.SetMiniGameItem(tempFoods);
                UIManager.Instance.cookingSceneUI.StartMiniGame();
                break;
            case 10:
                await SceneLoader.Instance.UnLoadSceneAsyncMiniGame();
                UIManager.Instance.cookingSceneUI.ButtonsBack();
                UIManager.Instance.cookingSceneUI.ReadyMiniGame(boilMiniGameButton);
                CookingMiniGameManager.Instance.SetMiniGameTool("Cooking_Boil_Test");
                break;
            case 11:
                tempFood = Data.GetRawItem(101001);
                tempFoods.Add(tempFood);
                CookingMiniGameManager.Instance.SetMiniGameItem(tempFoods);
                UIManager.Instance.cookingSceneUI.StartMiniGame();
                break;
            case 12:
                await SceneLoader.Instance.UnLoadSceneAsyncMiniGame();
                UIManager.Instance.cookingSceneUI.ButtonsBack();
                UIManager.Instance.cookingSceneUI.ReadyMiniGame(mixingMiniGameButton);
                CookingMiniGameManager.Instance.SetMiniGameTool("Cooking_MixingBowl_Test");
                break;
            case 13:
                tempFood = Data.GetRawItem(101001);
                tempFoods.Add(tempFood);
                CookingMiniGameManager.Instance.SetMiniGameItem(tempFoods);
                UIManager.Instance.cookingSceneUI.StartMiniGame();
                break;
        }

        textIndex++;
    }   
}
