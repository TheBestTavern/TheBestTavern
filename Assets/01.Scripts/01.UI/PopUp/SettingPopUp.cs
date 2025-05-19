using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ESC 설정 팝업 
/// </summary>
public class SettingPopUp : BasePopUp
{
    // 게임 종료 버튼
    [SerializeField] private Button quitButton;
    [SerializeField] private Button soundButton;
    [SerializeField] private Button saveButton;

    public override void Awake()
    {
        base.Awake();

        // 설정 팝업으로 설정 
        popUpType = PopUpType.Setting;

        // 게임 종료 버튼 클릭 이벤트 리스터 추가
        quitButton.onClick.AddListener(OnClickQuitButton);
        soundButton.onClick.AddListener(OnClickSoundButton);
        //saveButton.onClick.AddListener(OnClickSaveButton);
    }

    // 게임 종료 버튼 클릭 함수
    void OnClickQuitButton()
    {
        // 게임 종료 
        Application.Quit();

        // 유니티 플레이모드 종료
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    async void OnClickSoundButton()
    {
        SoundManager.Instance.PlaySFX("Button2");
        await PopUpManager.Instance.ShowPopUp(PopUpType.SoundSetting);
    }

    void OnClickSaveButton()
    {
        //저장 구현
    }

    // 팝업 열때 필요한 함수
    public override void OnOpen()
    {
        base.OnOpen();
        // 페이드인 애니메이션 
        transform.GetChild(0).GetComponent<CanvasGroup>().DOFade(1f, 1f);
    }

    // 팝업 닫을 때 필요한 함수
    public override void OnClose()
    {
        base.OnClose();
        // 페이드 아웃 애니메이션 후 비활성화
        SoundManager.Instance.PlaySFX("Button2");
        transform.GetChild(0).GetComponent<CanvasGroup>().DOFade(0f, 1f).OnComplete(() => gameObject.SetActive(false));
    }
}
