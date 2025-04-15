using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

public enum PopUpType
{
    Setting,
    Menu,
    SelectMap,
    Confirm,
    MiniGame,
    MailBox,
    Alarm,
    QuestLetter,
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

    public async Task<BasePopUp> ShowPopUp(PopUpType popUpType)
    {
        if (!popUps.TryGetValue(popUpType, out BasePopUp basePopUp))
        {
            GameObject go = await AddressablesLoader.Instance.AddressablesLoadAsync($"{popUpType.ToString()}PopUpPrefab.prefab");
            go = Instantiate(go);
            basePopUp = go.GetComponentInChildren<BasePopUp>();
            popUps.Add(popUpType, basePopUp);
        }


        if (popUpType == PopUpType.Confirm)
        {
            confirmPopUp = basePopUp.GetComponent<ConfirmPopUp>();
        }

        basePopUp.OnOpen();

        return basePopUp;
    }

    public void HidePopUp(PopUpType type)
    {
        if (popUps.TryGetValue(type, out BasePopUp popUp))
        {
            popUp.OnClose();
        }
    }
}
