using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoSingleton<TutorialManager>
{
    List<bool> levelState = new(); // 구간별 클리어 여부. (미리 클리어 해놓기 불가능)
    TutorialUIController tutorialUI; // 튜토리얼 UI
    int currentlevel; // 현재까지 명시적으로 클리어된 구간


    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();

        EventBus.Subscribe<OpenMailEvent>(OnOpenMailEvent);
        EventBus.Subscribe<AcceptQuest>(OnAcceptQuest);
        EventBus.Subscribe<OpenProgressInQuestSlot>(OnOpenProgressInQuestSlot);
        EventBus.Subscribe<OpenDetailPopup>(OnOpenDetailPopup);
        EventBus.Subscribe<GainItem>(OnGainItem);
        EventBus.Subscribe<EnterCookScene>(OnEnterCookScene);
        EventBus.Subscribe<SuccessProcessCook>(OnSuccessProcessCook);
        EventBus.Subscribe<SuccessMixCook>(OnSuccessMixCook);
        EventBus.Subscribe<EnterSubmissionMode>(OnEnterSubmissionMode);
    }

    public void OnOpenMailEvent(OpenMailEvent evt) => levelState[0] = true;
    public void OnAcceptQuest(AcceptQuest evt)
    {
        if (evt.questID == 900001)
            levelState[1] = true;
    }
    public void OnOpenProgressInQuestSlot(OpenProgressInQuestSlot evt)
    {
        if (evt.questID == 900001)
            levelState[2] = true;
    }
    public void OnOpenDetailPopup(OpenDetailPopup evt)
    {
        if (evt.FoodCategoryID == 110181)
            levelState[3] = true;
    }
    public void OnGainItem(GainItem evt)
    {
    }
    public void OnEnterCookScene(EnterCookScene evt)
    {

    }
    public void OnSuccessProcessCook(SuccessProcessCook evt)
    {

    }
    public void OnSuccessMixCook(SuccessMixCook evt)
    {

    }
    public void OnEnterSubmissionMode(EnterSubmissionMode evt)
    {

    }
}
