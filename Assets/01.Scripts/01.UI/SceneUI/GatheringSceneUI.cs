using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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

    // 카메라 컨트롤 버튼
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;

    [SerializeField] private Transform mainCameraTransform;
    private bool isCameraMoving = false;

    private void Awake()
    {
        // 맵 선택 버튼 클릭 이벤트 리스너 추가 
        mapButton.onClick.AddListener(OnClickMapButton);

        // 메인 씬으로 돌아가기 버튼 클릭 이벤트 리스너 추가
        mainSceneButton.onClick.AddListener(OnClickMainSceneButton);

        leftButton.onClick.AddListener(OnClickLeftButton);
        rightButton.onClick.AddListener(OnClickRightButton);
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
        SaveInventory();

        // 메인씬 불러오기 
        await SceneLoader.Instance.LoadSceneAsync("MainScene");
    }

    private void SaveInventory()
    {
        ForestGatheringManager.Instance.gatheringInventoryUI.AddItemToPlayerInventory();
    }

    void OnClickLeftButton()
    {
        if (isCameraMoving)
            return;

        Vector3 pos = mainCameraTransform.position + Vector3.left * 13.15f;

        if (pos.x < -13.15f)
            return;

        MoveCamera(pos);
    }
    void OnClickRightButton()
    {
        if (isCameraMoving)
            return;

        Vector3 pos = mainCameraTransform.position + Vector3.right * 13.15f;

        if (pos.x > 13.15f)
            return;

        MoveCamera(pos);

    }
    void MoveCamera(Vector3 newPos)
    {
        isCameraMoving = true;

        mainCameraTransform.DOMove(newPos, 1f).OnComplete(() =>
        {
            isCameraMoving = false;
        });
    }
}
