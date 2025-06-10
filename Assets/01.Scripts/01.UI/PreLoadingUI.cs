using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class PreLoadingUI : MonoBehaviour
{
    [SerializeField] private GameObject acceptPanel;
    [SerializeField] private Button acceptButton;
    [SerializeField] private Button rejectButton;

    [SerializeField] private GameObject progressPanel;
    [SerializeField] private Slider progressBar;
    [SerializeField] private TextMeshProUGUI progressText;

    private void Start()
    {
        acceptButton.onClick.AddListener(PreLoadingStart);
        rejectButton.onClick.AddListener(ExitGame);
    }

    private async void PreLoadingStart()
    {
        acceptPanel.SetActive(false);
        progressPanel.SetActive(true);
        acceptButton.interactable = false;

        var downloadHandle = Addressables.DownloadDependenciesAsync("PreLoad");

        while (!downloadHandle.IsDone)
        {
            SetProgress(downloadHandle.PercentComplete);
            await UniTask.Yield();
        }

        if (downloadHandle.Status == AsyncOperationStatus.Succeeded)
        {
            SetProgress(1f);
            SetDone();
        }
        else
        {
            Debug.LogError("에셋 다운로드 실패");
        }

        Addressables.Release(downloadHandle);
        await UniTask.Delay(1000);
    }


    public void SetProgress(float progress)
    {
        progressBar.value = progress;
        progressText.text = $"{(progress * 100f):0.0}%";
    }

    public void SetDone()
    {
        progressBar.value = 1f;
        progressText.text = "완료!";
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    private void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
