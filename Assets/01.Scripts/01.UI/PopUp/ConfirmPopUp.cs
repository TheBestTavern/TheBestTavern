using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmPopUp : BasePopUp
{
    [SerializeField] private Button confirmButton;
    [SerializeField] private TextMeshProUGUI confirmText;
    public Action confirmAction;

    public override void Awake()
    {
        base.Awake();
        confirmButton.onClick.AddListener(OnClickConfirmButton);
    }

    void OnClickConfirmButton()
    {
        confirmAction?.Invoke();
    }

    public void SetConfirmText(string text)
    {
        confirmText.text = text;
    }
}
