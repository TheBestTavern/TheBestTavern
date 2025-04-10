using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SceneLoader : MonoSingleton<SceneLoader>
{
    [SerializeField] GameObject loadingUIPrefab;
    LoadingUI loadingUI;


    public async UniTask LoadSceneAsync(string sceneName)
    {
        await ShowLoadingScene();

        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        op.allowSceneActivation = false;

        while (op.progress < 0.9f)
        {
            loadingUI.SetProgress(op.progress);
            await UniTask.Yield();
        }

        op.allowSceneActivation = true;

        float timer = 0f;
        while (timer < 1f)
        {
            timer += Time.deltaTime;
            loadingUI.SetProgress(Mathf.Lerp(0.9f, 1f, timer));
            await UniTask.Yield();
        }

        await HideLoadingScene();      
    }
    async Task ShowLoadingScene()
    {
        loadingUI = Instantiate(Resources.Load<GameObject>("UI/LoadingUIPrefab")).GetComponent<LoadingUI>();
        DontDestroyOnLoad(loadingUI.gameObject);
        await loadingUI.FadeIn();
    }
    async Task HideLoadingScene()
    {
        await loadingUI.FadeOut();
        Destroy(loadingUI.gameObject);
    }
}
