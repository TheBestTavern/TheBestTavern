using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingUI : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public Slider progressSlider;
    public TextMeshProUGUI progressText;

    private async UniTask Fade(float from, float to, float duration)
    {
        float time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(from, to, time / duration);
            await UniTask.Yield();
        }
        canvasGroup.alpha = to;
    }

    public async Task FadeIn() => await Fade(0f, 1f, 0.5f);
    public async Task FadeOut() => await Fade(1f, 0f, 0.5f);

    public void SetProgress(float progress)
    {
        if (progressSlider != null)
            progressSlider.value = progress;
        if (progressText != null)
            progressText.text = progress.ToString("P0");
    }
}
