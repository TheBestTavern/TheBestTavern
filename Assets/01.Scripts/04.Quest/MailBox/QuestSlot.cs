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
    QuestUI questUI; // 부모 퀘스트 UI

    int index;

    QuestLetter currentLetter;

    bool isReady;

    private void Start() // 테스트용. SetSlot과 충돌. 나중에 지워야함.
    {
        openLetterBtn.onClick.RemoveAllListeners();
        openLetterBtn.onClick.AddListener(() => OpenLetter());
    }

    public void Init(QuestUI questUI)
    {
        if (!isReady)
        {
            this.questUI = questUI;
            isReady = true;
        }
    }

    public void SetSlot(Quest quest, int indexNum)
    {
        // 슬롯 번호 메기기
        index = indexNum;

        // 현재 퀘스트에 맞게 슬롯 정보 갱신(추후 구현)
        slotQuest = quest;
        npcName.text = NPCManager.Instace.NPCData.AllNPC[quest.origin.givingNPC].origin.name;
        questName.text = quest.origin.name;
        Debug.Log($"{index}번 슬롯 정보 입력완료");

        // 버튼에 메서드 구독
        openLetterBtn.onClick.RemoveAllListeners();
        openLetterBtn.onClick.AddListener(() => OpenLetter());
        Debug.Log($"{index}번 슬롯 메서드 구독완료");

    }

    private void OpenLetter() // 나중에 풀링으로 관리해보기
    {
        // 1. 이미 열린 편지는 파괴
        if(currentLetter != null)
        {
            Destroy(currentLetter.gameObject);
            currentLetter = null;
            Debug.Log($"{index}번 슬롯 열린 편지 파괴");

        }

        // 2. 편지 프리팹 생성
        currentLetter = questUI.OpenLetter(slotQuest);
        Debug.Log($"{index}번 편지 열람");
    }
}