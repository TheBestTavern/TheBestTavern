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
    [SerializeField] private Button BedButton;

    private void Awake()
    {
        cookingSceneButton.onClick.AddListener(OnClickCookingSceneButton);
        gatheringSceneButton.onClick.AddListener(OnClickGatheringSceneButton);
        mailBoxButton.onClick.AddListener(OnClickMailBoxButton);
    }

    void OnClickGatheringSceneButton()
    {
        UIManager.Instance.ShowPopUp(PopUpType.SelectMap);
    }

    void OnClickCookingSceneButton()
    {
        UIManager.Instance.ShowPopUp(PopUpType.Confirm);
        UIManager.Instance.confirmPopUp.SetConfirm("부엌으로 이동하시겠습니까?", ConfirmFunc);
    }

    void OnClickMailBoxButton()
    {
        //UIManager.Instance.ShowPopUp(PopUpType.MailBox);
    }

    async void ConfirmFunc()
    {
        await SceneLoader.Instance.LoadSceneAsync("CookingSceneDev");
    } 
}
