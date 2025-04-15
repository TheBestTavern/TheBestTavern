using System.Collections.Generic;
using UnityEngine;

public class MailBoxContentResult : MailBoxContentBase // 제네릭으로 할 수 있을려나
{
    [SerializeField] ResultSlot resultSlotPref;
    [SerializeField] Transform slotPrt;

    List<ResultSlot> resultSlots = new();

    public ResultLetter currentLetter;

    bool isReady;

    private void OnEnable()
    {
        MakeSlot();
    }

    public void MakeSlot()
    {
        if (isReady) return;

        // 1. RewardSlot 생성(JustCompleteQuests 목록을 통해서 RewardSlot을 생성하고 slotPrt 밑에 붙이기.) (슬롯 pool로 관리하면 좋을듯)
        ResultSlot pref;
        int i = 1;
        foreach (var quest in QuestManager.Instance.questData.JustCompleteQuests)
        {
            pref = Instantiate(resultSlotPref, slotPrt);
            pref.Init(this);
            pref.SetSlot(quest, i);
            resultSlots.Add(pref);
            Debug.Log($"{quest.origin.name} 보상 편지 슬롯 생성 완료");
            i++;
        }

        // 2. isReady true로 바꾸기.
        isReady = true;
    }


    /// <summary>
    /// 다른 곳에서 사용할 함수
    /// </summary>

    public void OpenLetter(Quest quest, ResultSlot resultSlot)
    {
        //1. 편지 띄우기
        currentLetter = UIManager.Instance.ShowPopUp(PopUpType.ResultLetter) as ResultLetter;
        if (currentLetter == null)
        {
            Debug.LogError("편지가 null입니다.");
            return;
        }

        //2. 초기화
        currentLetter.FirstInit(quest);

        // 3. 편지 내용 채우기
        currentLetter.EveryInit(quest, resultSlot);
    }

    public void RemoveResultSlot(ResultSlot resultSlot)
    {
        Destroy(resultSlot.gameObject);
        resultSlots.Remove(resultSlot);
    }
}