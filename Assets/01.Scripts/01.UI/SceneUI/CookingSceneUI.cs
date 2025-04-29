using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using static UnityEditor.Progress;

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

    [SerializeField] private Button blurBackGround;

    [SerializeField] private Button miniGameStartButton;

    private RectTransform curBtn;
    private Vector2 curBtnPos;

    private int btnClickCount = 0;

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

        blurBackGround.onClick.AddListener(OnClickBlurBackGround);

        miniGameStartButton.onClick.AddListener(OnClickMiniGameStartButton);
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
        if (btnClickCount == 0)
        {
            btnClickCount++;
            ReadyMiniGame(grillMiniGameButton);
            grillMiniGameButton.GetComponent<RectTransform>().DORotate(new Vector3(0, 0, 0), 2f);
        }
        else
        {
            if (CookingMiniGameManager.Instance.GetMiniGameItem() == null)
            {
                NotSelectedFood();
            }
            else
            {
                // 굽기 미니게임 씬 불러오기
                StartMinGame("Cooking_Grill_Test");
            }
        }
    }

    // 절구 미니게임 시작 버튼 함수 
    void OnClickGrindMiniGameButton()
    {
        if (btnClickCount == 0)
        {
            btnClickCount++;
            ReadyMiniGame(grindMiniGameButton);
        }
        else
        {
            if (CookingMiniGameManager.Instance.GetMiniGameItem() == null)
            {
                NotSelectedFood();
            }
            else
            {
                // 절구 미니게임 씬 불러오기
                StartMinGame("Cooking_Grind_Test");
            }
        }
    }

    // 맷돌 미니게임 시작 버튼 함수
    void OnClickMillMiniGameButton()
    {
        if (btnClickCount == 0)
        {
            btnClickCount++;
            ReadyMiniGame(millMiniGameButton);
        }
        else
        {
            if (CookingMiniGameManager.Instance.GetMiniGameItem() == null)
            {
                NotSelectedFood();
            }
            else
            {
                // 맷돌 미니게임 씬 불러오기 
                StartMinGame("Cooking_Mill_Test");
            }
        }
    }

    private void OnClickMiniGameStartButton()
    {

    }

    private void OnClickBlurBackGround()
    {
        btnClickCount = 0;
        blurBackGround.gameObject.SetActive(false);
        miniGameStartButton.gameObject.SetActive(false);
        curBtn.DOAnchorPos(curBtnPos, 2f);
        curBtn.DOScale(new Vector3(1, 1, 1), 1.5f);
        if (curBtn.gameObject.name == "GrillMiniGameButton")
        {
            curBtn.DORotate(new Vector3(0, 0, -40), 1.5f);
        }
        CookingMiniGameManager.Instance.SetMiniGameItem();
    }

    // 확인 팝업 함수
    async void ConfirmFunc()
    {
        // 메인씬 불러오기
        await SceneLoader.Instance.LoadSceneAsync("MainSceneDev");
    }

    void ReadyMiniGame(Button button)
    {
        RectTransform btnRect = button.GetComponent<RectTransform>();
        curBtn = btnRect;
        curBtnPos = new Vector2(btnRect.anchoredPosition.x, btnRect.anchoredPosition.y);

        button.transform.SetAsLastSibling();
        btnRect.DOAnchorPos(new Vector2(0, 0), 1.5f);
        btnRect.DOScale(new Vector3(3, 3, 3), 1.5f);
        blurBackGround.gameObject.SetActive(true);
        miniGameStartButton.gameObject.SetActive(true);
    }

    void StartMinGame(string miniGameName)
    {
        CookingMiniGameManager.Instance.ShowMiniGame(miniGameName);
    }

    async void NotSelectedFood()
    {
        await UIManager.Instance.ShowPopUp(PopUpType.Alarm);
        UIManager.Instance.alarmPopUp.SetAlarm("음식을 선택해주세요");
    }
}
