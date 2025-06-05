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
    //public override void Init()
    //{
    //    if (_isInitialized) return;
    //    base.Init();
    //}

    public EndingType CurrentEndingType { get; private set; }

    // 요리 도감 100% 달성 시 호출
    public async void TriggerEnding()
    {
        Debug.Log("엔딩씬 시작");
        await SceneLoader.Instance.LoadSceneAsync(SceneType.EndingScene);
    }

    public void SelectEnding(EndingType type)
    {
        switch (type)
        {
            case EndingType.Home:
                ShowHomeEnding();
                break;
            case EndingType.Stay:
                ShowStayEnding();
                break;
        }
    }

    public void ShowHomeEnding()
    {

    }

    public void ShowStayEnding()
    {

    }
}
