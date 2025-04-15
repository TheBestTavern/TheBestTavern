using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SceneLoader : MonoSingleton<SceneLoader>
{
    [SerializeField] GameObject loadingUIPrefab;
    LoadingUI loadingUI;

    SceneInstance miniGameInstance;

    bool isInitializeAsync = false;

    public async UniTask LoadSceneAsync(string sceneName)
    {
        if (!isInitializeAsync)
            await Addressables.InitializeAsync().ToUniTask();

        await ShowLoadingUI();

        var loadScene = Addressables.LoadSceneAsync($"{sceneName}.unity");

        while (!loadScene.IsDone)
        {
            loadingUI.SetProgress(loadScene.PercentComplete);
            await UniTask.Yield();
        }

        await loadScene.ToUniTask();

        await HideLoadingUI();
    }

    public async UniTask LoadSceneAsyncMiniGame(string miniGameSceneName)
    {
        miniGameInstance = await Addressables.LoadSceneAsync($"{miniGameSceneName}.unity", LoadSceneMode.Additive);
    }

    public async UniTask UnLoadSceneAsyncMiniGame()
    {
        await Addressables.UnloadSceneAsync(miniGameInstance);
    }

    async Task ShowLoadingUI()
    {
        //loadingUI = Instantiate(Resources.Load<GameObject>("UI/LoadingUIPrefab")).GetComponent<LoadingUI>();
        loadingUI = Instantiate(await AddressablesLoader.Instance.AddressablesLoadAsync("LoadingUIPrefab.prefab")).GetComponent<LoadingUI>();
        DontDestroyOnLoad(loadingUI.gameObject);
        await loadingUI.FadeIn();
    }

    async Task HideLoadingUI()
    {
        await loadingUI.FadeOut();
        Destroy(loadingUI.gameObject);
    }
}
