using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 채집씬 기본 UI
/// </summary>
public class GatheringSceneUI : MonoBehaviour
{
    // 맵 선택 버튼
    [SerializeField] private Button mapButton;
    // 메인 씬으로 돌아가기 버튼 
    [SerializeField] private Button mainSceneButton;

    private void Awake()
    {
        // 맵 선택 버튼 클릭 이벤트 리스너 추가 
        mapButton.onClick.AddListener(OnClickMapButton);

        // 메인 씬으로 돌아가기 버튼 클릭 이벤트 리스너 추가
        mainSceneButton.onClick.AddListener(OnClickMainSceneButton);
    }

    // 맵 선택 버튼 함수 
    async void OnClickMapButton()
    {
        // 맵 선택 팝업 불러오기
        await UIManager.Instance.ShowPopUp(PopUpType.SelectMap);
    }

    // 메인 씬으로 돌아가기 버튼 함수
    async void OnClickMainSceneButton()
    {
        // 확인 팝업 불러오기 
        await UIManager.Instance.ShowPopUp(PopUpType.Confirm);
        // 확인 팝업 설정
        UIManager.Instance.confirmPopUp.SetConfirm("주막으로 돌아가시겠습니까?", ConfirmFunc);
    }

    // 확인 팝업 함수
    async void ConfirmFunc()
    {
        // 메인씬 불러오기 
        await SceneLoader.Instance.LoadSceneAsync("MainSceneDev");
    }
}
