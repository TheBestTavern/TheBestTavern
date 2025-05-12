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

    // Update is called once per frame
    void Update()
    {

    }

    private async void OnClickGameStartButton()
    {
        await SceneLoader.Instance.LoadSceneAsync("MainScene");
    }
}
