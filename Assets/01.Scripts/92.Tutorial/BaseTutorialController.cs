using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseTutorialController : MonoBehaviour
{
    [SerializeField] protected GameObject basePanel;
    [SerializeField] protected TextMeshProUGUI npcText;
    [SerializeField] protected Image npcImage;
    [SerializeField] protected Button nextButton;

    protected int textIndex = 0;

    protected bool isTexting = false;

    protected CancellationTokenSource flashTokenSource;

    private void Start()
    {
        nextButton.onClick.AddListener(OnClickNextButton);
        npcText.text = "";
        basePanel.GetComponent<CanvasGroup>().DOFade(1, 1.5f);
        npcImage.DOFade(1, 1.5f).OnComplete(() =>
        {
            OnClickNextButton();
        });       
    }

    public abstract void OnClickNextButton();


    protected async Task ShowText(string text)
    {
        isTexting = true;
        npcText.text = "";

        SoundManager.Instance.PlaySFX("TutorialLine");
        for (int i = 0; i < text.Length; i++)
        {
            npcText.text += text[i];
            await UniTask.WaitForSeconds(0.05f);
        }

        isTexting = false;
    }
    protected void StartFlashingButton(Button button)
    {
        flashTokenSource = new CancellationTokenSource();
        _ = FlashButton(button, flashTokenSource.Token);
    }

    protected async Task FlashButton(Button button, CancellationToken token)
    {
        Image image = button.GetComponent<Image>();
        Color originalColor = image.color;

        try
        {
            while (true)
            {
                token.ThrowIfCancellationRequested();
                await image.DOColor(new Color(1, 0f, 0), 0.5f).AsyncWaitForCompletion();
                await image.DOColor(new Color(1, 1, 1), 0.5f).AsyncWaitForCompletion();
            }
        }
        catch (OperationCanceledException)
        {
            image.color = originalColor; // 원래 색상 복원
        }
    }

}
