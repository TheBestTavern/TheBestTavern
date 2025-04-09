using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookingSceneUI : MonoBehaviour
{
    [SerializeField] private Button mainSceneButton;

    private void Awake()
    {
        mainSceneButton.onClick.AddListener(OnClickMainSceneButton);
    }

    void OnClickMainSceneButton()
    {
        UIManager.Instance.ShowPopUp(PopUpType.Confirm);
        UIManager.Instance.confirmPopUp.SetConfirm("마당으로 이동하시겠습니까?", ConfirmFunc);
    }

    async void ConfirmFunc()
    {
        await SceneLoader.Instance.LoadSceneAsync("MainSceneDev");
    }
}
