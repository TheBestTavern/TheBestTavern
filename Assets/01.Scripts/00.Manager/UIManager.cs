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
    Quest,
    Confirm,
}


public class UIManager : MonoSingleton<UIManager>
{
    private Dictionary<PopUpType, BasePopUp> popUps = new Dictionary<PopUpType, BasePopUp>();

    public ConfirmPopUp confirmPopUp;

    public void ShowPopUp(PopUpType type)
    {
        if (!popUps.TryGetValue(type, out var popUp))
        {
            GameObject go = Instantiate(LoadPopUpResource(type.ToString()));
            popUp = go.GetComponentInChildren<BasePopUp>();
            popUps.Add(type, popUp);
        }

        popUp.gameObject.SetActive(true);
    }

    public void HidePopUp(PopUpType type)
    {
        if (popUps.TryGetValue(type, out var popUp))
        {
            popUp.gameObject.SetActive(false);
        }
    }

    private GameObject LoadPopUpResource(string resourceName)
    {
        GameObject resource = Resources.Load<GameObject>($"UI/PopUp/{resourceName}PopUpPrefab");
        return resource;
    }
}
