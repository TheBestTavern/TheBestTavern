using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartSceneUI : MonoBehaviour
{
    [SerializeField] private Button gameStartButton;
    void Start()
    {
        UIManager.Instance.startSceneUI = this;

        gameStartButton.onClick.AddListener(OnClickGameStartButton);
    }

    private async void OnClickGameStartButton()
    {
        SoundManager.Instance.PlayBGM("Main1");
        await SceneLoader.Instance.LoadSceneAsync("MainScene");
    }
}
