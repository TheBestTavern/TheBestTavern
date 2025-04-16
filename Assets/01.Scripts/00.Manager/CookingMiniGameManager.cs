
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public class CookingMiniGameManager : MonoSingleton<CookingMiniGameManager>
{
    [SerializeField] private MiniGameUI miniGameUI;

    private ICookingMiniGameHandler currentGame;

    async public void ShowMiniGame(string miniGameSceneName)
    {
        miniGameUI.OnMiniGameUI();
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
    }
}
