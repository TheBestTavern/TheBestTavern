using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public enum SceneType
{
    IntroScene,
    MainScene,
    CookingScene,
    ForestGatheringScene,
    SeaGatheringScene,
    TutorialScene,
    TutorialCookingScene,
    TutorialForestGatheringScene,
}

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

    Dictionary<SceneType, BaseScene> sceneMap = new();

    public override void Init()
    {
        if (_isInitialized) return;
        base.Init();

        DontDestroyOnLoad(gameObject);

        sceneMap = new()
        {
            {SceneType.IntroScene,new IntroScene()},
            {SceneType.MainScene,new MainScene()},
            {SceneType.CookingScene,new CookingScene()},
            {SceneType.ForestGatheringScene,new ForestGatheringScene()},
            {SceneType.SeaGatheringScene,new SeaGatheringScene()},
            {SceneType.TutorialScene,new TutorialScene()},
            {SceneType.TutorialCookingScene,new TutorialCookingScene()},
            {SceneType.TutorialForestGatheringScene,new TutorialForestGatheringScene()}
        };
        
        Enum.TryParse<SceneType>(SceneManager.GetActiveScene().name, out var currentSceneType);
        currentScene = sceneMap[currentSceneType];
    }

    // 비동기로 씬 불러오기 함수
    public async UniTask LoadSceneAsync(SceneType sceneType)
    {
        SoundManager.Instance.PlaySFX("SceneMoveButton");

        // 한번도 씬을 불러온 적이 없으면 
        if (!isInitializeAsync)
            // Addressables 초기화
            await Addressables.InitializeAsync().ToUniTask();

        // 로딩 UI 불러오기
        await ShowLoadingUI();

        if (currentScene != null)
        {
            await currentScene.OnExitScene();
        }

        // 씬 불러오기 
        var loadScene = Addressables.LoadSceneAsync($"{sceneType.ToString()}.unity");

        // 씬 이동이 끝날때까지 반복
        while (!loadScene.IsDone)
        {
            // 로딩 UI 진행률 설정 
            loadingUI.SetProgress(loadScene.PercentComplete);
            await UniTask.Yield();
        }

        await loadScene;
        currentScene = sceneMap[sceneType];
        EventBus.Publish<SceneMove>(new SceneMove(sceneType.ToString()));

        if (currentScene != null)
        {
            await currentScene.OnEnterScene();
        }

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

