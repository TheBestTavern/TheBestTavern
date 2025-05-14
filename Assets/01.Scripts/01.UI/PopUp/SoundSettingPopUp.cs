using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SoundSettingPopUp : BasePopUp
{
    // 게임 종료 버튼
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;

    public override void Awake()
    {
        base.Awake();

        // 설정 팝업으로 설정 
        popUpType = PopUpType.SoundSetting;

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
        transform.GetChild(0).GetComponent<CanvasGroup>().DOFade(0f, 1f).OnComplete(() => gameObject.SetActive(false));
    }
}
