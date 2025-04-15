using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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

    async void OnClickGatheringSceneButton()
    {
        await UIManager.Instance.ShowPopUp(PopUpType.Confirm);
        UIManager.Instance.confirmPopUp.SetConfirm("정말 이동하시겠습니까?", ConfirmFunc);
    }

    async void ConfirmFunc()
    {       
        await SceneLoader.Instance.LoadSceneAsync("GatheringSceneDev");
    }

    public override void OnOpen()
    {
        base.OnOpen();

        RectTransform panel = transform.GetChild(0).GetComponent<RectTransform>();
        panel.localScale = new Vector3(1, 0, 1);
        panel.DOScaleY(1f, 0.8f).SetEase(Ease.OutBack);
    }

    public override void OnClose()
    {
        base.OnClose();

        RectTransform panel = transform.GetChild(0).GetComponent<RectTransform>();
        panel.DOScaleY(0f, 0.6f).SetEase(Ease.InBack).OnComplete(() => gameObject.SetActive(false));
    }
}
