using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestLetter : BasePopUp
{
    private int days;

    [SerializeField] Button btn7;
    [SerializeField] Button btn11;
    [SerializeField] Button btn14;

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
        btn7.onClick.AddListener(() => days = 7);
        btn11.onClick.AddListener(() => days = 11);
        btn14.onClick.AddListener(() => days = 14);
        btnYes.onClick.AddListener(() => AcceptQuest());
        btnNo.onClick.AddListener(() => RejectQuest());
        IsReady = true;
    }

    // 편지 열때마다 필요한 초기화.
    public void EveryInit(Quest quest)
    {
        // 문구 초기화
        title.text = quest.origin.name;
        bodyText.text = quest.origin.description;
        from.text = NPCManager.Instance.NPCData.AllNPC[quest.origin.givingNPC].origin.name;
    }

    // 수락 버튼 메서드
    private void AcceptQuest()
    {
        //미구현
        Debug.Log($"{days}일 뒤로 퀘스트 수락");
    }

    // 거절 버튼 메서드
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
