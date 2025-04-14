using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AlarmPopUp : BasePopUp
{
    [SerializeField] private TextMeshProUGUI bodyText;

    public override void Awake()
    {
        base.Awake();
        closeButton.onClick.AddListener(OnClickCloseButton);
    }

    public void SetAlarm(string text)
    {
        bodyText.text = text;
    }

    public override void OnClose()
    {
        base.OnClose();
        gameObject.SetActive(false);
    }
}
