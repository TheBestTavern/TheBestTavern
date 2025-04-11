using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestSlot : MonoBehaviour
{
    [SerializeField] Image icon;

    [SerializeField] TextMeshProUGUI npcName;
    [SerializeField] TextMeshProUGUI questName;

    [SerializeField] Button openLetterBtn;

    Quest slotQuest; // 이 슬롯이 표시할 퀘스트
    QuestContent questContent; // 부모 퀘스트 UI

    int index;


    bool isReady;

    private void Start() // 테스트용. SetSlot과 충돌. 나중에 지워야함.
    {
        openLetterBtn.onClick.RemoveAllListeners();
        openLetterBtn.onClick.AddListener(() => OpenLetter());
    }

    public void Init(QuestContent questContent)
    {
        if (!isReady)
        {
            this.questContent = questContent;
            isReady = true;
        }
    }

    public void SetSlot(Quest quest, int indexNum)
    {
        // 슬롯 번호 메기기
        index = indexNum;

        // 현재 퀘스트에 맞게 슬롯 정보 갱신(추후 구현)
        slotQuest = quest;
        npcName.text = NPCManager.Instance.NPCData.AllNPC[quest.origin.givingNPC].origin.name;
        questName.text = quest.origin.name;
        Debug.Log($"{index}번 슬롯 정보 입력완료");

        // 버튼에 메서드 구독
        openLetterBtn.onClick.RemoveAllListeners();
        openLetterBtn.onClick.AddListener(() => OpenLetter());
        Debug.Log($"{index}번 슬롯 메서드 구독완료");

    }

    private void OpenLetter() // 나중에 풀링으로 관리해보기
    {
        questContent.OpenLetter(slotQuest);
        Debug.Log($"{index}번 편지 열람");
    }
}