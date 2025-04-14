using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestLetter : BasePopUp
{
    private int days;
    private Quest quest;
    private QuestSlot questSlot;

    [SerializeField] Button btn7;
    [SerializeField] Button btn11;
    [SerializeField] Button btn14;
    bool isSetDays;

    [SerializeField] Button btnYes;
    [SerializeField] Button btnNo;

    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI bodyText;
    [SerializeField] TextMeshProUGUI from;

    public bool IsReady { get; private set; }

    // 편지 생성시 한번 초기화
    public void FirstInit(Quest quest)
    {
        // 같은 기능의 버튼 초기화
        btn7.onClick.AddListener(() => OnClickDays(7));
        btn11.onClick.AddListener(() => OnClickDays(11));
        btn14.onClick.AddListener(() => OnClickDays(14));
        btnYes.onClick.AddListener(() => AcceptQuest());
        btnNo.onClick.AddListener(() => RejectQuest());
        IsReady = true;
    }

    // 편지 열때마다 필요한 초기화.
    public void EveryInit(Quest quest, QuestSlot questSlot)
    {
        // 문구 초기화
        this.quest = quest;
        this.questSlot = questSlot;
        isSetDays = false;
        days = 0;
        title.text = quest.origin.name;
        bodyText.text = quest.origin.description;
        from.text = NPCManager.Instance.NPCData.AllNPC[quest.origin.givingNPC].origin.name;
    }

    private void OnClickDays(int day)
    {
        days = day;
        isSetDays = true;
    }

    // 수락 버튼 메서드
    private void AcceptQuest()
    {
        if (isSetDays)
        {
            Debug.Log($"{days}일 뒤로 퀘스트 수락");
            QuestManager.Instance.AcceptQuest(quest, days, questSlot);
        }
        else
        {
            Debug.Log($"일수가 선택안됨");
            UIManager.Instance.ShowPopUp(PopUpType.Alarm);
            UIManager.Instance.alarmPopUp.SetAlarm("일수를 먼저 선택해주세요.");
        }
    }

    // 거절 버튼 메서드 ( 필요할지 의문 )
    private void RejectQuest()
    {
        //미구현, 퀘스트 거절 시 퀘스트 목록에서 지우고 한동안 퀘스트 안뜨게 하는 방식 생각해봄.
        Debug.Log($"퀘스트 거절");
    }

    public override void OnOpen()
    {
        base.OnOpen();
        Debug.Log("여는 효과음 재생");
    }

    public override void OnClose()
    {
        base.OnClose();
        gameObject.SetActive(false);
        Debug.Log("닫는 효과음 재생");

    }
}
