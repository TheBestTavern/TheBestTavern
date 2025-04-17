using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 채집 맵 팝업
/// </summary>
public class SelectMapPopUp : BasePopUp
{
    // 숲이나 바다 선택하는 게임 오브젝트 
    [SerializeField] private GameObject selectForestOcean;

    // 임시 선택 버튼 
    [SerializeField] private Button selectButton;
    // 임시 선택 취소 버튼 
    [SerializeField] private Button selectCloseButton;

    // 채집 씬 이동 버튼 
    [SerializeField] private Button gatheringSceneButton;

    public override void Awake()
    {
        base.Awake();

        // 팝업 타입 맵 선택 팝업으로 설정 
        popUpType = PopUpType.SelectMap;

        // 임시 선택 버튼 클릭 이벤트 리스너 추가 
        selectButton.onClick.AddListener(OnClickSelectButton);
        // 임시 선택 취소 버튼 클릭 이벤트 리스너 추가 
        selectCloseButton.onClick.AddListener(OnClickSelectCloseButton);

        // 채집 씬 이동 버튼 클릭 이벤트 리스너 추가
        gatheringSceneButton.onClick.AddListener(OnClickGatheringSceneButton);
    }

    // 임시 선택 버튼 클릭 함수 
    void OnClickSelectButton()
    {
        selectForestOcean.SetActive(true);
    }

    // 임시 선택 취소 버튼 클릭 함수
    void OnClickSelectCloseButton()
    {
        selectForestOcean.SetActive(false);
    }

    // 채집 씬 이동 버튼 함수
    async void OnClickGatheringSceneButton()
    {
        // 확인 팝업 불러오기
        await UIManager.Instance.ShowPopUp(PopUpType.Confirm);
        // 확인 팝업 설정
        UIManager.Instance.confirmPopUp.SetConfirm("정말 이동하시겠습니까?", ConfirmFunc);
    }

    // 확인 팝업 함수 
    async void ConfirmFunc()
    {       
        // 채집 씬으로 이동 
        await SceneLoader.Instance.LoadSceneAsync("GatheringSceneDev");
    }

    // 팝업 열때 필요한 함수
    public override void OnOpen()
    {
        base.OnOpen();

        RectTransform panel = transform.GetChild(0).GetComponent<RectTransform>();
        // 맵 선택 팝업 접어 놓기
        panel.localScale = new Vector3(1, 0, 1);
        // 맵 선택 팝업 접힌 상태에서 열리는 애니메이션 
        panel.DOScaleY(1f, 0.8f).SetEase(Ease.OutBack);
    }

    // 팝업 닫을 때 필요한 함수
    public override void OnClose()
    {
        base.OnClose();

        RectTransform panel = transform.GetChild(0).GetComponent<RectTransform>();
        // 맵 선택 팝업 열린 상태에서 접히는 애니메이션 후 비활성화 
        panel.DOScaleY(0f, 0.6f).SetEase(Ease.InBack).OnComplete(() => gameObject.SetActive(false));
    }
}
