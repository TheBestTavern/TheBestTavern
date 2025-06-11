using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class EndingController : MonoBehaviour
{
    [SerializeField] private EndingSceneUI ui;


    private async void Start()
    {
        await StartEndingScene(); // 테스트용
    }
    public async UniTask StartEndingScene()
    {
        await ui.ShowText("그대, 하늘의 뜻을 모두 담았도다.");
        await ui.ShowText("이제 돌아갈지 남을지 그대가 선택할지니.");
        
        int choice = await ui.ShowChoices("돌아간다", "남는다");

        if (choice == 0) await PlayHomeEnding();

        else await PlayStayEnding();

    }

    private async UniTask PlayHomeEnding() 
    { await ui.ShowHomeEnding();
      SaveLoadManager.Instance.SaveData();
    }
    private async UniTask PlayStayEnding() 
    { 
        await ui.ShowStayEnding();
        SaveLoadManager.Instance.SaveData();
    }

}
