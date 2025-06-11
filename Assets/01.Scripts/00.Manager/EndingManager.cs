using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EndingType
{
    Home,
    Stay
}

public class EndingManager : MonoSingleton<EndingManager>
{
    public bool hasSeenEnding { get; private set; }
    //public override void Init()
    //{
    //    if (_isInitialized) return;
    //    base.Init();
    //}

    public EndingType CurrentEndingType { get; private set; }

    public override void Init()
    {
        if(_isInitialized) return;
        base.Init();
        DontDestroyOnLoad(gameObject);
        EventBus.Subscribe<EndEvent>(OnEndingTriggered);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        EventBus.UnSubscribe<EndEvent>(OnEndingTriggered);
    }

    public void SetEndingState(bool hasSeen)
    {
        hasSeenEnding = hasSeen;
    }
    // 요리 도감 100% 달성 시 호출
    public async void TriggerEnding()
    {
        Debug.Log("엔딩씬 시작");
        await SceneLoader.Instance.LoadSceneAsync(SceneType.EndingScene);
    }

    void OnEndingTriggered(EndEvent evt)
    {
        if (hasSeenEnding)
        {
            Debug.Log("이미 엔딩 봤음");
            return;
        }

        hasSeenEnding = true;
        TriggerEnding();
    }
    //public void SelectEnding(EndingType type)
    //{
    //    switch (type)
    //    {
    //        case EndingType.Home:
    //            ShowHomeEnding();
    //            break;
    //        case EndingType.Stay:
    //            ShowStayEnding();
    //            break;
    //    }
    //}

    //public void ShowHomeEnding()
    //{

    //}

    //public void ShowStayEnding()
    //{

    //}
}
