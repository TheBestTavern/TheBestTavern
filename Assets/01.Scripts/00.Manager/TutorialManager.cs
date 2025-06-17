using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class TutorialManager : MonoSingleton<TutorialManager>
{
    TtrUI UIController; // 튜토리얼 UI

    public Dictionary<int, TtrStepDef> ttrStepDefDict = new();

    public TtrStepInstance curTtrStepInstance;
    public int? curStepID = null;
    public int? nextStepID = null;

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
        List<TtrStepDef> stepDefs = await AddressablesLoader.Instance.AddressablesListLoadFromLabelAsync<TtrStepDef>("TutorialSteps");

        foreach (var stepDef in stepDefs)
        {
            ttrStepDefDict.Add(stepDef.TutorialStepID, stepDef);
        }

        var go = await AddressablesLoader.Instance.AddressablesLoadAsync<GameObject>("TtrUIController.prefab");
        UIController = Instantiate(go).GetComponent<TtrUI>();
        UIController.transform.SetParent(this.transform, true);
        UIController.Init(this);

        //로드 후 로직
        if (curStepID.HasValue)
            UIController.SetAllObvs();
        if (curTtrStepInstance?.instanceState == TtrInstanceState.InProgress)
            ReadyClear2ProgressStateStep();
        else if (curTtrStepInstance?.instanceState == TtrInstanceState.ReadyClear)
            Progress2ReadyClearStep();
        if(curTtrStepInstance != null)
        {
            ChangeCurTtrStep(curStepID.Value);
        }
    }

    /// <summary>
    /// step 상태 조정 메서드
    /// </summary>
    public void AcceptNewStep(int tutorialStepID)
    {
        if (GameManager.Instance.tutorialState != TtrState.InProgress)
            GameManager.Instance.tutorialState = TtrState.InProgress;
        UIController.DeactivateRope();
        ChangeCurTtrStep(tutorialStepID);
    }

    public void Progress2ReadyClearStep()
    {
        UIController.ActivateRope();
    }

    public void ReadyClear2ProgressStateStep()
    {
        UIController.DeactivateRope();
    }

    public void ClearStep()
    {

        // 기존 구독은 해제하기.
        foreach (var tuple in preSubs)
        {
            typeof(EventBus)
            .GetMethod("UnSubscribe")
            .MakeGenericMethod(tuple.Item1)
            .Invoke(null, new object[] { tuple.Item2 });
        }

        UIController.HideObjectvie();
        if (curStepID == 910013)
        {
            QuitTutorial(TtrState.Completed);
        }
        curTtrStepInstance = null;
        curStepID = null;
    }

    public void QuitTutorial(TtrState state)
    {
        GameManager.Instance.tutorialState = state;

        Destroy(gameObject);
    }

    List<(Type, Delegate)> preSubs = new ();

    private void ChangeCurTtrStep(int tutorialStepID)
    {
        // 인스턴스 생성, currentTutorialStep 할당. 
        TtrStepInstance tutorialStep = new(tutorialStepID, ttrStepDefDict[tutorialStepID].TutorialObjectives.Count);
        curTtrStepInstance = tutorialStep;
        curStepID = GetCurTtrStepDef().TutorialStepID;
        nextStepID = GetCurTtrStepDef().NextTutorialStepID;

        // 현재 스텝에 맞는 동작을, 현재 스텝에 맞는 이벤트에 구독시키기.
        var stepDef = ttrStepDefDict[tutorialStepID];
        for (int i = 0; i < stepDef.TutorialObjectives.Count; i++)
        {
            var stepObvDef = stepDef.TutorialObjectives[i];
            switch (stepObvDef.objectiveDoType)
            {
                case ObvDoType.SceneMove:
                    Action<SceneMove> delegate_SceneMove = OnDoSomething<SceneMove>;
                    EventBus.Subscribe<SceneMove>(OnDoSomething);
                    preSubs.Add((typeof(SceneMove), delegate_SceneMove));
                    break;
                case ObvDoType.CompleteSubmit:
                    Action<CompleteSubmit> delegate_EnterSubmissionMode = OnDoSomething<CompleteSubmit>;
                    EventBus.Subscribe<CompleteSubmit>(OnDoSomething);
                    preSubs.Add((typeof(CompleteSubmit), delegate_EnterSubmissionMode));
                    break;
                //case ObvDoType.OpenProgressInLetter:
                //    Action<OpenProgressInLetter> delegate_OpenProgressInLetter = OnDoSomething<OpenProgressInLetter>;
                //    EventBus.Subscribe<OpenProgressInLetter>(OnDoSomething);
                //    preSubs.Add((typeof(OpenProgressInLetter), delegate_OpenProgressInLetter));
                //    break;
                case ObvDoType.OpenPopup:
                    Action<OpenPopup> delegate_OpenPopup = OnDoSomething<OpenPopup>;
                    EventBus.Subscribe<OpenPopup>(OnDoSomething);
                    preSubs.Add((typeof(OpenPopup), delegate_OpenPopup));
                    break;
                case ObvDoType.AcceptQuest:
                    Action<AcceptQuest> delegate_AcceptQuest = OnDoSomething<AcceptQuest>;
                    EventBus.Subscribe<AcceptQuest>(OnDoSomething);
                    preSubs.Add((typeof(AcceptQuest), delegate_AcceptQuest));
                    break;
                case ObvDoType.GainItem:
                    Action<GainItem> delegate_GainItem = OnDoSomething<GainItem>;
                    EventBus.Subscribe<GainItem>(OnDoSomething);
                    preSubs.Add((typeof(GainItem), delegate_GainItem));
                    OnDoSomething<GainItem>(new GainItem(stepObvDef.detail));
                    break;
            }
        }

        UIController.SetAllObvs();
    }

    public void OnDoSomething<T>(T evt) where T : TtrDoSomething
    {
        List<bool> ChangedIndex = new();
        var stepDef = ttrStepDefDict[curTtrStepInstance.ttrStepDefID];

        // 튵 목표 순회 검사
        TtrStepObvDef stepObvDef;
        for (int i = 0; i < stepDef.TutorialObjectives.Count; i++)
        {
            stepObvDef = stepDef.TutorialObjectives[i];

            if (stepObvDef.detail != evt.Detail || stepObvDef.objectiveDoType != evt.ObvDoType) // 목표와 상관없는 이벤트라면 다음 순회로 넘어가기
            {
                ChangedIndex.Add(false);
                continue;
            }
            else
                ChangedIndex.Add(true);

            if (stepObvDef.tutorialCountType == ObvCountType.Cumulative)
            {
                if (curTtrStepInstance.obvStates[i] != ObvState.InProgress)
                    continue;

                if (stepObvDef.objectiveDoType == evt.ObvDoType && stepObvDef.detail == evt.Detail)
                    curTtrStepInstance.obvCurCounts[i]++;
                else
                    continue;
            }
            else if (stepObvDef.tutorialCountType == ObvCountType.Renew)
            {
                if (stepObvDef.objectiveDoType == ObvDoType.GainItem)
                {
                    if (int.TryParse(evt.Detail, out int itemID))
                        curTtrStepInstance.obvCurCounts[i] = InventoryManager.Instance.Invens[InvenType.Player].GetHowManyCategoryItems(itemID);
                    else
                        Debug.Log($"{curTtrStepInstance}의 {stepObvDef}의 {evt.Detail}가 아이템 획득");
                }
            }

            // 튵 목표 체크
            if (stepObvDef.targetCount <= curTtrStepInstance.obvCurCounts[i])
            {
                if (curTtrStepInstance.obvStates[i] != ObvState.Completed)
                {
                    curTtrStepInstance.obvStates[i] = ObvState.Completed;
                    Debug.Log($"튜토리얼 {i}번 목표: InProgress -> Completed");
                }
            }
            else
            {
                if (curTtrStepInstance.obvStates[i] == ObvState.Completed)
                {
                    curTtrStepInstance.obvStates[i] = ObvState.InProgress;
                    Debug.Log($"튜토리얼 {i}번 목표: Completed -> InProgress");
                }
            }
        }

        if (!ChangedIndex.Any(c => c)) // 변화 없으면 종료
            return;

        // 튵 인스턴스 체크
        for (int j = 0; j < curTtrStepInstance.obvStates.Count; j++)
        {
            if (curTtrStepInstance.obvStates[j] != ObvState.Completed)
            {
                if (curTtrStepInstance.instanceState == TtrInstanceState.ReadyClear)
                {
                    curTtrStepInstance.instanceState = TtrInstanceState.InProgress;
                    ReadyClear2ProgressStateStep();

                    Debug.Log($"{curTtrStepInstance.ttrStepDefID}번 튜토리얼: Completed -> InProgress");
                }
                break;
            }

            if (curTtrStepInstance.instanceState != TtrInstanceState.ReadyClear)
            {
                curTtrStepInstance.instanceState = TtrInstanceState.ReadyClear;
                Progress2ReadyClearStep();
                // Step 완료 상태 트리거
                Debug.Log($"{curTtrStepInstance.ttrStepDefID}번 튜토리얼: InProgress -> Completed");
            }
        }

        //UIController.SetAllObvs();
        for (int k = 0; k < ChangedIndex.Count; k++)
        {
            if (ChangedIndex[k])
                UIController.SetObv(k);
        }
    }

    public TtrStepDef GetCurTtrStepDef()
    {
        if (curTtrStepInstance != null)
            return ttrStepDefDict[curTtrStepInstance.ttrStepDefID];
        else
            return null;
    }

    public TtrStepDef GetTtrStepDef(int ttrID)
    {
        if (ttrStepDefDict.ContainsKey(ttrID))
            return ttrStepDefDict[ttrID];
        else return null;
    }

    public int GetNextStepID(int prevTtrID)
    {
        return GetTtrStepDef(prevTtrID).NextTutorialStepID;
    }

    public void ApplyLoadData(TtrStepInstance curTtrStepInstance, int? curStepID, int? nextStepID)
    {
        this.curTtrStepInstance = curTtrStepInstance;
        this.curStepID = curStepID;
        this.nextStepID = nextStepID;
    }
}
