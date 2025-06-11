using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndingSceneUI : MonoBehaviour
{
    //[SerializeField] private Image paper;

    [Header("Home")]
    [SerializeField] private Image homeEnding1;
    [SerializeField] private Image homeEnding2;
    [SerializeField] private Image homeEnding3;

    [Header("Stay")]
    [SerializeField] private Image stayEnding1;
    [SerializeField] private Image stayEnding2;
    [SerializeField] private Image stayEnding3;
    [SerializeField] private Image stayEnding4;

    [Header("기타 이미지")]
    [SerializeField] private Image originalJumak;
    [SerializeField] private Image paper;
    [SerializeField] private Image blackScreen;
    [SerializeField] private Image panel;
    [SerializeField] private Image finish;

    [Header("버튼")]
    [SerializeField] private CanvasGroup btns;
    [SerializeField] private Button btn1;
    [SerializeField] private Button btn2;

    [Header("자막")]
    [SerializeField] private TextMeshProUGUI subtitle;
    [SerializeField] private TextMeshProUGUI finishText;

    public async UniTask ShowHomeEnding()
    {
        paper.gameObject.SetActive(false);
        blackScreen.DOFade(1f, 1f);
        SoundManager.Instance.PlaySFX("Door");
        await UniTask.WaitForSeconds(2f);


        // #0
        // 화면 하얗게 점멸
        // 문 끼익소리

        // #1
        // 블랙아웃
        // 눈깜빡이는 애니메이션
        blackScreen.gameObject.SetActive(true);
        homeEnding1.gameObject.SetActive(true);
        await FadeIn();
        await UniTask.Delay(100);
        await blackScreen.DOFade(0.9f, 0.1f).AsyncWaitForCompletion();
        await UniTask.Delay(100);
        await blackScreen.DOFade(0.9f, 0.1f).AsyncWaitForCompletion();
        await FadeIn();
        await UniTask.Delay(100);
        await blackScreen.DOFade(0f, 1f).AsyncWaitForCompletion();
        await ShowText("여기 환자가 깨어났어요!");

        // #2
        await FadeIn();
        await ShowText("<b><color=#FFFFFF>정신을 차리고 나니 손에는 이세계 조선 요리 도감이 있었고</color></b>");
        await ShowText("<b><color=#FFFFFF>그 안에는 이런 메모가 남겨져 있다.</color></b>");
        await UniTask.WaitForSeconds(1f);
        blackScreen.DOFade(0f, 0.2f);
        homeEnding2.gameObject.SetActive(true);
        await UniTask.WaitForSeconds(3f);


        // #3
        
        await FadeIn();

        await ShowText("<b><color=#FFFFFF>그 뒤로, 나는 골목 어귀에 전통 주점을 열었다.</color></b>");
        blackScreen.DOFade(0f, 0.2f);
        homeEnding3.gameObject.SetActive(true);

        // 마지막 엔딩 이미지
        await UniTask.WaitForSeconds(3f);
        await FadeIn();
        await ShowEndImage();

        // 메인씬으로 이동
        await SceneLoader.Instance.LoadSceneAsync(SceneType.MainScene);
    }

    public async UniTask ShowStayEnding()
    {
        paper.gameObject.SetActive(false);
        blackScreen.DOFade(1f, 1f);
        SoundManager.Instance.PlaySFX("Door");
        await UniTask.WaitForSeconds(2f);

        // #0
        // 문 탁 닫는 소리

        // #1
        // 도깨비 대사
        blackScreen.gameObject.SetActive(true);
        await FadeIn();
        FadeOut();
        stayEnding1.gameObject.SetActive(true);
        await UniTask.WaitForSeconds(3f);

        // #2
        await FadeIn();
        FadeOut();
        stayEnding2.gameObject.SetActive(true);
        panel.gameObject.SetActive(true);
        await ShowText("<b><color=#FFFFFF>콩쥐, 호랑이, 허생, 선녀까지 매일 들락날락한다.</color></b>");
        await ShowText("<b><color=#FFFFFF>주방에서는 여전히 좋은 냄새가 풍긴다.</color></b>");
 
        await FadeIn();
        panel.gameObject.SetActive(false);

        await ShowText("<b><color=#FFFFFF>한편 하늘 위에서 옥황상제는...</color></b>");

        // #3
        await FadeIn();
        FadeOut();
        stayEnding3.gameObject.SetActive(true);
        await UniTask.WaitForSeconds(3f);
        FadeOut();

        // #4
        await FadeIn();
        FadeOut();
        originalJumak.gameObject.SetActive(true);
        stayEnding3.gameObject.SetActive(false);
        stayEnding2.gameObject.SetActive(false);
        stayEnding1.gameObject.SetActive(false);
        await UniTask.WaitForSeconds(1.5f);
        
        Sequence seq = DOTween.Sequence();
        await seq.Append(originalJumak.DOFade(0f, 2f))
           .Join(stayEnding4.DOFade(1f, 2f)).AsyncWaitForCompletion();

        //await blackScreen.DOFade(0.5f, 2f).AsyncWaitForCompletion();
        //blackScreen.DOFade(0f, 2f).AsyncWaitForCompletion();

        //stayEnding4.gameObject.SetActive(true);

        // 마지막 엔딩 이미지
        await UniTask.WaitForSeconds(3f);
        await FadeIn();
        await ShowEndImage();


        // 스타트화면
        await SceneLoader.Instance.LoadSceneAsync(SceneType.IntroScene);
    }

    public async UniTask ShowText(string text)
    {
        subtitle.text = "";
        subtitle.alpha = 0f;
        subtitle.gameObject.SetActive(true);
        SoundManager.Instance.PlaySFX("LineWhoosh");

        foreach (char cha in text) 
        {
            subtitle.text += cha;
        }
        await subtitle.DOFade(1, 2).AsyncWaitForCompletion();
        await UniTask.WaitForSeconds(2f);
        await subtitle.DOFade(0, 1).AsyncWaitForCompletion();
        await UniTask.WaitForSeconds(1f);
        subtitle.gameObject.SetActive(false);
    }

    public async UniTask<int> ShowChoices(string home, string stay)
    {
        //btn1.gameObject.SetActive(true);
        //btn2.gameObject.SetActive(true);
        btns.DOFade(1f, 0.3f);

        var tcs = new UniTaskCompletionSource<int>();
        btn1.GetComponentInChildren<TextMeshProUGUI>().text = home;
        btn2.GetComponentInChildren<TextMeshProUGUI>().text = stay;

        btn1.onClick.RemoveAllListeners();
        btn2.onClick.RemoveAllListeners();

        btn1.onClick.AddListener(() => { tcs.TrySetResult(0); HideBtn(); }); // 돌아간다 0
        btn2.onClick.AddListener(() => { tcs.TrySetResult(1); HideBtn(); }); // 남는다 1

        return await tcs.Task;
    }

    private void HideBtn()
    {
        btns.DOFade(0f, 0.3f);
    }

    private async UniTask FadeIn()
    {
        await blackScreen.DOFade(1f, 2f).AsyncWaitForCompletion();
    }

    private void FadeOut()
    {
        blackScreen.DOFade(0f, 1f);
    }

    private async UniTask ShowEndImage()
    {
        // 완 한자 이미지 출력
        finish.gameObject.SetActive(true);
        finish.transform.DOScale(5f, 0.3f).SetEase(Ease.OutBack);
        finishText.DOFade(1, 2);
        await UniTask.WaitForSeconds(5f);
    }
}
