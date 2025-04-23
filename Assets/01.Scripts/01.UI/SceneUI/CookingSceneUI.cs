using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

/// <summary>
/// 요리씬 기본 UI 
/// </summary>
public class CookingSceneUI : MonoBehaviour
{
    // 메인 씬으로 돌아가기 버튼 
    [SerializeField] private Button mainSceneButton;

    // 굽기 미니게임 시작 버튼 
    [SerializeField] private Button grillMiniGameButton;

    // 절구 미니게임 시작 버튼
    [SerializeField] private Button grindMiniGameButton;

    // 맷돌 미니게임 시작 버튼
    [SerializeField] private Button millMiniGameButton;

    private void Awake()
    {
        // 메인 씬으로 돌아가기 버튼 이벤트 리스너 추가
        mainSceneButton.onClick.AddListener(OnClickMainSceneButton);
        // 굽기 미니게임 시작 버튼 이벤트 리스너 추가
        grillMiniGameButton.onClick.AddListener(OnClickGrillMiniGameButton);
        // 절구 미니게임 시작 버튼 이벤트 리스너 추가
        grindMiniGameButton.onClick.AddListener(OnClickGrindMiniGameButton);
        // 맷돌 미니게임 시작 버튼 이벤트 리스너 추가 
        millMiniGameButton.onClick.AddListener(OnClickMillMiniGameButton);
    }

    // 메인 씬으로 돌아가기 버튼 함수
    async void OnClickMainSceneButton()
    {
        // 확인 팝업 불러오기 
        await UIManager.Instance.ShowPopUp(PopUpType.Confirm);
        // 확인 팝업 설정
        UIManager.Instance.confirmPopUp.SetConfirm("마당으로 이동하시겠습니까?", ConfirmFunc);
    }

    // 굽기 미니게임 시작 버튼 함수
    void OnClickGrillMiniGameButton()
    {
        // 굽기 미니게임 씬 불러오기
        CookingMiniGameManager.Instance.ShowMiniGame("Cooking_Grill_Test");
    }

    // 절구 미니게임 시작 버튼 함수 
    void OnClickGrindMiniGameButton()
    {
        // 절구 미니게임 씬 불러오기
        CookingMiniGameManager.Instance.ShowMiniGame("Cooking_Grind_Test");
    }

    // 맷돌 미니게임 시작 버튼 함수
    void OnClickMillMiniGameButton()
    {
        // 맷돌 미니게임 씬 불러오기 
        CookingMiniGameManager.Instance.ShowMiniGame("Cooking_Mill_Test");
    }

    // 확인 팝업 함수
    async void ConfirmFunc()
    {
        // 메인씬 불러오기
        await SceneLoader.Instance.LoadSceneAsync("MainSceneDev");
    }
}
