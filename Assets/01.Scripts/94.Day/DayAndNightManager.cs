using System.Collections;
using System.Net;
using UnityEngine;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Unity.VisualScripting;

public class DayAndNightManager : MonoSingleton<DayAndNightManager>
{
    public Material nightMat;
    [Range(0f, 1f)] public float process = 0; // 현재 하루의 진행도(시간)
    [Range(0f, 1f)] public float limitProcess; // 시간이 흐를때 리미트
    [Range(0f, 3f)] public float duration = 0.0001f;
    public AnimationCurve saturationCurve;
    public AnimationCurve lightnessCurve;
    public async override void Init()
    {
        if (_isInitialized) return;
        base.Init();
        DontDestroyOnLoad(gameObject);

        var container = await AddressablesLoader.Instance.AddressablesLoadAsync<ScriptableObject>("DayAndNightManagerContainer.asset");
        ManagerContainer so = (ManagerContainer)container;

        if (nightMat == null)
        {
            nightMat = so.nightMaterial;
        }

        if (saturationCurve == null)
        {
            saturationCurve = so.saturationCurve;
        }

        if (lightnessCurve == null)
        {
            lightnessCurve = so.lightnessCurve;
        }
    }

    public void pass1hour() // 한시간(정확히는 하루의 1/10씩 밝기 변경. 2차 시간표현 구현할때 필요한 기능. 제대로 구현하려면 process에 제한 줘야함.
    {
        limitProcess = process + 0.1f;
        TriggerTimeProcess(limitProcess);
    }

    public void TriggerTimeProcess(float targetProcess)
    {
        StartCoroutine(LerpProcess(targetProcess));
    }

    IEnumerator LerpProcess(float targetProcess)
    {
        while (true)
        {
            if (process < limitProcess)
            {
                process += Time.deltaTime / duration;
                nightMat.SetFloat("_Saturation", saturationCurve.Evaluate(process));
                nightMat.SetFloat("_Lightness", lightnessCurve.Evaluate(process));
            }

            if(process >= 1)
            {
                DayAndNightManager.Instance.process = 0;
                DayAndNightManager.Instance.limitProcess = 0;
                yield break;
            }
            if (targetProcess < process) yield break;

            yield return null;
        }
    }
}