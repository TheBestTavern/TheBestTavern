using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasePopUp : MonoBehaviour
{
    [SerializeField] private Button closeButton;

    private void Awake()
    {
        closeButton.onClick.AddListener(OnClickCloseButton);
    }

    void OnClickCloseButton()
    {
        gameObject.SetActive(false);
    }
}
