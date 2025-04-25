
using Cinemachine;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public class CookingMiniGameManager : MonoSingleton<CookingMiniGameManager>
{
    public MiniGameUI miniGameUI;
    public CookingSceneUI cookingSceneUI;

    private ICookingMiniGameHandler currentGame;

    async public void ShowMiniGame(string miniGameSceneName)
    {
        await SceneLoader.Instance.LoadSceneAsyncMiniGame(miniGameSceneName);
        miniGameUI.ResetTimer();
        SettingMiniGame(true);
    }

    public void GetCurrentMiniGame(ICookingMiniGameHandler game)
    {
        currentGame = game;
        currentGame?.StartGame();
    }

    async public void CloseMiniGame()
    {
        await SceneLoader.Instance.UnLoadSceneAsyncMiniGame();
        SettingMiniGame(false);
    }

    void SettingMiniGame(bool active)
    {
        miniGameUI.gameObject.SetActive(active);
    }
}
