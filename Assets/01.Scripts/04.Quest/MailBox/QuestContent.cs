using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestContent : MailBoxContent
{
    [SerializeField] QuestSlot questSlotPref;
    [SerializeField] Transform slotPrt;

    Stack<QuestSlot> questSlots = new();

    //[SerializeField] GameObject letterPref;
    public QuestLetter currentLetter;

    bool isReady;

    private void OnEnable()
    {
        Debug.Log("활성화");

        if (!isReady)
        {
            // 0. 전날 있던 퀘스트 리스트 삭제.
            foreach (var slot in questSlots)
            {
                Destroy(questSlots.Pop());
            }

            // 1. QuestSlot을 생성(TodayAvailableQuest 목록을 통해서 QuestSlot을 생성하고 slotPrt 밑에 붙이기.) (슬롯 pool로 관리하면 좋을듯)
            Debug.Log("가능 퀘스트 리스트를 통해 슬롯 생성");
            QuestSlot pref;
            int i = 1;
            foreach (var quest in QuestManager.Instance.questData.TodayAvailableQuest)
            {

                pref = Instantiate(questSlotPref, slotPrt);
                pref.Init(this);
                pref.SetSlot(quest, i);
                Debug.Log($"{quest.origin.name} 퀘스트 슬롯 생성 완료");
                i++;
            }

            // 2. isReady true로 바꾸기.
            isReady = true;
        }
    }

    public async void OpenLetter(Quest quest)
    {
        //1. 편지 띄우기
        currentLetter = await UIManager.Instance.ShowPopUp(PopUpType.QuestLetter) as QuestLetter;
        if(currentLetter == null)
        {
            Debug.LogError("편지가 null입니다.");
            return;
        }

        if (!currentLetter.IsReady)
        {
            //2. 초기화
            currentLetter.FirstInit(quest);
        }

        // 3. 편지 내용 채우기
        currentLetter.EveryInit(quest);
    }
}