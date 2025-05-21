using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 씬 이동시 로딩 UI
/// </summary>
public class LoadingUI : MonoBehaviour
{
    // 페이드 인 아웃용 캔버스 그룹
    public CanvasGroup canvasGroup;

    // 진행률 슬라이더 
    public Slider progressSlider;
    // 진행률 텍스트 
    public TextMeshProUGUI progressText;

    /// <summary>
    /// 페이드 함수
    /// </summary>
    /// <param name="from">시작 값</param>
    /// <param name="to">끝 값</param>
    /// <param name="duration">재생 시간</param>
    /// <returns></returns>

    private void Awake()
    {
        // 씬 넘어가도 파괴 금지
        DontDestroyOnLoad(gameObject);
    }

    private async UniTask Fade(float from, float to, float duration)
    {
        float time = 0f;
        // 재생 시간 동안 
        while (time < duration)
        {
            time += Time.deltaTime;
            // 시작 값과 끝 값 사이 보간
            canvasGroup.alpha = Mathf.Lerp(from, to, time / duration);
            await UniTask.Yield();
        }

        // 마지막 값 고정
        canvasGroup.alpha = to;
    }

    // 페이드 인 
    public async Task FadeIn() => await Fade(0f, 1f, 0.5f);

    // 페이드 아웃
    public async Task FadeOut() => await Fade(1f, 0f, 0.5f);

    // 진행률 설정 함수
    public void SetProgress(float progress)
    {
        if (progressSlider != null)
            progressSlider.value = progress;
        if (progressText != null)
            // 진행률 퍼센트 형식으로 설정 
            progressText.text = progress.ToString("P0");
    }
}
