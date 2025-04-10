using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MiniGameManager : MonoSingleton<MiniGameManager>
{
    [SerializeField] private MiniGameUI miniGameUI;

    public void ShowMiniGame(string sceneName)
    {
        miniGameUI.OnMiniGameUI();
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    }

    public void CloseMiniGame()
    {
        SceneManager.UnloadSceneAsync("Cooking_Grill_Test");
    }
}
