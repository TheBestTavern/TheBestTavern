using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MailBoxContentResult : MailBoxContentBase // 제네릭으로 할 수 있을려나
{
    public override void OnEnable()
    {
        if (isReadyTodaySlot) return;
        isReadyTodaySlot = true;
        base.OnEnable();

        MakeSlot(QuestManager.Instance.questData.JustCompleteQuests);
        //OnNewDay command = new(this);
        //DayManager.Instance.AddCommand(command);
    }

    public async override void OpenLetter(Quest quest, QuestBaseSlot slot)
    {
        //1. 편지 띄우기
        currentLetter = await PopUpManager.Instance.ShowPopUp(PopUpType.ResultLetter) as QuestBaseLetter;

        base.OpenLetter(quest, slot);
    }

    //public class OnNewDay : IDayCommand
    //{
    //    MailBoxContentResult prt;
    //    public OnNewDay(MailBoxContentResult mailBoxContentOffer)
    //    {
    //        this.prt = mailBoxContentOffer;
    //    }

    //    public int Priority => 2000;

    //    public void Execute()
    //    {
    //        prt.MakeSlot(QuestManager.Instance.questData.JustCompleteQuests);
    //        //QuestManager.Instance.questData.JustCompleteQuests.Clear();
    //        Debug.Log("의뢰 결과창 슬롯 생성");

    //    }
    //}
}