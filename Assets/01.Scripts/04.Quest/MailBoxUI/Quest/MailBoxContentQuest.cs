using System;
using System.Collections.Generic;
using UnityEngine;

public class MailBoxContentQuest : MailBoxContentBase
{
    [SerializeField] QuestSlot questSlotPref;
    [SerializeField] Transform slotPrt;

    List<QuestSlot> questSlots = new();

    //[SerializeField] GameObject letterPref;
    public QuestLetter currentLetter;

    bool isReady;

    private void OnEnable()
    {
        if (!isReady)
        {
            // 0. 전날 있던 퀘스트 리스트 삭제. 나중에 풀링으로 바꾸기.
            foreach (var slot in questSlots)
            {
                Destroy(slot.gameObject);
            }
            questSlots.Clear();

            // 1. QuestSlot을 생성(TodayAvailableQuest 목록을 통해서 QuestSlot을 생성하고 slotPrt 밑에 붙이기.) (슬롯 pool로 관리하면 좋을듯)
            Debug.Log("가능 퀘스트 리스트를 통해 슬롯 생성");
            QuestSlot pref;
            int i = 1;
            foreach (var quest in QuestManager.Instance.questData.TodayAvailableQuest)
            {
                pref = Instantiate(questSlotPref, slotPrt);
                pref.Init(this);
                pref.SetSlot(quest, i);
                questSlots.Add(pref);
                Debug.Log($"{quest.origin.name} 퀘스트 슬롯 생성 완료");
                i++;
            }

            // 2. isReady true로 바꾸기.
            isReady = true;
        }
    }

    public void OpenLetter(Quest quest, QuestSlot questSlot)
    {
        //1. 편지 띄우기
        currentLetter = UIManager.Instance.ShowPopUp(PopUpType.QuestLetter) as QuestLetter;
        if (currentLetter == null)
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
        currentLetter.EveryInit(quest, questSlot);
    }

    public void RemoveQuestSlot(QuestSlot questSlot)
    {
        Destroy(questSlot.gameObject);
        questSlots.Remove(questSlot);
    }
}