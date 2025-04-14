using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum PopUpType
{
    Setting,
    Menu,
    SelectMap,
    Confirm,
    MiniGame,
    MailBox,
    Alarm,
    QuestLetter
}


public class UIManager : MonoSingleton<UIManager>
{
    private Dictionary<PopUpType, BasePopUp> popUps = new Dictionary<PopUpType, BasePopUp>();

    public ConfirmPopUp confirmPopUp;
    public AlarmPopUp alarmPopUp;

    public override void Init()
    {
        base.Init();
    }

    public BasePopUp ShowPopUp(PopUpType type)
    {
        if (!popUps.TryGetValue(type, out BasePopUp popUp))
        {
            GameObject go = Instantiate(LoadPopUpResource(type.ToString()));
            popUp = go.GetComponentInChildren<BasePopUp>();
            popUp.Init();
            popUps.Add(type, popUp);
        }

        if (type == PopUpType.Confirm)
        {
            confirmPopUp = popUp.GetComponent<ConfirmPopUp>();
        }
        else if (type == PopUpType.Alarm)
        {
            alarmPopUp = popUp.GetComponent<AlarmPopUp>();
        }
        popUp.OnOpen();
        return popUp;
    }

    public void HidePopUp(PopUpType type)
    {
        if (popUps.TryGetValue(type, out BasePopUp popUp))
        {
            popUp.OnClose();
        }
    }

    private GameObject LoadPopUpResource(string resourceName)
    {
        GameObject resource = Resources.Load<GameObject>($"UI/PopUp/{resourceName}PopUpPrefab");
        return resource;
    }
}
