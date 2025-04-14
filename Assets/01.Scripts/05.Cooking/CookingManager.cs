using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// 요리 미니게임 관리 (시작, 종료) 
/// </summary>
public class CookingManager : MonoSingleton<CookingManager>
{
    ICookingState cookingState;
    public CookingMiniGameController controller;
    ICookingMiniGameHandler currentMiniGame;


    // 게임 시작

    private void Start()
    {
       // string currentSceneName = SceneManager.GetActiveScene().name;


       // currentMiniGame = controller.Init();
        currentMiniGame.StartGame();
    }


    private void Update()
    {
        currentMiniGame.UpdateGame();
    }

    // 게임 종료
}
