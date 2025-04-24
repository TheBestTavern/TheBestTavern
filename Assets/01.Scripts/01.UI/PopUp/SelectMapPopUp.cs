using System;
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

    // 지역 선택 버튼 
    [SerializeField] private Button[] selectRegionButton;
    // 지역 선택 취소 버튼 
    [SerializeField] private Button selectCloseButton;

    // 채집 씬 이동 버튼 
    [SerializeField] private Button seaButton;
    [SerializeField] private Button forestButton;

    DesignEnums.RegionType region;

    public override void Awake()
    {
        base.Awake();

        // 팝업 타입 맵 선택 팝업으로 설정 
        popUpType = PopUpType.SelectMap;
        
        // 지역 선택 취소 버튼 클릭 이벤트 리스너 추가 
        selectCloseButton.onClick.AddListener(OnClickSelectCloseButton);

        // 산 바다 선택 버튼 클릭 이벤트 리스너 추가
        seaButton.onClick.AddListener(OnClickSeaButton);
        forestButton.onClick.AddListener(OnClickforestButton);

        // 지역 선택 버튼 클릭 이벤트 리스너 추가
        for(int i = 0; i < selectRegionButton.Length; i++)
        {
            int index = i;
            selectRegionButton[index].onClick.AddListener(() => OnClickSelectRegionButton(selectRegionButton[index].name));
        }
    }

    // 지역 선택 버튼 클릭 함수 
    void OnClickSelectRegionButton(string regionName)
    {
        selectForestOcean.SetActive(true);
        region = (DesignEnums.RegionType)Enum.Parse(typeof(DesignEnums.RegionType), regionName);
        Debug.Log(region.ToString());
    }

    // 지역 선택 취소 버튼 클릭 함수
    void OnClickSelectCloseButton()
    {
        selectForestOcean.SetActive(false);
    }

    // 산 바다 선택 버튼 함수
    async void OnClickSeaButton()
    {
        // 확인 팝업 불러오기
        await UIManager.Instance.ShowPopUp(PopUpType.Confirm);
        // 확인 팝업 설정
        UIManager.Instance.confirmPopUp.SetConfirm("정말 이동하시겠습니까?", ConfirmMoveSeaFunc);
    }

    async void OnClickforestButton()
    {
        // 확인 팝업 불러오기
        await UIManager.Instance.ShowPopUp(PopUpType.Confirm);
        // 확인 팝업 설정
        UIManager.Instance.confirmPopUp.SetConfirm("정말 이동하시겠습니까?", ConfirmMoveForestFunc);
    }

    // 확인 팝업 함수 
    async void ConfirmMoveSeaFunc()
    {       
        // 채집 씬으로 이동 
        await SceneLoader.Instance.LoadSceneAsync("GatheringSceneDev1");
    }

    // 확인 팝업 함수 
    async void ConfirmMoveForestFunc()
    {
        // 채집 씬으로 이동 
        SceneParameter.Set("Region", region);
        SceneParameter.Set("Season", DesignEnums.SeasonType.winter);
        await SceneLoader.Instance.LoadSceneAsync("GatheringSceneDev1");
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
