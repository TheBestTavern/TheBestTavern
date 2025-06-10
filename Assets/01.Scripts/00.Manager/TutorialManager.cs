using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class TutorialManager : MonoSingleton<TutorialManager>
{
    TtrUIController UIController; // 튜토리얼 UI

    public Dictionary<int, TtrStepDef> ttrStepDefDict = new();
    public TtrStepInstance curTtrStepInstance;
    // Start is called before the first frame update
    protected async override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);

        //if() 튜토리얼 매니저를 메인에 배치해두고, 로드로 가져오거나 시작해서 생성한 이후에, 
        //필요하지 않다면 삭제하기 (필요하지 않은 경우는, 플레이어가 튜토리얼을 완수했거나 취소한 경우.)
        if (GameManager.Instance.tutorialState == TtrState.Completed || GameManager.Instance.tutorialState == TtrState.Cancelled)
        {
            Destroy(this);
        }


        //튜토리얼 단계 SO 불러오기.
        var gos = await AddressablesLoader.Instance.AddressablesListLoadFromLabelAsync("TutorialSteps");
        TtrStepDef temp;
        foreach (var go in gos)
        {
            if (go.TryGetComponent<TtrStepDef>(out temp))
            {
                ttrStepDefDict.Add(temp.TutorialStepID, temp);
            }
            else
            {
                Debug.LogError("Wrong TutorialStep is registered to Addressable");
            }
        }

        UIController = await AddressablesLoader.Instance.AddressablesLoadAsync<TtrUIController>("TtrUIController");
        UIController.transform.SetParent(this.transform, true);
        UIController.Init(this);
    }

    public void ChangeCurTutorial(int tutorialStepID)
    {
        // 인스턴스 생성, currentTutorialStep 할당.
        TtrStepInstance tutorialStep = new(tutorialStepID);
        curTtrStepInstance = tutorialStep;

        // 기존 구독은 해제하기.
        //EventBus.UnSubscribe<TtrDoSomething>(OnDoSomething);

        // 현재 스텝에 맞는 동작을, 현재 스텝에 맞는 이벤트에 구독시키기.
        var stepDef = ttrStepDefDict[tutorialStepID];
        for (int i = 0; stepDef.TutorialObjectives.Count > 0; i++)
        {
            var stepObvDef = stepDef.TutorialObjectives[i];
            switch (stepObvDef.objectiveDoType)
            {
                case ObvDoType.SceneMove:
                    EventBus.Subscribe<SceneMove>(OnDoSomething);
                    break;

            }

        }
    }

    public void OnDoSomething<T>(T evt) where T : TtrDoSomething
    {
        var stepDef = ttrStepDefDict[curTtrStepInstance.ttrStepDefID];

        //// 튵 진행중이 아니면 메서드 종료.
        //if (curTtrStepInstance.instanceState != TtrInstanceState.InProgress )
        //    return;

        for (int i = 0; stepDef.TutorialObjectives.Count > 0; i++)
        {
            var stepObv = stepDef.TutorialObjectives[i];
            if (stepObv.tutorialCountType == ObvCountType.Cumulative)
            {

            }
            else if(stepObv.tutorialCountType == ObvCountType.Renew)
            {

            }
            // 튵 목표가 갱신
            if (stepObv.objectiveDoType == evt.ObvDoType && stepObv.doWhat == evt.Detail)
            {
                switch (stepObv.tutorialCountType)
                {
                    case ObvCountType.Cumulative:
                        if (curTtrStepInstance.ObvsStates[i] == ObvState.InProgress)
                            curTtrStepInstance.curCount++;
                        else
                            continue;
                        break;
                    case ObvCountType.Renew:
                        switch (stepObv.objectiveDoType)
                        {
                            case ObvDoType.GainItem:
                                if (int.TryParse(evt.Detail, out int itemID))
                                    curTtrStepInstance.curCount = InventoryManager.Instance.Invens[InvenType.Player].GetHowManyCategoryItems(itemID);
                                break;
                        }
                        break;
                }

                // 튵 목표 체크
                if (stepObv.targetCount <= curTtrStepInstance.curCount)
                {
                    if (curTtrStepInstance.ObvsStates[i] != ObvState.Completed)
                    {
                        curTtrStepInstance.ObvsStates[i] = ObvState.Completed;
                        Debug.Log($"튜토리얼 {i}번 목표: InProgress -> Completed");
                    }
                }
                else
                {
                    if (curTtrStepInstance.ObvsStates[i] == ObvState.Completed)
                    {
                        curTtrStepInstance.ObvsStates[i] = ObvState.InProgress;
                        Debug.Log($"튜토리얼 {i}번 목표: Completed -> InProgress");
                    }
                }

                // 튵 인스턴스 체크
                for (int j = 0; j < curTtrStepInstance.ObvsStates.Count; j++)
                {
                    if (curTtrStepInstance.ObvsStates[j] == ObvState.Completed)
                        break;

                    if (curTtrStepInstance.instanceState != TtrInstanceState.Completed)
                    {
                        curTtrStepInstance.instanceState = TtrInstanceState.Completed;
                        Debug.Log($"{curTtrStepInstance.ttrStepDefID}번 튜토리얼 완료 상태");

                    }
                }


            }
        }
    }
    //public void OnDoSomething(TtrDoSomething evt)
    //{
    //    var stepDef = ttrStepDefDict[curTtrStepInstance.ttrStepDefID];

    //    // 튵 진행중이 아니면 메서드 종료.
    //    if (curTtrStepInstance.instanceState != TtrInstanceState.InProgress)
    //        return;

    //    for (int i = 0; stepDef.TutorialObjectives.Count > 0; i++)
    //    {
    //        var stepObv = stepDef.TutorialObjectives[i];
    //        // 튵 목표가 갱신
    //        if (curTtrStepInstance.ObvsStates[i] == ObvState.InProgress && stepObv.objectiveDoType == ObvDoType.SceneMove && stepObv.doWhat == evt.SceneName)
    //        {
    //            switch (stepObv.tutorialCountType)
    //            {
    //                case ObvCountType.Cumulative:
    //                    curTtrStepInstance.curCount++;
    //                    break;
    //            }

    //            // 튵 목표 완료됐는지 체크
    //            if (stepObv.targetCount <= curTtrStepInstance.curCount)
    //            {
    //                curTtrStepInstance.ObvsStates[i] = ObvState.Completed;

    //                // 튵 인스턴스 완료됐는지 체크
    //                for (int j = 0; j < curTtrStepInstance.ObvsStates.Count; j++)
    //                {
    //                    if (curTtrStepInstance.ObvsStates[j] == ObvState.Completed)
    //                        break;

    //                    curTtrStepInstance.instanceState = TtrInstanceState.Completed;
    //                }
    //            }
    //        }
    //    }
    //}


    public TtrStepDef GetCurTtrStepDef()
    {
        return ttrStepDefDict[curTtrStepInstance.ttrStepDefID];
    }
}
