using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MainTutorialController : BaseTutorialController
{
    [SerializeField] private Button mailBoxButton;
    [SerializeField] private Button cookingSceneButton;
    [SerializeField] private Button gatheringSceneButton;
    [SerializeField] private Button sleepButton;

    private string[] mainScenetexts = { "안녕하세요 이 세계에 오신 용사... 아니 요리사님", "이곳에 갑자기 오시게 된 당신을 위해서",
        "어떻게 하면 원래 세계로 돌아갈 수 있는지 설명해드리려고 합니다.",
        "이곳은 매일 의뢰가 들어오는 의뢰함입니다.", "의뢰를 받은 날짜로부터 며칠 뒤에 완료를 할 것인지 정할 수 있습니다.",
        "Tab을 눌러 현재 진행중인 의뢰를 포함한","여러가지 메뉴를 볼 수 있습니다." ,
        "의뢰에서 원하는 음식을 구체적으로 설명하지 않으니", "내용을 보고 잘 추측해 보시길 바랍니다.",
        "이곳을 누르면 의뢰에 필요한 재료를 찾으러 갈 수 있습니다.", "지역, 장소, 계절에 따라 다른 재료들을 얻을 수 있습니다.",
        "이곳을 누르면 재료를 가지고 요리를 할 수 있는 주방에 갈 수 있습니다.", "이곳을 누르면 잠을 잘 수 있습니다.",
        "잠을 자고 날짜가 지나면 NPC가 와서 의뢰를 완료할 수 있습니다.",
        "그럼 이제 재료를 모으러 가보겠습니다."};

    public async override void OnClickNextButton()
    {
        if (textIndex >= mainScenetexts.Length)
            return;

        if (isTexting)
        {
            return;
        }

        ShowText(mainScenetexts[textIndex]);

        switch (textIndex)
        {
            case 3:
                npcImage.DOFade(0, 1f);
                StartFlashingButton(mailBoxButton);
                break;
            case 9:
                flashTokenSource?.Cancel();
                StartFlashingButton(gatheringSceneButton);
                break;
            case 11:
                flashTokenSource?.Cancel();
                StartFlashingButton(cookingSceneButton);
                break;
            case 12:
                flashTokenSource?.Cancel();
                StartFlashingButton(sleepButton);
                break;
            case 14:
                nextButton.gameObject.SetActive(false);
                flashTokenSource?.Cancel();
                await Task.Delay(2000);
                await SceneLoader.Instance.LoadSceneAsync("TutorialForestGatheringScene");
                break;
        }

        textIndex++;
    }
}
