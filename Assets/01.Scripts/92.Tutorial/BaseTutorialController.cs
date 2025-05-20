using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
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
    [SerializeField] protected Button NextButton;

    protected int textIndex = 0;

    protected bool isTexting = false;

    protected CancellationTokenSource flashTokenSource;
    protected bool isFlashing = false;

    private void Start()
    {
        NextButton.onClick.AddListener(OnClickNextButton);
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

        for (int i = 0; i < text.Length; i++)
        {
            npcText.text += text[i];
            await Task.Delay(50);
        }

        isTexting = false;
    }
    protected void StartFlashingButton(Button button)
    {
        if (isFlashing) return;

        isFlashing = true;
        flashTokenSource = new CancellationTokenSource();
        _ = FlashButton(button, flashTokenSource.Token); // fire-and-forget
    }

    protected async Task FlashButton(Button button, CancellationToken token)
    {
        Image image = button.GetComponent<Image>();
        Color originalColor = image.color;

        try
        {
            while (true)
            {
                await image.DOBlendableColor(new Color(0.9f, 0.9f, 0.9f), 0.5f).AsyncWaitForCompletion();
                await image.DOBlendableColor(new Color(1, 1, 1), 0.5f).AsyncWaitForCompletion();

                token.ThrowIfCancellationRequested();
            }
        }
        catch (OperationCanceledException)
        {
            image.color = originalColor; // 원래 색상 복원
            isFlashing = false;
        }
    }

}
