using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IntroSceneUI : MonoBehaviour
{
    [SerializeField] private Image blackBackGround;
    [SerializeField] private Image tractor;
    [SerializeField] private Image tractorLeftLights;
    [SerializeField] private Image tractorRightLights;
    [SerializeField] private Image startSceneImage;
    [SerializeField] private Image paper;
    [SerializeField] private Image framingImage;

    [SerializeField] private TextMeshProUGUI introText;
    [SerializeField] private TextMeshProUGUI titleText;

    [SerializeField] private Image gameStartBtnImage;
    [SerializeField] private TextMeshProUGUI gameStartBtnText;

    private string[] introTexts = { "강원도 시골", "나는 주말마다 부모님을 위해 손을 거든다.", "오늘도 여느때와 다를것이 없었고", "평소처럼 논두렁으로 향하던 그때", "눈을 떠보니 이곳은 내가 있던 곳이 아니었다.", "눈앞에 펼쳐진 건 고요하고 낯선 이세계 조선.", "그 옆에는 허름한 주막과 함께", "이런 쪽지가 떨어져 있었다.", "옥황상제 특명", "지상 요리 도감 완성" };

    private async void Start()
    {
        introText.gameObject.SetActive(false);
        await blackBackGround.DOFade(0, 2).AsyncWaitForCompletion();

        introText.gameObject.SetActive(true);

        for (int i = 0; i < 4; i++)
        {
            if (i == 3)
            {
                blackBackGround.DOFade(1, 2);
            }
            await ShowText(introTexts[i]);
        }
        await introText.DOFade(0, 1).AsyncWaitForCompletion();
        framingImage.gameObject.SetActive(false);

        tractor.gameObject.SetActive(true);
        tractor.rectTransform.DOScale(2f, 3f);
        tractorLeftLights.rectTransform.DOScale(8f, 3f);
        await tractorRightLights.rectTransform.DOScale(8f, 3f).AsyncWaitForCompletion();

        blackBackGround.color = new Color(0, 0, 0, 0);
        blackBackGround.transform.SetAsLastSibling();
        await blackBackGround.DOFade(1, 2).AsyncWaitForCompletion();
        tractor.gameObject.SetActive(false);
        for (int i = 4; i < introTexts.Length - 2; i++)
        {
            await ShowText(introTexts[i]);
        }
        for (int i = introTexts.Length - 2; i < introTexts.Length; i++)
        {
            await ShowText2(introTexts[i]);
        }
        await Task.Delay(1000);
        paper.gameObject.SetActive(true);
        startSceneImage.gameObject.SetActive(true);
        blackBackGround.DOFade(0, 2);
        await introText.DOFade(0, 2).AsyncWaitForCompletion();

        await Task.Delay(1000);

        await paper.rectTransform.DOMoveY(-1500, 2f).AsyncWaitForCompletion();

        RevealText();
        gameStartBtnImage.gameObject.SetActive(true);
        gameStartBtnImage.DOFade(1, 2);
        gameStartBtnText.DOFade(1, 2);
    }

    private async Task ShowText(string text)
    {
        await introText.DOFade(0, 1).AsyncWaitForCompletion();
        introText.text = text;
        await introText.DOFade(1, 1.5f).AsyncWaitForCompletion();
        await Task.Delay(1500); // 텍스트가 보이는 시간
    }

    private async Task ShowText2(string text)
    {
        await introText.DOFade(0, 1).AsyncWaitForCompletion();
        introText.fontSize = 200;
        introText.text = text;
        await introText.DOFade(1, 1.5f).AsyncWaitForCompletion();
        await Task.Delay(1500); // 텍스트가 보이는 시간
    }

    private async Task RevealText()
    {
        titleText.gameObject.SetActive(true);
        titleText.ForceMeshUpdate();
        TMP_TextInfo textInfo = titleText.textInfo;
        titleText.maxVisibleCharacters = 0;

        int totalVisibleCharacters = textInfo.characterCount;
        int counter = 0;

        while (counter <= totalVisibleCharacters)
        {
            titleText.maxVisibleCharacters = counter;
            counter++;
            await Task.Delay(200);
        }
    }
}
