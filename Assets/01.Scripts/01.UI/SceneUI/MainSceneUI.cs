using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneUI : MonoBehaviour
{
    [SerializeField] private Button cookingSceneButton;
    [SerializeField] private Button gatheringSceneButton;
    [SerializeField] private Button mailBoxButton;
    [SerializeField] private Button bedButton;

    private void Awake()
    {
        cookingSceneButton.onClick.AddListener(OnClickCookingSceneButton);
        gatheringSceneButton.onClick.AddListener(OnClickGatheringSceneButton);
        mailBoxButton.onClick.AddListener(OnClickMailBoxButton);
        bedButton.onClick.AddListener(OnClickBedButton);
    }

    async void OnClickGatheringSceneButton()
    {
        await UIManager.Instance.ShowPopUp(PopUpType.SelectMap);
    }

    async void OnClickCookingSceneButton()
    {
        await UIManager.Instance.ShowPopUp(PopUpType.Confirm);
        UIManager.Instance.confirmPopUp.SetConfirm("부엌으로 이동하시겠습니까?", ConfirmFunc);
    }

    void OnClickMailBoxButton()
    {
        //UIManager.Instance.ShowPopUp(PopUpType.MailBox);
    }
    void OnClickBedButton()
    {
        TimerManager.Instance.OneDayPass();
    }

    async void ConfirmFunc()
    {
        await SceneLoader.Instance.LoadSceneAsync("CookingSceneDev");
    }
}
