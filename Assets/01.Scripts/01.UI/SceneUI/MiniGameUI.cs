using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameUI : MonoBehaviour
{
    [SerializeField] private GameObject miniGame;
    [SerializeField] private Button closeButton;

    private void Awake()
    {
        closeButton.onClick.AddListener(OnClickCloseButton);
    }

    public void OnMiniGameUI()
    {
        miniGame.SetActive(true);
    }

    private void OnClickCloseButton()
    {
        MiniGameManager.Instance.CloseMiniGame();
        miniGame.SetActive(false);
    }
}
