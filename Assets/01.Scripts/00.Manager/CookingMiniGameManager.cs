using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CookingMiniGameManager : MonoSingleton<CookingMiniGameManager>
{
    [SerializeField] private MiniGameUI miniGameUI;
    private string miniGameName;

    public void ShowMiniGame(string sceneName)
    {
        miniGameUI.OnMiniGameUI();
        miniGameName = sceneName;
        SceneManager.LoadSceneAsync(miniGameName, LoadSceneMode.Additive);
    }

    public void CloseMiniGame()
    {
        SceneManager.UnloadSceneAsync(miniGameName);
    }
}
