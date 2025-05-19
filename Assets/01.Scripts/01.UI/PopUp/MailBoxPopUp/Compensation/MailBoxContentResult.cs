using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MailBoxContentResult : MailBoxContentBase // 제네릭으로 할 수 있을려나
{
    public async override void OnEnable()
    {
        if (isReadyTodaySlot) return;
        isReadyTodaySlot = true;
        base.OnEnable();

        MakeSlot(QuestManager.Instance.questData.JustCompleteQuests);

        currentLetter = (QuestBaseLetter)await PopUpManager.Instance.ShowPopUp(PopUpType.ResultLetter);
        currentLetter.OnClickCloseButton();
        currentLetter.FirstInit(RemoveSlot);
        //OnNewDay command = new(this);
        //DayManager.Instance.AddCommand(command);
    }

    //public async override void OpenLetter(Quest quest, QuestBaseSlot slot)
    //{
    //    //1. 편지 띄우기
    //    currentLetter = await PopUpManager.Instance.ShowPopUp(PopUpType.ResultLetter) as QuestBaseLetter;
    //    base.OpenLetter(quest, slot);
    //}

    public void MakeSlot(List<int> quests)
    {
        QuestBaseSlot pref;
        int i = 1;
        foreach (var questID in quests)
        {
            pref = Instantiate(slotPref, slotPrt);
            pref.Init(this);
            pref.SetSlot(questID, i);
            slots.Add(pref);
            i++;
        }
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