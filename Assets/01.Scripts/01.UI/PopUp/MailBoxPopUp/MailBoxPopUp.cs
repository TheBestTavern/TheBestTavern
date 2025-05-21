using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static UnityEngine.RuleTile.TilingRuleOutput;

public enum MailBoxContentType
{
    Offer,
    Result
}

public class MailBoxPopUp : BasePopUp 
{
    // BasePopUp : 클로즈 버튼, 팝업 타입, 클로즈버튼에 메서드(UIManager 하이드 팝업) 구독

    MailBoxContentType currentMailBoxContent;
    [SerializeField] List<Button> contentBtn;
    Dictionary<MailBoxContentType, Button> contentsBtnDic = new();
    [SerializeField] List<MailBoxContentBase> mailBoxContentsList;
    public Dictionary<MailBoxContentType, MailBoxContentBase> contentsDic = new();
    public Dictionary<MailBoxContentType, Image> contentsBtnImage = new();

    public override void Init(int id, IPopupManager manager)
    {
        base.Init(id, manager);
        popUpType = PopUpType.MailBox;

        for(int i  = 0; i < mailBoxContentsList.Count; i++)
        {
            //mailBoxContentsList[i].Init();
            MailBoxContentType temp = mailBoxContentsList[i].ContentType;
            contentsDic.Add(temp, mailBoxContentsList[i]);
            contentsBtnDic.Add(temp, contentBtn[i]);
            contentsBtnImage.Add(temp, contentBtn[i].gameObject.GetComponent<Image>());
        }

        foreach(var btnPair in contentsBtnDic)
        {
            btnPair.Value.onClick.AddListener(() => OnClick(btnPair.Key));
        }

        OnClick(MailBoxContentType.Offer);
    }

    private void OnClick(MailBoxContentType targetContent)
    {
        foreach(var contentPair in contentsDic)
        {
            if(targetContent != contentPair.Key)
            {
                contentPair.Value.gameObject.SetActive(false);
                contentsBtnImage[contentPair.Key].DOColor(new Color(0.6f, 0.6f, 0.6f), 0.15f);
            }
            else
            {
                contentPair.Value.gameObject.SetActive(true);
                contentsBtnImage[contentPair.Key].DOColor(new Color(1f, 1f, 1f), 0.15f);
            }
        }
    }

    public override void OnOpen()
    {
        base.OnOpen();
    }

    public override void OnClose()
    {
        base.OnClose();
    }
}
