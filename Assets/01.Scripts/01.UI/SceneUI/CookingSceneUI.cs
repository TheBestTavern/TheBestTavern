using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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

    // 도마 미니게임 시작 버튼
    [SerializeField] private Button cuttingMiniGameButton;

    // 합치기 버튼
    [SerializeField] private Button plateButton;

    [SerializeField] private Button blurBackGround;

    private RectTransform curBtn;
    private Vector2 curBtnPos;

    private bool isFocused;

    public Action<string> selectTool;
    public Action deselectTool;

    private void Awake()
    {
        // 메인 씬으로 돌아가기 버튼 이벤트 리스너 추가
        mainSceneButton.onClick.AddListener(OnClickMainSceneButton);
        // 굽기 미니게임 시작 버튼 이벤트 리스너 추가
        grillMiniGameButton.onClick.AddListener(() => ClickToolButton("Cooking_Grill_Test"));
        // 절구 미니게임 시작 버튼 이벤트 리스너 추가
        grindMiniGameButton.onClick.AddListener(() => ClickToolButton("Cooking_Grind_Test"));
        // 맷돌 미니게임 시작 버튼 이벤트 리스너 추가 
        millMiniGameButton.onClick.AddListener(() => ClickToolButton("Cooking_Mill_Test"));
        // 도마 미니게임 시작 버튼 이벤트 리스너 추가
        cuttingMiniGameButton.onClick.AddListener(() => ClickToolButton("Cooking_Cutting_Test"));

        plateButton.onClick.AddListener(() => ClickToolButton("Plate"));

        blurBackGround.onClick.AddListener(OnClickBlurBackGround);
    }

    // 메인 씬으로 돌아가기 버튼 함수
    async void OnClickMainSceneButton()
    {
        // 확인 팝업 불러오기 
        await UIManager.Instance.ShowPopUp(PopUpType.Confirm);
        // 확인 팝업 설정
        UIManager.Instance.confirmPopUp.SetConfirm("마당으로 이동하시겠습니까?", ConfirmFunc);
    }

    void ClickToolButton(string s)
    {
        if (!isFocused)
        {
            isFocused = true;

            switch (s)
            {
                case "Cooking_Grill_Test":
                    ReadyMiniGame(grillMiniGameButton);
                    grillMiniGameButton.GetComponent<RectTransform>().DORotate(new Vector3(0, 0, 0), 2f);
                    break;
                case "Cooking_Grind_Test":
                    ReadyMiniGame(grindMiniGameButton);
                    break;
                case "Cooking_Mill_Test":
                    ReadyMiniGame(millMiniGameButton);
                    break;
                case "Cooking_Cutting_Test":
                    ReadyMiniGame(cuttingMiniGameButton);
                    break;
                case "Plate":
                    ReadyMiniGame(plateButton);
                    break;
                default:
                    break;
            }

            OnClickCookingTool(s);
        }
    }

    void OnClickCookingTool(string s)
    {
        selectTool?.Invoke(s);
    }

    void OnClickBG()
    {
        deselectTool?.Invoke();
    }

    void OnClickGrillMiniGameButton(string s)
    {
        if (!isFocused)
        {
            isFocused = true;

            ReadyMiniGame(grillMiniGameButton);
            grillMiniGameButton.GetComponent<RectTransform>().DORotate(new Vector3(0, 0, 0), 2f);

            OnClickCookingTool(s);
        }
    }

    void OnClickBlurBackGround()
    {
        if (isFocused)
        {
            isFocused = false;

            curBtn.DOAnchorPos(curBtnPos, 2f);
            curBtn.DOScale(new Vector3(1, 1, 1), 1.5f);
            if (curBtn.gameObject.name == "GrillMiniGameButton")
            {
                curBtn.DORotate(new Vector3(0, 0, -40), 1.5f);
            }

            OnClickBG();
            blurBackGround.gameObject.SetActive(false);
        }
        //CookingMiniGameManager.Instance.SetMiniGameItem();
    }

    // 확인 팝업 함수
    async void ConfirmFunc()
    {
        // 메인씬 불러오기
        await SceneLoader.Instance.LoadSceneAsync("MainScene");
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
        //miniGameStartButton.gameObject.SetActive(true);
    }

    //void StartMinGame(string miniGameName)
    //{
    //    CookingMiniGameManager.Instance.ShowMiniGame();
    //}

    //async void NotSelectedFood()
    //{
    //    await UIManager.Instance.ShowPopUp(PopUpType.Alarm);
    //    UIManager.Instance.alarmPopUp.SetAlarm("음식을 선택해주세요");
    //}
}
