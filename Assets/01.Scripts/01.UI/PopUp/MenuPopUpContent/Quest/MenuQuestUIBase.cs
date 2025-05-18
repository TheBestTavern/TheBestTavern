using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.ResourceProviders.Simulation;

public abstract class MenuQuestUIBase<TList> : BaseMenuContentUI
{
    protected List<QuestSlot> slots = new();
    protected TList toShowList;
    [SerializeField] protected Transform spawnTsr;

    public override void CreateContent()
    {
        base.CreateContent();

        SetList();

        EventBus.Subscribe<QuestClickLetterBtnEvent>(OpenLetter);
    }

    public virtual void SetList()
    {
    }

    //이벤트 버스 함수
    public virtual void OpenLetter(QuestClickLetterBtnEvent evt)
    {
    }
}
