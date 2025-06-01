using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Unity.Services.Analytics;
using UnityEngine;
using UnityEngine.UI;

public class CookingTutorialController : BaseTutorialController
{
    // 굽기 미니게임 시작 버튼 
    [SerializeField] private Button grillMiniGameButton;

    // 절구 미니게임 시작 버튼
    [SerializeField] private Button grindMiniGameButton;

    // 맷돌 미니게임 시작 버튼
    [SerializeField] private Button millMiniGameButton;

    // 도마 미니게임 시작 버튼
    [SerializeField] private Button cuttingMiniGameButton;

    // 끓이기 미니게임 시작 버튼
    [SerializeField] private Button boilMiniGameButton;

    // 믹싱 볼 미니게임 시작 버튼
    [SerializeField] private Button mixingMiniGameButton;

    // 합치기 버튼
    [SerializeField] private Button plateButton;

    [SerializeField] private TutorialVideoPlayerController videoPlayerController;

    private string[] cookingScenetexts = {"이곳은 요리를 하는 주방입니다.", "요리를 하기 위해서는 사용하고 싶은 도구를 클릭하고",
        "재료를 클릭한 다음 시작을 해주시면 됩니다.",
        "먼저 재료를 갈아주는 맷돌" , "제한시간 동안 맷돌 손잡이를 잡고 돌리면서", "정해진 회전 방향과 속도를 유지해보세요.",
        "다음으로는 재료를 자르는 도마", "제한시간 내로 스페이스 바를 눌러 재료를 균일하게 \n잘라보세요.",
        "다음으로 재료를 빻는 절구" , "제한시간 동안 노트가 내려오는 타이밍에 \n스페이스 바를 정확하게 눌러보세요.",
        "다음으로 재료를 굽는 가마솥 뚜껑", "제한시간 내 같은 숫자의 카드를 최대한 많이 \n뒤집어보세요.",
        "다음으로 재료를 끓이는 가마솥", "제한시간 내 빨간 네모를 클릭하면 나타나는 화살표를 따라 그려보세요.",
        "다음으로 믹싱볼", "게이지가 다 찰 때까지 숟가락을 클릭해 \n재료를 섞어보세요.",
        "마지막으로 접시에서는 재료들을 넣고 \n음식을 완성해보세요.", "자 이제 제가 설명드릴 부분은 여기까지 입니다.",
        "앞으로는 직접 경험해 보세요!"};

    List<Data_Foods> tempFoods = new();
    Data_Foods tempFood = new();

    int videoIndex = 0;

    public async override void OnClickNextButton()
    {
        if (textIndex >= cookingScenetexts.Length)
            return;

        if (isTexting)
        {
            return;
        }

        ShowText(cookingScenetexts[textIndex]);

        switch (textIndex)
        {
            case 3:
                npcImage.DOFade(0, 1f);
                UIManager.Instance.cookingSceneUI.ReadyMiniGame(millMiniGameButton);
                break;
            case 4:
                videoPlayerController.PlayTutorialVideo(videoIndex);
                videoIndex++;
                break;
            case 6:
                videoPlayerController.StopTutorialVideo();
                UIManager.Instance.cookingSceneUI.ButtonsBack();
                UIManager.Instance.cookingSceneUI.ReadyMiniGame(cuttingMiniGameButton);
                break;
            case 7:
                videoPlayerController.PlayTutorialVideo(videoIndex);
                videoIndex++;
                break;
            case 8:
                videoPlayerController.StopTutorialVideo();
                UIManager.Instance.cookingSceneUI.ButtonsBack();
                UIManager.Instance.cookingSceneUI.ReadyMiniGame(grindMiniGameButton);
                break;
            case 9:
                videoPlayerController.PlayTutorialVideo(videoIndex);
                videoIndex++;
                break;
            case 10:
                videoPlayerController.StopTutorialVideo();
                UIManager.Instance.cookingSceneUI.ButtonsBack();
                UIManager.Instance.cookingSceneUI.ReadyMiniGame(grillMiniGameButton);
                break;
            case 11:
                videoPlayerController.PlayTutorialVideo(videoIndex);
                videoIndex++;
                break;
            case 12:
                videoPlayerController.StopTutorialVideo();
                UIManager.Instance.cookingSceneUI.ButtonsBack();
                UIManager.Instance.cookingSceneUI.ReadyMiniGame(boilMiniGameButton);
                break;
            case 13:
                videoPlayerController.PlayTutorialVideo(videoIndex);
                videoIndex++;
                break;
            case 14:
                videoPlayerController.StopTutorialVideo();
                UIManager.Instance.cookingSceneUI.ButtonsBack();
                UIManager.Instance.cookingSceneUI.ReadyMiniGame(mixingMiniGameButton);
                break;
            case 15:
                videoPlayerController.PlayTutorialVideo(videoIndex);
                videoIndex++;
                break;
            case 16:
                videoPlayerController.StopTutorialVideo();
                UIManager.Instance.cookingSceneUI.ButtonsBack();
                UIManager.Instance.cookingSceneUI.ReadyMiniGame(plateButton);
                break;
            case 17:
                UIManager.Instance.cookingSceneUI.ButtonsBack();               
                break;
            case 18:
                nextButton.gameObject.SetActive(false);
                await UniTask.WaitForSeconds(3f);
                await SceneLoader.Instance.LoadSceneAsync(SceneType.MainScene);
                break;
        }

        textIndex++;
    }   
}
