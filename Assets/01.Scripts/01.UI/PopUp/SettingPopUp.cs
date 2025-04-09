using System.Collections;
using System.Collections.Generic;
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
}
