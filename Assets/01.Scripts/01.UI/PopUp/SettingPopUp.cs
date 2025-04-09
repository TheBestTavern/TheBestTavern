using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SettingPopUp : BasePopUp
{
    [SerializeField] private Button quitButton;

    public override void Awake()
    {
        base.Awake();
        popUpType = PopUpType.Setting;
        quitButton.onClick.AddListener(OnClickQuitButton);
    }

    void OnClickQuitButton()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public override void OnOpen()
    {
        base.OnOpen();
        transform.GetChild(0).GetComponent<CanvasGroup>().DOFade(1f, 1f);
    }

    public override void OnClose()
    {
        base.OnClose();
        transform.GetChild(0).GetComponent<CanvasGroup>().DOFade(0f, 1f).OnComplete(() => gameObject.SetActive(false));
    }
}
