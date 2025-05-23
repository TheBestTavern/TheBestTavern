using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class QuestBaseSlot : MonoBehaviour
{
    [SerializeField] protected Image icon;

    [SerializeField] protected TextMeshProUGUI npcName;
    [SerializeField] protected TextMeshProUGUI questName;

    [SerializeField] protected Button openLetterBtn;

    protected Quest slotQuest; // 이 슬롯이 표시할 퀘스트
    protected MailBoxContentBase mailBoxContent; // 부모 퀘스트 UI

    protected int index;
    protected bool isSuccessful;
    protected bool isReady;

    public void Init(MailBoxContentBase Content)
    {
        if (!isReady)
        {
            this.mailBoxContent = Content;
            isReady = true;
            transform.localScale = Vector3.one;
        }
    }

    public virtual void SetSlot(int questID, SuccessDegree successDegree, int indexNum)
    {
        // 슬롯 번호 메기기
        index = indexNum;

        // 현재 퀘스트에 맞게 슬롯 정보 갱신(추후 구현)
        slotQuest = Data.GetQuest(questID);
        npcName.text = NPCManager.Instance.NPCData.AllNPC[slotQuest.Origin.givingNPC].Origin.name;


    }
}