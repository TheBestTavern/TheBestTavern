using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameUI : MonoBehaviour
{
    [SerializeField] private Button closeButton;

    private void Awake()
    {
        closeButton.onClick.AddListener(OnClickCloseButton);
    }

    public void OnMiniGameUI()
    {
        gameObject.SetActive(true);
    }

    private void OnClickCloseButton()
    {
        CookingMiniGameManager.Instance.CloseMiniGame();
        gameObject.SetActive(false);
    }
}
