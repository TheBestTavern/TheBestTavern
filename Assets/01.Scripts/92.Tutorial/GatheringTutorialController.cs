using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GatheringTutorialController : BaseTutorialController
{
    private string[] gatheringScenetexts = { "이곳은 채집을 할 수 있는 산입니다." ," 이곳에 있는 나무나 수풀들을 눌러 \n재료를 얻을 수 있습니다.",
        "위에 있는 보따리가 가득 차면 \n더이상 채집을 할 수 없습니다.", "간혹 가다 야생 동물이 튀어나올 수 \n있으니 조심하시기 바랍니다." ,
        "야생동물을 만났을 때는 당황하지말고", "동물의 크기에 따라 대처해보세요.", "작은 동물은 스페이스바를 눌러 \n돌을 던져 잡을 수 있고", "큰 동물들은 미끼를 던져야합니다.",
        "그렇게 해서 야생동물을 잡았다면 \n고기를 얻을 수 있습니다.", "반대로 바다에서 낚시를 하면 \n해산물을 얻을 수 있습니다",
        "낚시를 할 때는 F를 눌러 시작하고", "스페이스바를 눌러 줄을 당겨보세요", "힘 조절이 필요할거에요!",
        "자 이제 요리를 하러 가보도록 하죠 "};

    [SerializeField] private TutorialGatheringProps[] gatheringPorps;
    TutorialGatheringManager tutorialGatheringManager;

    int videoIndex = 0;


    public Button NextButton => nextButton;

    public async override void OnClickNextButton()
    {
        if (textIndex >= gatheringScenetexts.Length)
            return;

        if (isTexting)
        {
            return;
        }

        ShowText(gatheringScenetexts[textIndex]);

        switch (textIndex)
        {
            case 1:
                npcImage.DOFade(0, 1f);
                break;
            case 4:
                tutorialGatheringManager = GatheringManager.Instance as TutorialGatheringManager;
                tutorialGatheringManager.tutorialVideoPlayerController.PlayTutorialVideo(videoIndex);
                videoIndex++;
                break;
            case 6:
                tutorialGatheringManager.tutorialVideoPlayerController.videoPlayer.Stop();
                tutorialGatheringManager.tutorialVideoPlayerController.PlayTutorialVideo(videoIndex);
                videoIndex++;
                break;
            case 9:
                tutorialGatheringManager.tutorialVideoPlayerController.videoPlayer.Stop();
                tutorialGatheringManager.tutorialVideoPlayerController.PlayTutorialVideo(videoIndex);
                break;
            case 13:
                NextButton.gameObject.SetActive(false);
                await UniTask.WaitForSeconds(3f);
                await SceneLoader.Instance.LoadSceneAsync("TutorialCookingScene");
                break;
        }

        textIndex++;
    }
}
