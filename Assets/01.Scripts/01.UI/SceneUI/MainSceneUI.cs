using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 메인씬 기본 UI
/// </summary>
public class MainSceneUI : MonoBehaviour
{
    // 요리 씬 이동 버튼 
    [SerializeField] private Button cookingSceneButton;
    // 채집 씬 이동 버튼
    [SerializeField] private Button gatheringSceneButton;
    // 퀘스트 보기 버튼 
    [SerializeField] private Button mailBoxButton;
    // 하루 보내기 버튼
    [SerializeField] private Button bedButton;

    private void Awake()
    {
        // 요리 씬 이동 버튼 클릭 이벤트 리스너 추가 
        cookingSceneButton.onClick.AddListener(OnClickCookingSceneButton);
        // 채집 씬 이동 버튼 클릭 이벤트 리스너 추가 
        gatheringSceneButton.onClick.AddListener(OnClickGatheringSceneButton);
        // 퀘스트 보기 버튼 클릭 이벤트 리스너 추가 
        mailBoxButton.onClick.AddListener(OnClickMailBoxButton);
        // 하루 보내기 버튼 클릭 이벤트 리스너 추가 
        bedButton.onClick.AddListener(OnClickBedButton);
    }

    // 요리 씬 이동 버튼 함수 
    async void OnClickCookingSceneButton()
    {
        // 확인 팝업 불러오기 
        await UIManager.Instance.ShowPopUp(PopUpType.Confirm);

        // 확인 팝업 설정 
        UIManager.Instance.confirmPopUp.SetConfirm("부엌으로 이동하시겠습니까?", ConfirmFunc);
    }

    // 채집씬 이동 버튼 함수 
    async void OnClickGatheringSceneButton()
    {
        // 맵 선택 팝업 불러오기 
        await UIManager.Instance.ShowPopUp(PopUpType.SelectMap);
    }

    // 퀘스트 보기 버튼 함수 
    void OnClickMailBoxButton()
    {
        //UIManager.Instance.ShowPopUp(PopUpType.MailBox);
    }

    // 하루 보내기 버튼 함수 
    void OnClickBedButton()
    {
        TimerManager.Instance.OneDayPass();
    }

    // 확인 버튼 함수
    async void ConfirmFunc()
    {
        await SceneLoader.Instance.LoadSceneAsync("CookingSceneDev");
    }
}
