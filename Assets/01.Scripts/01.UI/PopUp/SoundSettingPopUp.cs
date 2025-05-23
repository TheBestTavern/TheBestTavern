using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SoundSettingPopUp : BasePopUp
{
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider ambienceSlider;

    public override void Awake()
    {
        base.Awake();

        popUpType = PopUpType.SoundSetting;

        SetInitialVolume();
        bgmSlider.onValueChanged.AddListener(SoundManager.Instance.SetBGMVolume);
        sfxSlider.onValueChanged.AddListener(SoundManager.Instance.SetSFXVolume);
        ambienceSlider.onValueChanged.AddListener(SoundManager.Instance.SetAmbienceVolume);

    }

    // 팝업 열때 필요한 함수
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

    private void SetInitialVolume()
    {
        bgmSlider.value = SoundManager.Instance.GetBGMVolume();
        sfxSlider.value = SoundManager.Instance.GetSFXVolume();
        ambienceSlider.value = SoundManager.Instance.GetAmbienceVolume();
    }
}
