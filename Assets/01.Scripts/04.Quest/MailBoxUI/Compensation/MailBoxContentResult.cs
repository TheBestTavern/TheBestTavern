using System.Collections.Generic;
using UnityEngine;

public class MailBoxContentResult : MailBoxContentBase // 제네릭으로 할 수 있을려나
{
    protected override  void OnEnable()
    {
        if (isReady) return;

        base.OnEnable();
        MakeSlot(QuestManager.Instance.questData.JustCompleteQuests);
    }

    public override void OpenLetter(Quest quest, QuestBaseSlot slot)
    {
        //1. 편지 띄우기
        currentLetter = UIManager.Instance.ShowPopUp(PopUpType.ResultLetter) as QuestBaseLetter;

        base.OpenLetter(quest, slot);
    }
}