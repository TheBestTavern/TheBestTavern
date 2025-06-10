using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class IntroSceneUI : MonoBehaviour
{
    [Header("UI Images")]
    [SerializeField] private Image blackBackGround;
    [SerializeField] private Image tractor;
    [SerializeField] private Image tractorLeftLights;
    [SerializeField] private Image tractorRightLights;
    [SerializeField] private Image startSceneImage;
    [SerializeField] private Image paper;
    [SerializeField] private Image framingImage;
    [SerializeField] private RectTransform maskRectTransform;

    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI introText;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI gameStartBtnText;
    [SerializeField] private TextMeshProUGUI gameLoadStartBtnText;
    [SerializeField] private TextMeshProUGUI gameExitBtnText;

    [Header("Button")]
    [SerializeField] private Image gameStartBtnImage;
    [SerializeField] private Image gameLoadStartBtnImage;
    [SerializeField] private Image gameExitBtnImage;
    [SerializeField] private Button skipButton;

    private readonly string[] introTexts = {
        "강원도 시골", "나는 주말마다 부모님을 위해 손을 거든다.",
        "오늘도 여느때와 다를 것이 없었고", "평소처럼 논두렁으로 향하던 그때",
        "눈을 떠보니 이곳은 내가 있던 곳이 아니었다.",
        "눈앞에 펼쳐진 건 고요하고 낯선 이세계 조선.",
        "그 옆에는 허름한 주막과 함께", "이런 쪽지가 떨어져 있었다.",
        "옥황상제 특명", "지상 요리 도감 완성"
    };

    private const float shortDelay = 1500;
    private const int largeFontSize = 200;

    private CancellationTokenSource cts;

    [SerializeField] private PreLoadingUI preLoadingUI;

    private async void Awake()
    {
        Time.timeScale = 0;
        var sizeHandle = Addressables.GetDownloadSizeAsync("PreLoad");
        await sizeHandle.Task;

        if (sizeHandle.Status != AsyncOperationStatus.Succeeded || sizeHandle.Result <= 0)
        {
            Debug.LogWarning("다운로드할 에셋이 없습니다.");
            Time.timeScale = 1;
            return;
        }
        else
        {
            preLoadingUI.gameObject.SetActive(true);
        }
    }

    private async void Start()
    {
        cts = new CancellationTokenSource();
        skipButton.onClick.AddListener(OnClickSkipButton);

        try
        {
            await PlayIntroSequence(cts.Token);
        }
        catch (OperationCanceledException)
        {
            Debug.Log("인트로 스킵됨");
        }
    }

    private void OnClickSkipButton()
    {
        if (cts == null || cts.IsCancellationRequested) return;

        cts.Cancel();
        DOTween.KillAll();
        SetFinalIntroState();
        skipButton.gameObject.SetActive(false);
    }

    private void SetFinalIntroState()
    {
        introText.gameObject.SetActive(false);
        tractor.gameObject.SetActive(false);
        framingImage.gameObject.SetActive(false);

        blackBackGround.color = new Color(0, 0, 0, 0);
        paper.gameObject.SetActive(false);
        startSceneImage.gameObject.SetActive(true);

        gameStartBtnImage.gameObject.SetActive(true);
        gameLoadStartBtnImage.gameObject.SetActive(true);
        gameExitBtnImage.gameObject.SetActive(true);

        gameStartBtnImage.color = new Color(1, 1, 1, 1);
        gameStartBtnText.color = new Color(0, 0, 0, 1);

        gameLoadStartBtnImage.color = new Color(1, 1, 1, 1);
        gameLoadStartBtnText.color = new Color(0, 0, 0, 1);

        gameExitBtnImage.color = new Color(1, 1, 1, 1);
        gameExitBtnText.color = new Color(0, 0, 0, 1);

        titleText.gameObject.SetActive(true);
        titleText.color = new Color(0,0,0,1);

        SoundManager.Instance.PlayBGM("IntroBGM");
        SoundManager.Instance.StopLoop();
    }

    private async Task PlayIntroSequence(CancellationToken token)
    {
        introText.gameObject.SetActive(false);
        await blackBackGround.DOFade(0, 2).AsyncWaitForCompletion();
        SoundManager.Instance.PlayAmbience("CountrySide");

        token.ThrowIfCancellationRequested();

        introText.gameObject.SetActive(true);
        await PlayIntroTexts(token);
        SoundManager.Instance.StopLoop();

        await introText.DOFade(0, 1).AsyncWaitForCompletion();
        framingImage.gameObject.SetActive(false);
        token.ThrowIfCancellationRequested();

        await PlayTractorSequence(token);
        await PlayPostTractorTexts(token);

        await UniTask.WaitForSeconds(1f);
        SoundManager.Instance.PlaySFX("Paper");
        paper.gameObject.SetActive(true);
        startSceneImage.gameObject.SetActive(true);
        blackBackGround.DOFade(0, 2);
        skipButton.gameObject.SetActive(false);
        await introText.DOFade(0, 2).AsyncWaitForCompletion();

        await UniTask.WaitForSeconds(1f);
        await paper.rectTransform.DOMoveY(-1500, 2f).AsyncWaitForCompletion();

        //await RevealText(titleText,token);
        BrushTitleText();
        SoundManager.Instance.PlayBGM("IntroBGM");

        gameStartBtnImage.gameObject.SetActive(true);
        gameStartBtnImage.DOFade(1, 4);
        gameStartBtnText.DOFade(1, 4);

        gameLoadStartBtnImage.gameObject.SetActive(true);
        gameLoadStartBtnImage.DOFade(1, 4);
        gameLoadStartBtnText.DOFade(1, 4);

        gameExitBtnImage.gameObject.SetActive(true);
        gameExitBtnImage.DOFade(1, 4);
        gameExitBtnText.DOFade(1, 4);
    }

    // 트랙터 전 글씨뜰때
    private async Task PlayIntroTexts(CancellationToken token)
    {
        for (int i = 0; i < 4; i++)
        {
            token.ThrowIfCancellationRequested();
            if (i == 3)
                blackBackGround.DOFade(1, 2);

            await ShowText(introTexts[i], 100, token);
        }
    }

    private async Task PlayTractorSequence(CancellationToken token)
    {
        SoundManager.Instance.PlaySFX("Car");

        tractor.gameObject.SetActive(true);
        tractor.rectTransform.DOScale(2f, 3f);
        tractorLeftLights.rectTransform.DOScale(8f, 3f);
        await tractorRightLights.rectTransform.DOScale(8f, 3f).AsyncWaitForCompletion();
        token.ThrowIfCancellationRequested();

        blackBackGround.color = new Color(0, 0, 0, 0);
        blackBackGround.transform.SetAsLastSibling();
        await blackBackGround.DOFade(1, 2).AsyncWaitForCompletion();
        token.ThrowIfCancellationRequested();

        tractor.gameObject.SetActive(false);
    }

    private async Task PlayPostTractorTexts(CancellationToken token)
    {
        for (int i = 4; i < introTexts.Length - 2; i++)
        {
            token.ThrowIfCancellationRequested();
            //SoundManager.Instance.PlaySFX("LineWhoosh");
            await ShowText(introTexts[i], 100, token);
        }
        introText.fontSize = largeFontSize;
        //for (int i = introTexts.Length - 2; i < introTexts.Length; i++)
        //{
        //    SoundManager.Instance.PlaySFX("Impact");

        //    token.ThrowIfCancellationRequested();
        //    introText.text = introTexts[i];
        //    introText.alpha = 1;
        //    await RevealText(introText, token);
        //    await Task.Delay(1000, token);
        //    await introText.DOFade(0, 1).AsyncWaitForCompletion();
        //}

        //
        token.ThrowIfCancellationRequested();
        introText.text = introTexts[introTexts.Length - 2];
        SoundManager.Instance.PlaySFX("LineWhoosh");
        introText.alpha = 1;
        await RevealText(introText, token);
        await UniTask.WaitForSeconds(1f);  
        await introText.DOFade(0, 1).AsyncWaitForCompletion();

        token.ThrowIfCancellationRequested();
        introText.text = introTexts[introTexts.Length - 1];
        SoundManager.Instance.PlaySFX("Impact");
        introText.alpha = 1;
        await RevealText(introText, token);
        await UniTask.WaitForSeconds(1f);  
        await introText.DOFade(0, 1).AsyncWaitForCompletion();

    }

    private async Task ShowText(string text, int fontSize, CancellationToken token)
    {
        token.ThrowIfCancellationRequested();

        introText.fontSize = fontSize;
        introText.text = text;

        await introText.DOFade(1, 1f).AsyncWaitForCompletion();
        //await Task.Delay((int)shortDelay, token);
        await UniTask.WaitForSeconds(1.5f);

        await introText.DOFade(0, 1).AsyncWaitForCompletion();
        token.ThrowIfCancellationRequested();

    }

    private async Task RevealText(TextMeshProUGUI text, CancellationToken token)
    {
        text.gameObject.SetActive(true);
        text.ForceMeshUpdate();
        TMP_TextInfo textInfo = text.textInfo;

        text.maxVisibleCharacters = 0;
        int total = textInfo.characterCount;

        for (int i = 0; i <= total; i++)
        {
            token.ThrowIfCancellationRequested();
            text.maxVisibleCharacters = i;
            await UniTask.WaitForSeconds(0.015f);
        }
    }

    private void BrushTitleText()
    {
        titleText.gameObject.SetActive(true);
        titleText.DOFade(1,2);
        //maskRectTransform.sizeDelta = new Vector2(0f, titleText.rectTransform.sizeDelta.x);
        //maskRectTransform.DOSizeDelta(new Vector2(titleText.rectTransform.sizeDelta.x, titleText.rectTransform.sizeDelta.y), 2f).SetEase(Ease.Linear);
    }
}
