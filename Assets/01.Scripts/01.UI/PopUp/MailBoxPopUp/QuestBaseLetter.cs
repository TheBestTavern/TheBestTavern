using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class QuestBaseLetter : BasePopUp
{
    protected Quest quest;
    protected QuestBaseSlot baseQuestSlot;

    [SerializeField] protected List<Button> buttons;
    [SerializeField] protected TextMeshProUGUI title;
    [SerializeField] protected TextMeshProUGUI bodyText;
    [SerializeField] protected TextMeshProUGUI from;

    public bool IsReady { get; protected set; }
    public Action<QuestBaseSlot> OnCompleteLetter;

    /// <summary>
    /// 편지 초기화
    /// </summary>

    // 편지 생성시 한번 초기화
    public virtual void FirstInit(Action<QuestBaseSlot> action)
    {
        OnCompleteLetter = action;
    }

    // 편지 열때마다 필요한 초기화.
    public virtual void On(Quest quest, QuestBaseSlot baseQuestSlot)
    {
        // 문구 초기화
        this.quest = quest;
        this.baseQuestSlot = baseQuestSlot;
        title.text = quest.origin.name;
        from.text = NPCManager.Instance.NPCData.AllNPC[quest.origin.givingNPC].origin.name;
    }

    /// <summary>
    /// 버튼 구독 메서드들
    /// </summary>

    // 수락 버튼 메서드

    /// <summary>
    /// 편지 열고 닫을때 효과
    /// </summary>

    public override void OnOpen()
    {
        base.OnOpen();
        Debug.Log("여는 효과음 재생");
    }

    public override void OnClose()
    {
        base.OnClose();
        Debug.Log("닫는 효과음 재생");

    }

    public void TriggerOnCompleteLetter()
    {
        OnCompleteLetter?.Invoke(baseQuestSlot);
    }
}
