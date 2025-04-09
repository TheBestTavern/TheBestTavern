using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmPopUp : BasePopUp
{
    [SerializeField] private Button yesButton;
    public Action conFirmAction;

    public override void Awake()
    {
        base.Awake();
        yesButton.onClick.AddListener(OnClickYesButton);
    }

    void OnClickYesButton()
    {
        conFirmAction?.Invoke();
    }
}
