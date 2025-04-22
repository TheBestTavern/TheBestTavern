
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public class CookingMiniGameManager : MonoSingleton<CookingMiniGameManager>
{
    public MiniGameUI miniGameUI;
    public CookingSceneUI cookingSceneUI;
    public GameObject mainCamera;

    private ICookingMiniGameHandler currentGame;

    async public void ShowMiniGame(string miniGameSceneName)
    {
        miniGameUI.ResetTimer();
        SettingMiniGame(false);
        await SceneLoader.Instance.LoadSceneAsyncMiniGame(miniGameSceneName);
    }

    public void GetCurrentMiniGame(ICookingMiniGameHandler game)
    {
        currentGame = game;
        currentGame?.StartGame();
    }

    async public void CloseMiniGame()
    {
         await SceneLoader.Instance.UnLoadSceneAsyncMiniGame();
        SettingMiniGame(true);
    }

    void SettingMiniGame(bool active)
    {
        miniGameUI.gameObject.SetActive(!active);
        cookingSceneUI.gameObject.SetActive(active);
        mainCamera.SetActive(active);
    }
}
