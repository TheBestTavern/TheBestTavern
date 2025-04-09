using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectMapPopUp : BasePopUp
{
    [SerializeField] private GameObject selectForestOcean;

    [SerializeField] private Button selectButton;
    [SerializeField] private Button selectCloseButton;
    [SerializeField] private Button gatheringSceneButton;

    public override void Awake()
    {
        base.Awake();
        popUpType = PopUpType.SelectMap;
        selectButton.onClick.AddListener(OnClickSelectButton);
        selectCloseButton.onClick.AddListener(OnClickSelectCloseButton);
        gatheringSceneButton.onClick.AddListener(OnClickGatheringSceneButton);
    }

    void OnClickSelectButton()
    {
        selectForestOcean.SetActive(true);
    }

    void OnClickSelectCloseButton()
    {
        selectForestOcean.SetActive(false);
    }

    void OnClickGatheringSceneButton()
    {
        UIManager.Instance.ShowPopUp(PopUpType.Confirm);
        UIManager.Instance.confirmPopUp.SetConfirmText("정말 이동하시겠습니까?");
        UIManager.Instance.confirmPopUp.confirmAction += ConfirmFunc;
    }

    async void ConfirmFunc()
    {       
        await SceneLoader.Instance.LoadSceneAsync("GatheringSceneDev");
    } 
}
