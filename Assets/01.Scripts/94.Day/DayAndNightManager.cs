using System.Collections;
using UnityEngine;

public class DayAndNightManager : MonoSingleton<DayAndNightManager>
{
    public Material nightMat;
    [Range(0f, 1f)] public float process; // 현재 하루의 진행도(시간)
    [Range(0f, 1f)] public float limitProcess; // 시간이 흐를때 리미트
    [SerializeField, Range(0f, 10f)] float duration = 3f;
    public AnimationCurve saturationCurve;
    public AnimationCurve lightnessCurve;

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