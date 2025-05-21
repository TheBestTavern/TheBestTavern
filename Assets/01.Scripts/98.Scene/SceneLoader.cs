using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

/// <summary>
/// 씬 불러오기 클래스
/// </summary>
public class SceneLoader : MonoSingleton<SceneLoader>
{

    BaseScene currentScene;

    // 로딩 UI 클래스
    LoadingUI loadingUI;

    // 현재 Additive로 로드된 미니게임 씬
    SceneInstance miniGameInstance;

    // Addressables 초기화용 bool 
    bool isInitializeAsync = false;

    public override void Init()
    {
        if (_isInitialized) return;
        base.Init();

        DontDestroyOnLoad(gameObject);

        currentScene = FindObjectOfType<BaseScene>();
    }

    // 비동기로 씬 불러오기 함수
    public async UniTask LoadSceneAsync(string sceneName)
    {
        SoundManager.Instance.PlaySFX("SceneMoveButton");

        // 한번도 씬을 불러온 적이 없으면 
        if (!isInitializeAsync)
            // Addressables 초기화
            await Addressables.InitializeAsync().ToUniTask();

        currentScene?.OnEixtScene();

        // 로딩 UI 불러오기
        await ShowLoadingUI();

        // 씬 불러오기 
        var loadScene = Addressables.LoadSceneAsync($"{sceneName}.unity");

        // 씬 이동이 끝날때까지 반복
        while (!loadScene.IsDone)
        {
            // 로딩 UI 진행률 설정 
            loadingUI.SetProgress(loadScene.PercentComplete);
            await UniTask.Yield();
        }

        await loadScene;
        currentScene = FindObjectOfType<BaseScene>();

        currentScene?.OnEnterScene();

        // 로딩 UI 없애기
        await HideLoadingUI();
    }

    /// <summary>
    /// 비동기로 미니게임 씬 불러오기 함수
    /// </summary>
    /// <param name="miniGameSceneName">미니게임 씬 이름</param>
    /// <returns></returns>
    public async UniTask LoadSceneAsyncMiniGame(string miniGameSceneName)
    {
        // 미니게임 씬 이름으로 불러오기
        miniGameInstance = await Addressables.LoadSceneAsync($"{miniGameSceneName}.unity", LoadSceneMode.Additive);
        SceneManager.SetActiveScene(miniGameInstance.Scene);
    }

    /// <summary>
    /// 미니게임 닫기 함수
    /// </summary>
    /// <returns></returns>
    public async UniTask UnLoadSceneAsyncMiniGame()
    {
        // 현재 미니게임 닫기
        await Addressables.UnloadSceneAsync(miniGameInstance);
    }

    // 로딩UI 불러오기 함수 
    async Task ShowLoadingUI()
    {
        // 로딩UI Addressables로 불러오고 인스턴스화
        loadingUI = Instantiate(await AddressablesLoader.Instance.AddressablesLoadAsync<GameObject>("LoadingUIPrefab.prefab")).GetComponent<LoadingUI>();
        
        // 로딩 UI 페이드인
        await loadingUI.FadeIn();
    }

    // 로딩UI 없애기 함수 
    async Task HideLoadingUI()
    {
        // 로딩 UI 페이드 아웃 
        await loadingUI.FadeOut();
        // 로딩 UI 파괴
        Destroy(loadingUI.gameObject);
    }
}

