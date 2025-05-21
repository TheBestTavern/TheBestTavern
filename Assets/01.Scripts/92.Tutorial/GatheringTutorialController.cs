using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GatheringTutorialController : BaseTutorialController
{
    private string[] gatheringScenetexts = { "이곳은 채집을 할 수 있는 산입니다." ," 이곳에 있는 나무나 수풀들을 눌러 재료를 얻을 수 있습니다.", 
        "위에 있는 보따리가 가득 차면 더이상 채집을 할 수 없습니다.", "간혹 가다 야생 동물이 튀어나올 수 있으니 조심하시기 바랍니다." , 
        "이런 바로 야생동물이 튀어나왔네요.", "야생동물을 만났을 때는 왼쪽 위에 있는", "설명을 잘 읽어보시고 대처하시길 바랍니다",
        "이제 요리를 하러 가보도록 하죠"};

    [SerializeField] private TutorialGatheringProps[] gatheringPorps;

    public Button NextButton => nextButton;

    public async override void OnClickNextButton()
    {
        if (textIndex >= gatheringScenetexts.Length)
            return;

        if (isTexting)
        {
            return;
        }

        await ShowText(gatheringScenetexts[textIndex]);

        switch (textIndex)
        {
            case 1:
                npcImage.DOFade(0, 1f);
                NextButton.gameObject.SetActive(false);
                await Task.Delay(1500);
                textIndex++;
                OnClickNextButton();
                return;
            case 2:
                await Task.Delay(1500);
                textIndex++;
                OnClickNextButton();
                return;
            case 4:
                await Task.Delay(1500);
                textIndex++;
                OnClickNextButton();
                return;
            case 5:
                await Task.Delay(1500);
                textIndex++;
                OnClickNextButton();
                return;
            case 6:
                await Task.Delay(1500);
                textIndex++;
                OnClickNextButton();
                return;
            case 7:
                await Task.Delay(2500);
                //await SceneLoader.Instance.LoadSceneAsync("TutorialCookingScene");
                return;
        }

        textIndex++;
    }
}
