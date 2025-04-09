using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GatheringSceneUI : MonoBehaviour
{
    [SerializeField] private Button mapButton;
    [SerializeField] private Button mainSceneButton;

    private void Awake()
    {
        mapButton.onClick.AddListener(OnClickMapButton);
        mainSceneButton.onClick.AddListener(OnClickMainSceneButton);
    }

    void OnClickMapButton()
    {
        UIManager.Instance.ShowPopUp(PopUpType.SelectMap);
    }

    void OnClickMainSceneButton()
    {
        UIManager.Instance.ShowPopUp(PopUpType.Confirm);
        UIManager.Instance.confirmPopUp.SetConfirm("주막으로 돌아가시겠습니까?", ConfirmFunc);
    }

    async void ConfirmFunc()
    {
        await SceneLoader.Instance.LoadSceneAsync("MainSceneDev");
    }
}
