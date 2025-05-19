using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class StartSceneUI : MonoBehaviour
{
    [SerializeField] private Button gameStartButton;
    [SerializeField] private Button gameLoadStartButton;
    void Start()
    {
        UIManager.Instance.startSceneUI = this;

        gameStartButton.onClick.AddListener(OnClickGameStartButton);
        gameLoadStartButton.onClick.AddListener(OnClickGameLoadStartButton);
    }

    private async void OnClickGameStartButton()
    {
        SoundManager.Instance.PlayBGM("MainBGM1");
        await SceneLoader.Instance.LoadSceneAsync("MainScene");
    }
    private async void OnClickGameLoadStartButton()
    {
        await SceneLoader.Instance.LoadSceneAsync("MainScene");
    }
}
