using System;
using System.Collections;
using System.Collections.Generic;
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

    public override void Init()
    {
        base.Init();
        popUpType = PopUpType.MailBox;

        for(int i  = 0; i < mailBoxContentsList.Count; i++)
        {
            MailBoxContentType temp = mailBoxContentsList[i].ContentType;
            contentsDic.Add(temp, mailBoxContentsList[i]);
            contentsBtnDic.Add(temp, contentBtn[i]);
        }

        QuestManager.Instance.mailBoxContentQuest = contentsDic[MailBoxContentType.Offer] as MailBoxContentOffer;
        QuestManager.Instance.mailBoxContentCompensation = contentsDic[MailBoxContentType.Result] as MailBoxContentResult;

        foreach(var btnPair in contentsBtnDic)
        {
            btnPair.Value.onClick.AddListener(() => OnClick(btnPair.Key));
        }
    }

    private void OnClick(MailBoxContentType targetContent)
    {
        foreach(var contentPair in contentsDic)
        {
            if(targetContent != contentPair.Key)
            {
                contentPair.Value.gameObject.SetActive(false);
            }
            else
            {
                contentPair.Value.gameObject.SetActive(true);
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
        gameObject.SetActive(false);
    }
}
