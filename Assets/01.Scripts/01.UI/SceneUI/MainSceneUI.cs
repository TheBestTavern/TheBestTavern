using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneUI : MonoBehaviour
{
    [SerializeField] private Button cookingSceneButton;
    [SerializeField] private Button gatheringSceneButton;

    private void Awake()
    {
        cookingSceneButton.onClick.AddListener(OnClickCookingSceneButton);
        gatheringSceneButton.onClick.AddListener(() => UIManager.Instance.ShowPopUp(PopUpType.SelectMap));
    }

    void OnClickCookingSceneButton()
    {
        UIManager.Instance.ShowPopUp(PopUpType.Confirm);
        UIManager.Instance.confirmPopUp.SetConfirm("부엌으로 이동하시겠습니까?", ConfirmFunc);
    }

    async void ConfirmFunc()
    {
        await SceneLoader.Instance.LoadSceneAsync("CookingSceneDev");
    } 
}
